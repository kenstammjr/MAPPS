IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServerDrives]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[ServerDrives](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[ServerID] [int] NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](350) NOT NULL,
		[Size] [int] NOT NULL,
		[Free] [int] NOT NULL,
		[SeverityCode] [int] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [nvarchar](150) NOT NULL,
		[ModifiedOn] [datetime] NOT NULL,
		[ModifiedBy] [nvarchar](150) NOT NULL,
	 CONSTRAINT [PK_ServerDrives] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[ServerDrives] ADD  CONSTRAINT [DF_ServerDrives_Description]  DEFAULT ('') FOR [Description]
	ALTER TABLE [dbo].[ServerDrives] ADD  CONSTRAINT [DF_ServerDrives_Size]  DEFAULT ((0)) FOR [Size]
	ALTER TABLE [dbo].[ServerDrives] ADD  CONSTRAINT [DF_ServerDrives_Free]  DEFAULT ((0)) FOR [Free]
	ALTER TABLE [dbo].[ServerDrives] ADD  CONSTRAINT [DF_ServerDrives_SeverityCode]  DEFAULT ((3)) FOR [SeverityCode]
	ALTER TABLE [dbo].[ServerDrives] ADD  CONSTRAINT [DF_ServerDrives_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
	ALTER TABLE [dbo].[ServerDrives] ADD  CONSTRAINT [DF_ServerDrives_CreatedBy]  DEFAULT (N'System Account') FOR [CreatedBy]
	ALTER TABLE [dbo].[ServerDrives] ADD  CONSTRAINT [DF_ServerDrives_ModifiedOn]  DEFAULT (getutcdate()) FOR [ModifiedOn]
	ALTER TABLE [dbo].[ServerDrives] ADD  CONSTRAINT [DF_ServerDrives_ModifiedBy]  DEFAULT (N'System Account') FOR [ModifiedBy]
	ALTER TABLE [dbo].[ServerDrives]  WITH CHECK ADD  CONSTRAINT [FK_ServerDrives_Servers] FOREIGN KEY([ServerID])
	REFERENCES [dbo].[Servers] ([ID])
	ON DELETE CASCADE

	ALTER TABLE [dbo].[ServerDrives] CHECK CONSTRAINT [FK_ServerDrives_Servers]
END

/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.ServerDrives'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)