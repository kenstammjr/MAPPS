IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServerPorts]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[ServerPorts](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[ServerID] [int] NOT NULL,
		[Port] [nvarchar](50) NOT NULL,
		[Protocol] [nvarchar](50) NOT NULL,
		[CreatedBy] [nvarchar](150) NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[ModifiedBy] [nvarchar](150) NOT NULL,
		[ModifiedOn] [datetime] NOT NULL,
	 CONSTRAINT [PK_ServerPorts] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[ServerPorts]  WITH CHECK ADD  CONSTRAINT [FK_ServerPorts_Servers] FOREIGN KEY([ServerID])
	REFERENCES [dbo].[Servers] ([ID])

	ALTER TABLE [dbo].[ServerPorts] CHECK CONSTRAINT [FK_ServerPorts_Servers]
END

/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.ServerPorts'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)