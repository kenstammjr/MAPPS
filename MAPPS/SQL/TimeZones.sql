IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TimeZones]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[TimeZones](
		[ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
		[WebID] [uniqueidentifier] NOT NULL,
		[ZoneID] [nvarchar](50) NOT NULL,
		[DisplayName] [nvarchar](5) NOT NULL,
		[DisplayIndex] [int] NOT NULL,
		[ShowTimeZoneLetter] [bit] NOT NULL,
		[CreatedBy] [nvarchar](150) NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[ModifiedBy] [nvarchar](150) NOT NULL,
		[ModifiedOn] [datetime] NOT NULL,
	 CONSTRAINT [PK_TimeZones] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[TimeZones] ADD  CONSTRAINT [DF_TimeZones_DisplayIndex]  DEFAULT ((0)) FOR [DisplayIndex]
	ALTER TABLE [dbo].[TimeZones] ADD  CONSTRAINT [DF_TimeZones_ShowMilitaryTimeZone]  DEFAULT ((1)) FOR [ShowTimeZoneLetter]
	ALTER TABLE [dbo].[TimeZones] ADD  CONSTRAINT [DF_TimeZones_CreatedBy]  DEFAULT (N'System Account') FOR [CreatedBy]
	ALTER TABLE [dbo].[TimeZones] ADD  CONSTRAINT [DF_TimeZones_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
	ALTER TABLE [dbo].[TimeZones] ADD  CONSTRAINT [DF_TimeZones_ModifiedBy]  DEFAULT (N'System Account') FOR [ModifiedBy]
	ALTER TABLE [dbo].[TimeZones] ADD  CONSTRAINT [DF_TimeZones_ModifiedOn]  DEFAULT (getutcdate()) FOR [ModifiedOn]
END

-- update table version
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.TimeZones'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)