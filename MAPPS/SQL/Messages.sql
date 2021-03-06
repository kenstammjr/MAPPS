IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Messages]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Messages](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Number] [int] NOT NULL,
		[Message] [ntext] NOT NULL,
	 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	SET IDENTITY_INSERT [dbo].[Messages] ON 
	INSERT [dbo].[Messages] ([ID], [Number], [Message]) VALUES (1, 101, N'<H3>Security Group Membership Required for Access.</H3><li>To access the selected page, you must be a member of the security group that controls access.</li><li>If you require access, please submit your request to the help desk with justification along with a reference to the site in question.</li><br><br><li><i>Application Access Error 101</i></li>')
	INSERT [dbo].[Messages] ([ID], [Number], [Message]) VALUES (2, 102, N'<H3>Administrator Group Membership required for access.</H3><li>To access the selected page, you must be a member of the Administrators Security Group.</li><li>If you require access, please submit your request to the help desk with justification.</li><br><br><li><i>Application Access Error 102</i></li>')
	INSERT [dbo].[Messages] ([ID], [Number], [Message]) VALUES (3, 103, N'<H3>Manger Group Membership required for access.</H3><li>To access the selected page, you must be a member of the Managers Security Group.</li><li>If you require access, please submit your request to the help desk with justification.</li><br><br><li><i>Application Access Error 103</i></li>')
	INSERT [dbo].[Messages] ([ID], [Number], [Message]) VALUES (4, 104, N'<H3>Everyone Group Membership required for access.</H3><li>To access the selected page, you must be a member of theEveryone Security Group.</li><li>If you require access, please submit your request to the help desk with justification.</li><br><br><li><i>Application Access Error 104</i></li>')
	INSERT [dbo].[Messages] ([ID], [Number], [Message]) VALUES (5, 105, N'<H3>HR Administrator Group Membership required for access.</H3><li>To access the selected page, you must be a member of the HR Administrators Security Group.</li><li>If you require access, please submit your request to the help desk with justification.</li><br><br><li><i>Application Access Error 105</i></li>')
	SET IDENTITY_INSERT [dbo].[Messages] OFF

END

/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.Messages'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)
