IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Activity]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Activity](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[DisplayName] [nvarchar](255) NOT NULL,
		[Action] [nvarchar](max) NOT NULL,
		[Audience] [nvarchar](50) NOT NULL CONSTRAINT [DF_Activity_Audience]  DEFAULT (N''),
		[CreatedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_Activity_CreatedBy]  DEFAULT (N'System Account'),
		[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_Activity_CreatedOn]  DEFAULT (getutcdate()),
		[ModifiedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_Activity_ModifiedBy]  DEFAULT (N'System Account'),
		[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_Activity_ModifiedOn]  DEFAULT (getutcdate()),
	 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Audience' AND Object_ID = Object_ID(N'[dbo].[Activity]'))
BEGIN
	ALTER TABLE [dbo].[Activity] ADD [Audience] [nvarchar](50) NOT NULL CONSTRAINT [DF_Activity_Audience] DEFAULT (N'')
END


-- update table version
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.Activity'
SET @Version = '1.0.0'

IF NOT EXISTS (SELECT * FROM [dbo].[TableVersions] WHERE Name = @Name)
	INSERT INTO [dbo].[TableVersions] (Name, Version) VALUES (@Name,  @Version)
IF NOT EXISTS (SELECT * FROM [dbo].[TableVersions] WHERE Name = @Name AND Version = @Version)
	UPDATE [dbo].[TableVersions] SET Version = @Version, UpdatedOn = getutcdate() WHERE Name = @Name