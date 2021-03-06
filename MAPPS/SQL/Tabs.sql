IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tabs]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Tabs](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](150) NOT NULL CONSTRAINT [DF_Tabs_Description]  DEFAULT (''),
		[URL] [nvarchar](255) NOT NULL CONSTRAINT [DF_Tabs_URL]  DEFAULT (''),
		[DisplayIndex] [int] NOT NULL CONSTRAINT [DF_Tabs_DisplayIndex]  DEFAULT ((99)),
		[IsActive] [bit] NOT NULL CONSTRAINT [DF_Tabs_IsActive]  DEFAULT ((1)),
		[AdminOnly] [bit] NOT NULL CONSTRAINT [DF_Tabs_AdminOnly]  DEFAULT ((0)),
		[CreatedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_Tabs_CreatedBy]  DEFAULT ('System Account'),
		[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_Tabs_CreatedOn]  DEFAULT (getutcdate()),
		[ModifiedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_Tabs_ModifiedBy]  DEFAULT ('System Account'),
		[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_Tabs_ModifiedOn]  DEFAULT (getutcdate()),
	 CONSTRAINT [PK_Tabs] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET IDENTITY_INSERT [dbo].[Tabs] ON 
	INSERT [dbo].[Tabs] ([ID], [Name], [Description], [URL], [DisplayIndex], [IsActive], [AdminOnly], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1, N'SIS Home', N'MAPPS SIS Home Page', N'~/_layouts/nga/pages/home.aspx', 10, 1, 0, N'System Account', CAST(N'2015-07-21 20:53:50.897' AS DateTime), N'System Account', CAST(N'2015-07-21 20:53:50.897' AS DateTime))
	INSERT [dbo].[Tabs] ([ID], [Name], [Description], [URL], [DisplayIndex], [IsActive], [AdminOnly], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (2, N'Courses', N'Course Catalog', N'~/_layouts/nga/pages/courses.aspx', 15, 1, 0, N'System Account', CAST(N'2015-07-29 15:12:40.700' AS DateTime), N'Stamm, Kenneth E CTR SOCOM HQ SOCOM', CAST(N'2015-08-04 15:57:06.403' AS DateTime))
	INSERT [dbo].[Tabs] ([ID], [Name], [Description], [URL], [DisplayIndex], [IsActive], [AdminOnly], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (3, N'Students', N'Student Catalog', N'~/_layouts/nga/pages/students.aspx', 20, 1, 0, N'System Account', CAST(N'2015-08-04 15:56:53.783' AS DateTime), N'Stamm, Kenneth E CTR SOCOM HQ SOCOM', CAST(N'2015-08-04 15:56:53.783' AS DateTime))
	INSERT [dbo].[Tabs] ([ID], [Name], [Description], [URL], [DisplayIndex], [IsActive], [AdminOnly], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (4, N'Administration', N'Administrators Dashboard', N'~/_layouts/nga/pages/administration.aspx', 30, 1, 1, N'System Account', CAST(N'2015-10-19 19:06:20.760' AS DateTime), N'System Account', CAST(N'2015-10-19 19:06:20.760' AS DateTime))
	SET IDENTITY_INSERT [dbo].[Tabs] OFF

END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ParentID' AND Object_ID = Object_ID(N'[dbo].[Tabs]'))
BEGIN
	ALTER TABLE [dbo].[Tabs] ADD [ParentID] [int] NOT NULL CONSTRAINT [DF_Tabs_ParentID] DEFAULT ((0))
END
/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.Tabs'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)