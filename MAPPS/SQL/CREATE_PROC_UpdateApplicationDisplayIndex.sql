CREATE PROCEDURE [dbo].[usp_UpdateApplicationDisplayIndex]

@Direction nvarchar(4),
@ItemID int

AS
 
declare @NewValue int
declare @ID int
declare @DisplayIndex int
declare @Iterator int

SET @Iterator = 1

IF @Direction = 'up'
	IF (SELECT DisplayIndex From Applications where ID = @ItemID) < (SELECT MAX(DisplayIndex) From Applications)
		SET @NewValue = (SELECT DisplayIndex From Applications where ID = @ItemID) + 1
	ELSE
		SET @NewValue = (SELECT MAX(DisplayIndex) From Applications)
ELSE
	IF (SELECT DisplayIndex From Applications where ID = @ItemID) > 2
		SET @NewValue = (SELECT DisplayIndex From Applications where ID = @ItemID) - 1
	ELSE
		SET @NewValue = (SELECT DisplayIndex From Applications where ID = @ItemID)


-- get current displayindex of propertyid
-- id 4 = 4
UPDATE Applications SET DisplayIndex = 9999 WHERE ID = @ItemID


declare c_Items cursor static
for
select ID, DisplayIndex From Applications WHERE DisplayIndex <> 9999 ORDER BY DisplayIndex

open c_Items
if @@cursor_rows = 0
begin
	print 'No items found. '
	close c_Items
	deallocate c_Items
	return 1
end

fetch next from c_Items into @ID, @DisplayIndex

while @@fetch_status = 0
begin
	IF @Iterator = @NewValue SET @Iterator = @Iterator + 1
	UPDATE Applications SET DisplayIndex = @Iterator WHERE ID = @ID
	SET @Iterator = @Iterator + 1
	fetch next from c_Items into @ID, @DisplayIndex
end

close c_Items
deallocate c_Items

UPDATE Applications SET DisplayIndex = @NewValue WHERE ID = @ItemID

set nocount off
return 0