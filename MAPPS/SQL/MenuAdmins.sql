IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuAdmins]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[MenuAdmins](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[MenuID] [int] NOT NULL,
		[UserName] [nvarchar](50) NOT NULL,
		[DisplayName] [nvarchar](255) NOT NULL,
		[IsDisabled] [bit] NOT NULL,
		[CreatedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_MenuAdmins_CreatedBy]  DEFAULT ('System Account'),
		[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MenuAdmins_CreatedOn]  DEFAULT (getutcdate()),
		[ModifiedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_MenuAdmins_ModifiedBy]  DEFAULT ('System Account'),
		[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_MenuAdmins_ModifiedOn]  DEFAULT (getutcdate()),
	 CONSTRAINT [PK_MenuAdmins] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[MenuAdmins]  WITH CHECK ADD  CONSTRAINT [FK_MenuAdmins_Menus] FOREIGN KEY([MenuID])
	REFERENCES [dbo].[Menus] ([ID])

	ALTER TABLE [dbo].[MenuAdmins] CHECK CONSTRAINT [FK_MenuAdmins_Menus]
END

-- update table version
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.MenuAdmins'
SET @Version = '1.0'

IF NOT EXISTS (SELECT * FROM [dbo].[TableVersions] WHERE Name = @Name)
	INSERT INTO [dbo].[TableVersions] (Name, Version) VALUES (@Name,  @Version)
IF NOT EXISTS (SELECT * FROM [dbo].[TableVersions] WHERE Name = @Name AND Version = @Version)
	UPDATE [dbo].[TableVersions] SET Version = @Version, UpdatedOn = getutcdate() WHERE Name = @Name