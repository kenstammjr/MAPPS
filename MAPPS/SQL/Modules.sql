IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Modules]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Modules](
		[ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](255) NOT NULL,
		[Directory] [nvarchar](50) NOT NULL,
		[URL] [nvarchar](255) NOT NULL,
		[ImageURL] [nvarchar](255) NOT NULL,
		[DBVersion] [nvarchar](50) NOT NULL,
		[DisplayIndex] [int] NOT NULL,
		[IsActive] [bit] NOT NULL,
		[AdminURL] [nvarchar](255) NOT NULL,
		[CreatedBy] [nvarchar](150) NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[ModifiedBy] [nvarchar](150) NOT NULL,
		[ModifiedOn] [datetime] NOT NULL,
	 CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_Description]  DEFAULT (N' ') FOR [Description]
	ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_Directory]  DEFAULT (N' ') FOR [Directory]
	ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_URL]  DEFAULT (N' ') FOR [URL]
	ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_ImageURL]  DEFAULT (N'/_layouts/15/images/GenericFeature.gif') FOR [ImageURL]
	ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_DisplayIndex]  DEFAULT ((0)) FOR [DisplayIndex]
	ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_IsActive]  DEFAULT ((1)) FOR [IsActive]
	ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_AdminURL]  DEFAULT ('/_layouts/15/nga/pages/administration.aspx') FOR [AdminURL]
	ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_CreatedBy]  DEFAULT (N'System Account') FOR [CreatedBy]
	ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
	ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_ModifiedBy]  DEFAULT (N'System Account') FOR [ModifiedBy]
	ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_ModifiedOn]  DEFAULT (getutcdate()) FOR [ModifiedOn]
END

/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.Modules'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)
