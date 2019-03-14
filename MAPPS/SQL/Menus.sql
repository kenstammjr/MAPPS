IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menus]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Menus](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](255) NOT NULL,
		[IsVisible] [bit] NOT NULL,
		[CreatedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_Menus_CreatedBy]  DEFAULT ('System Account'),
		[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_Menus_CreatedOn]  DEFAULT (getutcdate()),
		[ModifiedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_Menus_ModifiedBy]  DEFAULT ('System Account'),
		[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_Menus_ModifiedOn]  DEFAULT (getutcdate()),
	 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

-- update table version
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.Menus'
SET @Version = '1.0'

IF NOT EXISTS (SELECT * FROM [dbo].[TableVersions] WHERE Name = @Name)
	INSERT INTO [dbo].[TableVersions] (Name, Version) VALUES (@Name,  @Version)
IF NOT EXISTS (SELECT * FROM [dbo].[TableVersions] WHERE Name = @Name AND Version = @Version)
	UPDATE [dbo].[TableVersions] SET Version = @Version, UpdatedOn = getutcdate() WHERE Name = @Name