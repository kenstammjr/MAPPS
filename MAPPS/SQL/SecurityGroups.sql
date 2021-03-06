IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SecurityGroups]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[SecurityGroups](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](150) NOT NULL,
		[Description] [nvarchar](255) NOT NULL,
		[RoleID] [int] NOT NULL CONSTRAINT [DF_SecurityGroups_RoleID]  DEFAULT ((0)),
		[DisplayIndex] [int] NOT NULL CONSTRAINT [DF_SecurityGroups_DisplayIndex]  DEFAULT ((99)),
		[ParentID] [int] NOT NULL CONSTRAINT [DF_SecurityGroups_ParentID]  DEFAULT ((99)),
	 CONSTRAINT [PK_SecurityGroups] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]


	ALTER TABLE [dbo].[SecurityGroups]  WITH CHECK ADD  CONSTRAINT [FK_SecurityGroups_Roles] FOREIGN KEY([RoleID])
	REFERENCES [dbo].[Roles] ([ID])
	ON DELETE CASCADE

	ALTER TABLE [dbo].[SecurityGroups] CHECK CONSTRAINT [FK_SecurityGroups_Roles]

	SET IDENTITY_INSERT [dbo].[SecurityGroups] ON
	INSERT [dbo].[SecurityGroups] ([ID], [Name], [Description], [RoleID], [DisplayIndex], [ParentID]) VALUES (1, N'Application Administrators', N'Members have full administrative control of entire application', 1, 1, 0)
	INSERT [dbo].[SecurityGroups] ([ID], [Name], [Description], [RoleID], [DisplayIndex], [ParentID]) VALUES (2, N'Application Managers', N'Managers may manage content anywhere in the application', 2, 2, 1)
	INSERT [dbo].[SecurityGroups] ([ID], [Name], [Description], [RoleID], [DisplayIndex], [ParentID]) VALUES (3, N'Application Contributors', N'Contributors may add content anywhere in the application', 3, 3, 2)
	INSERT [dbo].[SecurityGroups] ([ID], [Name], [Description], [RoleID], [DisplayIndex], [ParentID]) VALUES (9, N'Personnel Administrators', N'Members have full administrative control of personnel data', 6, 4, 1)
	INSERT [dbo].[SecurityGroups] ([ID], [Name], [Description], [RoleID], [DisplayIndex], [ParentID]) VALUES (10, N'Personnel Viewers', N'Members have full read access of all personnel data', 7, 5, 9)
	SET IDENTITY_INSERT [dbo].[SecurityGroups] OFF
END


/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.SecurityGroups'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)