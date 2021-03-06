IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TableVersions]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[TableVersions](
		[Name] [nvarchar](100) NOT NULL CONSTRAINT [DF_TableVersions_TableName]  DEFAULT (''),
		[Version] [nvarchar](50) NOT NULL CONSTRAINT [DF_TableVersions_TableVersion]  DEFAULT (''),
		[UpdatedOn] [datetime] NOT NULL CONSTRAINT [DF_TableVersions_UpdatedOn]  DEFAULT (getutcdate())
	) ON [PRIMARY]
END

/**  Update Table Default Load Data **/
IF NOT EXISTS(SELECT Name FROM dbo.TableVersions Where Name = 'FrameworkVersion')
	INSERT INTO dbo.TableVersions (Name, Version) VALUES ('FrameworkVersion', '1.0.0')