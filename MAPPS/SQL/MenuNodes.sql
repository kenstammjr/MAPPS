IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuNodes]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[MenuNodes](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[MenuID] [int] NOT NULL CONSTRAINT [DF_MenuItems_MenuID]  DEFAULT ((1)),
		[ParentID] [int] NOT NULL CONSTRAINT [DF_MenuItems_ParentID]  DEFAULT ((0)),
		[Name] [nvarchar](50) NOT NULL CONSTRAINT [DF_MenuItems_Name]  DEFAULT (N''),
		[Description] [nvarchar](255) NOT NULL CONSTRAINT [DF_MenuItems_Description]  DEFAULT (N''),
		[URL] [nvarchar](1000) NOT NULL CONSTRAINT [DF_MenuItems_URL]  DEFAULT (N''),
		[Target] [nvarchar](50) NOT NULL CONSTRAINT [DF_MenuItems_Target]  DEFAULT (N'_self'),
		[DisplayIndex] [int] NOT NULL CONSTRAINT [DF_MenuItems_DisplayIndex]  DEFAULT ((0)),
		[IsVisible] [bit] NOT NULL CONSTRAINT [DF_MenuItems_IsVisible]  DEFAULT ((1)),
		[CreatedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_MenuItems_CreatedBy]  DEFAULT (N'System Account'),
		[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MenuItems_CreatedOn]  DEFAULT (getutcdate()),
		[ModifiedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_MenuItems_ModifiedBy]  DEFAULT (N'System Account'),
		[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_MenuItems_ModifiedOn]  DEFAULT (getutcdate()),
	 CONSTRAINT [PK_MenuItems] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

END

-- update table version
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.MenuNodes'
SET @Version = '1.0'

IF NOT EXISTS (SELECT * FROM [dbo].[TableVersions] WHERE Name = @Name)
	INSERT INTO [dbo].[TableVersions] (Name, Version) VALUES (@Name,  @Version)
IF NOT EXISTS (SELECT * FROM [dbo].[TableVersions] WHERE Name = @Name AND Version = @Version)
	UPDATE [dbo].[TableVersions] SET Version = @Version, UpdatedOn = getutcdate() WHERE Name = @Name