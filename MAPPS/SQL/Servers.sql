IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Servers]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Servers](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](255) NOT NULL,
		[ServerFunctionID] [int] NOT NULL,
		[ServerTypeID] [int] NOT NULL,
		[ServerStatusID] [int] NOT NULL,
		[ServerEnvironmentID] [int] NOT NULL,
		[ServerVersionID] [int] NOT NULL,
		[IPAddress] [nvarchar](50) NOT NULL,
		[PrimaryPOC] [nvarchar](255) NOT NULL,
		[AlternatePOC] [nvarchar](255) NOT NULL,
		[RestartOrder] [nvarchar](255) NOT NULL,
		[Memory] [nvarchar](50) NOT NULL,
		[CPU] [nvarchar](50) NOT NULL,
		[Purpose] [nvarchar](255) NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [nvarchar](150) NOT NULL,
		[ModifiedOn] [datetime] NOT NULL,
		[ModifiedBy] [nvarchar](150) NOT NULL,
	 CONSTRAINT [PK_Servers] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]


	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_ServerFunctionID]  DEFAULT ((1)) FOR [ServerFunctionID]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_ServerTypeID]  DEFAULT ((1)) FOR [ServerTypeID]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_ServerStatusID]  DEFAULT ((1)) FOR [ServerStatusID]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_EnvironmentID]  DEFAULT ((1)) FOR [ServerEnvironmentID]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_OSVersionID]  DEFAULT ((1)) FOR [ServerVersionID]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_IPAddress]  DEFAULT ('') FOR [IPAddress]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_PrimaryPOC]  DEFAULT ('') FOR [PrimaryPOC]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_AlternatePOC]  DEFAULT ('') FOR [AlternatePOC]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_RestartOrder]  DEFAULT ('') FOR [RestartOrder]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_Memory]  DEFAULT ('') FOR [Memory]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_CPU]  DEFAULT ((1)) FOR [CPU]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_Purpose]  DEFAULT ('') FOR [Purpose]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_CreatedBy]  DEFAULT (N'System Account') FOR [CreatedBy]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_ModifiedOn]  DEFAULT (getutcdate()) FOR [ModifiedOn]
	ALTER TABLE [dbo].[Servers] ADD  CONSTRAINT [DF_Servers_ModifiedBy]  DEFAULT (N'System Account') FOR [ModifiedBy]

	SET IDENTITY_INSERT [dbo].[ServerEnvironments] ON 

	INSERT [dbo].[ServerEnvironments] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Unspecified', N'Server environment unspecified', CAST(N'2018-10-17 05:58:54.820' AS DateTime), N'System Account', CAST(N'2018-10-17 05:58:54.820' AS DateTime), N'System Account')

	SET IDENTITY_INSERT [dbo].[ServerEnvironments] OFF

	SET IDENTITY_INSERT [dbo].[ServerFunctions] ON 

	INSERT [dbo].[ServerFunctions] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Unspecified', N'Server function unspecified', CAST(N'2018-10-17 05:56:50.530' AS DateTime), N'System Account', CAST(N'2018-10-17 05:56:50.530' AS DateTime), N'System Account')
	INSERT [dbo].[ServerFunctions] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (3, N'SharePoint 2013', N'SharePoint 2013', CAST(N'2018-10-17 05:57:16.760' AS DateTime), N'System Account', CAST(N'2018-10-17 05:57:16.760' AS DateTime), N'System Account')
	INSERT [dbo].[ServerFunctions] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (4, N'SharePoint 2016', N'SharePoint 2016', CAST(N'2018-10-17 05:57:31.027' AS DateTime), N'System Account', CAST(N'2018-10-17 05:57:31.027' AS DateTime), N'System Account')
	INSERT [dbo].[ServerFunctions] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (5, N'SQL Server 2012', N'SQL Server 2012', CAST(N'2018-10-17 05:57:46.017' AS DateTime), N'System Account', CAST(N'2018-10-17 05:57:46.017' AS DateTime), N'System Account')
	INSERT [dbo].[ServerFunctions] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (6, N'SQL Server 2016', N'SQL Server 2016', CAST(N'2018-10-17 05:58:04.063' AS DateTime), N'System Account', CAST(N'2018-10-17 05:58:04.063' AS DateTime), N'System Account')
	SET IDENTITY_INSERT [dbo].[ServerFunctions] OFF

	SET IDENTITY_INSERT [dbo].[Servers] ON 
	INSERT [dbo].[Servers] ([ID], [Name], [Description], [ServerFunctionID], [ServerTypeID], [ServerStatusID], [ServerEnvironmentID], [ServerVersionID], [IPAddress], [PrimaryPOC], [AlternatePOC], [RestartOrder], [Memory], [CPU], [Purpose], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'MEPCOM-A4-932', N'SP2013 [PROD] Cluster - Node 1', 5, 3, 6, 1, 2, N'111.111.111.111', N'Rykyto, Evelyn G CTR (US) ', N'Smith, Manetia F (Mo) CTR (US) ', N'1', N'16 GB', N'4 Cores', N'', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account')
	INSERT [dbo].[Servers] ([ID], [Name], [Description], [ServerFunctionID], [ServerTypeID], [ServerStatusID], [ServerEnvironmentID], [ServerVersionID], [IPAddress], [PrimaryPOC], [AlternatePOC], [RestartOrder], [Memory], [CPU], [Purpose], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, N'MEPCOM-A4-933', N'SP2013 [PROD] Cluster - Node 2', 5, 3, 6, 1, 2, N'111.111.111.111', N'Rykyto, Evelyn G CTR (US) ', N'Smith, Manetia F (Mo) CTR (US) ', N'1', N'16 GB', N'4 Cores', N'', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account')
	INSERT [dbo].[Servers] ([ID], [Name], [Description], [ServerFunctionID], [ServerTypeID], [ServerStatusID], [ServerEnvironmentID], [ServerVersionID], [IPAddress], [PrimaryPOC], [AlternatePOC], [RestartOrder], [Memory], [CPU], [Purpose], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (3, N'MEPCOM-A4-934', N'SP2013 [PROD] Cluster - Node 3', 5, 3, 6, 1, 2, N'111.111.111.111', N'Rykyto, Evelyn G CTR (US) ', N'Smith, Manetia F (Mo) CTR (US) ', N'1', N'16 GB', N'4 Cores', N'', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account')
	INSERT [dbo].[Servers] ([ID], [Name], [Description], [ServerFunctionID], [ServerTypeID], [ServerStatusID], [ServerEnvironmentID], [ServerVersionID], [IPAddress], [PrimaryPOC], [AlternatePOC], [RestartOrder], [Memory], [CPU], [Purpose], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (4, N'MEPCOM-C2-900', N'SP2013 [PROD] - WFE 1', 3, 4, 6, 1, 2, N'111.111.111.111', N'Smith, Manetia F (Mo) CTR (US) ', N'Rykyto, Evelyn G CTR (US) ', N'1', N'16 GB', N'4 Cores', N'', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account')
	INSERT [dbo].[Servers] ([ID], [Name], [Description], [ServerFunctionID], [ServerTypeID], [ServerStatusID], [ServerEnvironmentID], [ServerVersionID], [IPAddress], [PrimaryPOC], [AlternatePOC], [RestartOrder], [Memory], [CPU], [Purpose], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (5, N'MEPCOM-C2-901', N'SP2013 [PROD] - WFE 2', 3, 4, 6, 1, 2, N'111.111.111.111', N'Smith, Manetia F (Mo) CTR (US) ', N'Rykyto, Evelyn G CTR (US) ', N'1', N'16 GB', N'4 Cores', N'', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account')
	INSERT [dbo].[Servers] ([ID], [Name], [Description], [ServerFunctionID], [ServerTypeID], [ServerStatusID], [ServerEnvironmentID], [ServerVersionID], [IPAddress], [PrimaryPOC], [AlternatePOC], [RestartOrder], [Memory], [CPU], [Purpose], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (6, N'MEPCOM-C2-902', N'SP2013 [PROD]- App Server 1', 3, 4, 6, 1, 2, N'111.111.111.111', N'Smith, Manetia F (Mo) CTR (US) ', N'Rykyto, Evelyn G CTR (US) ', N'1', N'16 GB', N'4 Cores', N'', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account')
	INSERT [dbo].[Servers] ([ID], [Name], [Description], [ServerFunctionID], [ServerTypeID], [ServerStatusID], [ServerEnvironmentID], [ServerVersionID], [IPAddress], [PrimaryPOC], [AlternatePOC], [RestartOrder], [Memory], [CPU], [Purpose], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (7, N'MEPCOM-C2-903', N'SP2013 [PROD] - App Server 2', 3, 4, 6, 1, 2, N'111.111.111.111', N'Smith, Manetia F (Mo) CTR (US) ', N'Rykyto, Evelyn G CTR (US) ', N'1', N'16 GB', N'4 Cores', N'', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account')
	INSERT [dbo].[Servers] ([ID], [Name], [Description], [ServerFunctionID], [ServerTypeID], [ServerStatusID], [ServerEnvironmentID], [ServerVersionID], [IPAddress], [PrimaryPOC], [AlternatePOC], [RestartOrder], [Memory], [CPU], [Purpose], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (8, N'MEPCOM-C2-904', N'Office Web Apps', 3, 4, 6, 1, 2, N'111.111.111.111', N'Smith, Manetia F (Mo) CTR (US)', N'Rykyto, Evelyn G CTR (US)', N'1', N'16 GB', N'4 Cores', N'', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account', CAST(N'2019-03-12 06:02:13.610' AS DateTime), N'Stamm, Ken  ')
	INSERT [dbo].[Servers] ([ID], [Name], [Description], [ServerFunctionID], [ServerTypeID], [ServerStatusID], [ServerEnvironmentID], [ServerVersionID], [IPAddress], [PrimaryPOC], [AlternatePOC], [RestartOrder], [Memory], [CPU], [Purpose], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (9, N'MEPCOM-C2-905', N'AvePoint', 3, 4, 6, 1, 2, N'111.111.111.111', N'Smith, Manetia F (Mo) CTR (US) ', N'Rykyto, Evelyn G CTR (US) ', N'1', N'16 GB', N'4 Cores', N'', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account')
	INSERT [dbo].[Servers] ([ID], [Name], [Description], [ServerFunctionID], [ServerTypeID], [ServerStatusID], [ServerEnvironmentID], [ServerVersionID], [IPAddress], [PrimaryPOC], [AlternatePOC], [RestartOrder], [Memory], [CPU], [Purpose], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (10, N'MEPCOM-C2-906', N'SP2013 [TEST]', 3, 4, 7, 1, 2, N'111.111.111.111', N'Smith, Manetia F (Mo) CTR (US)', N'Rykyto, Evelyn G CTR (US)', N'1', N'16 GB', N'4 Cores', N'', CAST(N'2019-03-11 20:30:47.630' AS DateTime), N'System Account', CAST(N'2019-03-12 06:02:21.347' AS DateTime), N'Stamm, Ken  ')
	SET IDENTITY_INSERT [dbo].[Servers] OFF

	SET IDENTITY_INSERT [dbo].[ServerStatuses] ON 
	INSERT [dbo].[ServerStatuses] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Unspecified', N'Server status unspecified', CAST(N'2018-10-17 05:53:28.407' AS DateTime), N'System Account', CAST(N'2018-10-17 05:53:28.407' AS DateTime), N'System Account')
	INSERT [dbo].[ServerStatuses] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (3, N'Requested', N'New server provisioning request', CAST(N'2018-10-17 05:54:19.020' AS DateTime), N'System Account', CAST(N'2018-10-17 05:54:19.020' AS DateTime), N'System Account')
	INSERT [dbo].[ServerStatuses] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (4, N'Provisioning', N'Server being provisioned', CAST(N'2018-10-17 05:54:38.173' AS DateTime), N'System Account', CAST(N'2018-10-17 05:54:38.173' AS DateTime), N'System Account')
	INSERT [dbo].[ServerStatuses] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (5, N'Buildout', N'Server being configured for role', CAST(N'2018-10-17 05:55:17.330' AS DateTime), N'System Account', CAST(N'2018-10-17 05:55:17.330' AS DateTime), N'System Account')
	INSERT [dbo].[ServerStatuses] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (6, N'On-Line', N'Server online for use', CAST(N'2018-10-17 05:55:34.823' AS DateTime), N'System Account', CAST(N'2018-10-17 05:55:34.823' AS DateTime), N'System Account')
	INSERT [dbo].[ServerStatuses] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (7, N'Off-Line', N'Server offline', CAST(N'2018-10-17 05:55:44.900' AS DateTime), N'System Account', CAST(N'2018-10-17 05:55:44.900' AS DateTime), N'System Account')
	INSERT [dbo].[ServerStatuses] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (8, N'Decom', N'Server tagged for decommisioning', CAST(N'2018-10-17 05:56:08.333' AS DateTime), N'System Account', CAST(N'2018-10-17 05:56:08.333' AS DateTime), N'System Account')
	SET IDENTITY_INSERT [dbo].[ServerStatuses] OFF

	SET IDENTITY_INSERT [dbo].[ServerTypes] ON 
	INSERT [dbo].[ServerTypes] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Unspecified', N'Server type unspecified', CAST(N'2018-10-17 05:51:32.717' AS DateTime), N'System Account', CAST(N'2018-10-17 05:51:32.717' AS DateTime), N'System Account')
	INSERT [dbo].[ServerTypes] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, N'File Server', N'File Server', CAST(N'2018-10-17 05:51:45.127' AS DateTime), N'System Account', CAST(N'2018-10-17 05:51:45.127' AS DateTime), N'System Account')
	INSERT [dbo].[ServerTypes] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (3, N'Database Server', N'Database Server', CAST(N'2018-10-17 05:52:01.287' AS DateTime), N'System Account', CAST(N'2018-10-17 05:52:01.287' AS DateTime), N'System Account')
	INSERT [dbo].[ServerTypes] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (4, N'Portal Server', N'Portal Server', CAST(N'2018-10-17 05:52:22.440' AS DateTime), N'System Account', CAST(N'2018-10-17 05:52:22.440' AS DateTime), N'System Account')
	INSERT [dbo].[ServerTypes] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (5, N'Print Server', N'Print Server', CAST(N'2018-10-17 05:52:33.547' AS DateTime), N'System Account', CAST(N'2018-10-17 05:52:33.547' AS DateTime), N'System Account')
	INSERT [dbo].[ServerTypes] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (6, N'Virtual Host', N'Virtual Host', CAST(N'2018-10-17 05:52:48.200' AS DateTime), N'System Account', CAST(N'2018-10-17 05:52:48.200' AS DateTime), N'System Account')
	SET IDENTITY_INSERT [dbo].[ServerTypes] OFF

	SET IDENTITY_INSERT [dbo].[ServerVersions] ON 
	INSERT [dbo].[ServerVersions] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Unspecified', N'Server OS version unspecified', CAST(N'2018-10-17 05:49:52.660' AS DateTime), N'System Account', CAST(N'2018-10-17 05:49:52.660' AS DateTime), N'System Account')
	INSERT [dbo].[ServerVersions] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, N'Windows Server 2012 R2', N'Windows Server 2012 R2', CAST(N'2018-10-17 05:50:20.477' AS DateTime), N'System Account', CAST(N'2018-10-17 05:50:20.477' AS DateTime), N'System Account')
	INSERT [dbo].[ServerVersions] ([ID], [Name], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (3, N'Windows Server 2016', N'Windows Server 2016', CAST(N'2018-10-17 05:50:49.273' AS DateTime), N'System Account', CAST(N'2018-10-17 05:50:49.273' AS DateTime), N'System Account')
	SET IDENTITY_INSERT [dbo].[ServerVersions] OFF

	ALTER TABLE [dbo].[ServerPorts]  WITH CHECK ADD  CONSTRAINT [FK_ServerPorts_Servers] FOREIGN KEY([ServerID])
	REFERENCES [dbo].[Servers] ([ID])

	ALTER TABLE [dbo].[ServerPorts] CHECK CONSTRAINT [FK_ServerPorts_Servers]

	ALTER TABLE [dbo].[Servers]  WITH CHECK ADD  CONSTRAINT [FK_Servers_ServerEnvironments] FOREIGN KEY([ServerEnvironmentID])
	REFERENCES [dbo].[ServerEnvironments] ([ID])

	ALTER TABLE [dbo].[Servers] CHECK CONSTRAINT [FK_Servers_ServerEnvironments]

	ALTER TABLE [dbo].[Servers]  WITH CHECK ADD  CONSTRAINT [FK_Servers_ServerFunctions] FOREIGN KEY([ServerFunctionID])
	REFERENCES [dbo].[ServerFunctions] ([ID])

	ALTER TABLE [dbo].[Servers] CHECK CONSTRAINT [FK_Servers_ServerFunctions]

	ALTER TABLE [dbo].[Servers]  WITH CHECK ADD  CONSTRAINT [FK_Servers_ServerStatuses1] FOREIGN KEY([ServerStatusID])
	REFERENCES [dbo].[ServerStatuses] ([ID])

	ALTER TABLE [dbo].[Servers] CHECK CONSTRAINT [FK_Servers_ServerStatuses1]

	ALTER TABLE [dbo].[Servers]  WITH CHECK ADD  CONSTRAINT [FK_Servers_ServerTypes] FOREIGN KEY([ServerTypeID])
	REFERENCES [dbo].[ServerTypes] ([ID])

	ALTER TABLE [dbo].[Servers] CHECK CONSTRAINT [FK_Servers_ServerTypes]

	ALTER TABLE [dbo].[Servers]  WITH CHECK ADD  CONSTRAINT [FK_Servers_ServerVersions] FOREIGN KEY([ServerVersionID])
	REFERENCES [dbo].[ServerVersions] ([ID])

	ALTER TABLE [dbo].[Servers] CHECK CONSTRAINT [FK_Servers_ServerVersions]
END

/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.Servers'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)