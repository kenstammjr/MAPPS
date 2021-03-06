IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Applications]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Applications](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](512) NOT NULL,
		[DisplayIndex] [int] NOT NULL CONSTRAINT [DF_Applications_DisplayIndex]  DEFAULT ((1)),
		[IsActive] [bit] NOT NULL CONSTRAINT [DF_Applications_IsActive]  DEFAULT ((1)),
		[IsProtected] [bit] NOT NULL CONSTRAINT [DF_Applications_IsProtected]  DEFAULT ((0)),
		[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_Applications_CreatedOn]  DEFAULT (getutcdate()),
		[CreatedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_Applications_CreatedBy]  DEFAULT (N'System Account'),
		[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_Applications_ModifiedOn]  DEFAULT (getutcdate()),
		[ModifiedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_Applications_ModifiedBy]  DEFAULT (N'System Account'),
	 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET IDENTITY_INSERT [dbo].[Applications] ON 
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'e-Security', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, N'e-Orders', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (3, N'e-Records', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (4, N'FBP', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (5, N'Expect-Onw', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (6, N'eSOA', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (7, N'OSB', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (8, N'CTS/CTV', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (9, N'ABIS', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (10, N'Documentum', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (11, N'JPS', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (12, N'Meal Check', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (13, N'USMIRS', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (14, N'BIR Web', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (15, N'Testing 2000', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (16, N'EFCS', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (17, N'DLAB', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (18, N'Schools 2K1', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (19, N'WinFAT', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (20, N'WinTIP', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (21, N'WinCAT', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (22, N'MOC Tools', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (23, N'UBIS', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (24, N'EPTS', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	INSERT [dbo].[Applications] ([ID], [Name], [Description], [DisplayIndex], [IsActive], [IsProtected], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (25, N'iCAT', N' ', 1, 1, 0, CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account', CAST(N'2018-10-19 15:45:29.673' AS DateTime), N'System Account')
	SET IDENTITY_INSERT [dbo].[Applications] OFF


END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'DisplayIndex' AND Object_ID = Object_ID(N'[dbo].[Applications]'))
BEGIN
	ALTER TABLE [dbo].[Applications] ADD [DisplayIndex] [int] NOT NULL CONSTRAINT [DF_Applications_DisplayIndex]  DEFAULT ((1))
END

/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.Applications'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)