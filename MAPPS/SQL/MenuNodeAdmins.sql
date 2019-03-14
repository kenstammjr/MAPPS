IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuNodeAdmins]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[MenuNodeAdmins](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[MenuNodeID] [int] NOT NULL,
		[UserName] [nvarchar](50) NOT NULL,
		[DisplayName] [nvarchar](255) NOT NULL,
		[IsDisabled] [bit] NOT NULL,
		[CreatedBy] [nvarchar](150) NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[ModifiedBy] [nvarchar](150) NOT NULL,
		[ModifiedOn] [datetime] NOT NULL,
	 CONSTRAINT [PK_MenuNodeAdmins] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[MenuNodeAdmins] ADD  CONSTRAINT [DF_MenuNodeAdmins_CreatedBy]  DEFAULT (N'System Account') FOR [CreatedBy]
	ALTER TABLE [dbo].[MenuNodeAdmins] ADD  CONSTRAINT [DF_MenuNodeAdmins_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
	ALTER TABLE [dbo].[MenuNodeAdmins] ADD  CONSTRAINT [DF_MenuNodeAdmins_ModifiedBy]  DEFAULT (N'System Account') FOR [ModifiedBy]
	ALTER TABLE [dbo].[MenuNodeAdmins] ADD  CONSTRAINT [DF_MenuNodeAdmins_ModifiedOn]  DEFAULT (getutcdate()) FOR [ModifiedOn]
	ALTER TABLE [dbo].[MenuNodeAdmins]  WITH CHECK ADD  CONSTRAINT [FK_MenuNodeAdmins_MenuNodes] FOREIGN KEY([MenuNodeID])
	REFERENCES [dbo].[MenuNodes] ([ID])

	ALTER TABLE [dbo].[MenuNodeAdmins] CHECK CONSTRAINT [FK_MenuNodeAdmins_MenuNodes]
END

-- update table version
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.MenuNodeAdmins'
SET @Version = '1.0'

IF NOT EXISTS (SELECT * FROM [dbo].[TableVersions] WHERE Name = @Name)
	INSERT INTO [dbo].[TableVersions] (Name, Version) VALUES (@Name,  @Version)
IF NOT EXISTS (SELECT * FROM [dbo].[TableVersions] WHERE Name = @Name AND Version = @Version)
	UPDATE [dbo].[TableVersions] SET Version = @Version, UpdatedOn = getutcdate() WHERE Name = @Name