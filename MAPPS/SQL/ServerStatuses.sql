IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServerStatuses]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[ServerStatuses](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](255) NOT NULL,
		[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_ServerStatuses_CreatedOn]  DEFAULT (getutcdate()),
		[CreatedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_ServerStatuses_CreatedBy]  DEFAULT (N'System Account'),
		[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_ServerStatuses_ModifiedOn]  DEFAULT (getutcdate()),
		[ModifiedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_ServerStatuses_ModifiedBy]  DEFAULT (N'System Account'),
	 CONSTRAINT [PK_ServerStatuses] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.ServerStatuses'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)