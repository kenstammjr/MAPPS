IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Roles](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Role] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](150) NOT NULL CONSTRAINT [DF_Roles_Description]  DEFAULT (N' '),
		[DisplayIndex] [int] NOT NULL CONSTRAINT [DF_Roles_DisplayIndex]  DEFAULT ((99)),
		[ParentRoleID] [int] NOT NULL,
	 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]	

	SET IDENTITY_INSERT [dbo].[Roles] ON 
	INSERT [dbo].[Roles] ([ID], [Role], [Description], [DisplayIndex], [ParentRoleID]) VALUES (1, N'Administrator', N'Full Control', 1, 0)
	INSERT [dbo].[Roles] ([ID], [Role], [Description], [DisplayIndex], [ParentRoleID]) VALUES (2, N'Manager', N'Full control of security, reference data, and content', 2, 1)
	INSERT [dbo].[Roles] ([ID], [Role], [Description], [DisplayIndex], [ParentRoleID]) VALUES (3, N'Contributor', N'Contribute Content', 3, 2)
	INSERT [dbo].[Roles] ([ID], [Role], [Description], [DisplayIndex], [ParentRoleID]) VALUES (6, N'UserAdmin', N'Full control of user data', 6, 1)
	INSERT [dbo].[Roles] ([ID], [Role], [Description], [DisplayIndex], [ParentRoleID]) VALUES (7, N'UserViewer', N'Full read access to all user data', 7, 6)
	SET IDENTITY_INSERT [dbo].[Roles] OFF
END

/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.Roles'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)
