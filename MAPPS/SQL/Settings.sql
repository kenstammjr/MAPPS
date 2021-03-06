IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Settings]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Settings](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Key] [nvarchar](50) NOT NULL,
		[Value] [nvarchar](255) NOT NULL,
		[Salt] [nvarchar](255) NOT NULL CONSTRAINT [DF_Settings_Salt]  DEFAULT (N' '),
		[Description] [nvarchar](255) NOT NULL CONSTRAINT [DF_Settings_Description]  DEFAULT (N' '),
		[IsPassword] [bit] NOT NULL CONSTRAINT [DF_Settings_IsPassword]  DEFAULT ((0)),
		[IsMultiline] [bit] NOT NULL CONSTRAINT [DF_Settings_IsMultiline]  DEFAULT ((0)),
		[ModifiedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_Settings_ModifiedBy]  DEFAULT (N'System Account'),
		[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_Settings_ModifiedOn]  DEFAULT (getutcdate()),
	 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET IDENTITY_INSERT [dbo].[Settings] ON 
	INSERT [dbo].[Settings] ([ID], [Key], [Value], [Salt], [Description], [IsPassword], [IsMultiline], [ModifiedBy], [ModifiedOn]) VALUES (17, N'TimeJobsEnabled', N'VaiM/fJHYik=', N'iFMvNyMIUm6Atw3E3RFGnRZ2LhoI4BGltCMklmyGKg+FlApiIQ34yb9f3FL64MmquRYIVjn0DH7cXxx/2x34zlssOMgBCNp5PSnBjzNgXFCVgLf43jwMseujO5KPsDStwCEetRm3gGw0qjXzYV1NOA==', N'True/False value - Determines if the timer jobs will be run', 0, 0, N'Stamm, Ken  ', CAST(N'2019-03-12 16:31:48.097' AS DateTime))
	INSERT [dbo].[Settings] ([ID], [Key], [Value], [Salt], [Description], [IsPassword], [IsMultiline], [ModifiedBy], [ModifiedOn]) VALUES (18, N'MaxRecords', N'MoX7TRnirZo=', N'lyvvbFTBvOgmC7UJynu9lTuY4oWlYxNMu78koESJ8KMDlvxKj4N0qKMOU3CGfGbCAzWN6FGQsQkQtnGGKFekiSLfD3cWo2x8hb6te2I1aHCzJBNwxriugdukDAYMjlrF3Pu3buY68819Rr5ecx6VttusX3fxxf4v', N'Maximum records returned from any single set', 0, 0, N'Stamm, Ken  ', CAST(N'2018-10-17 14:36:59.307' AS DateTime))
	INSERT [dbo].[Settings] ([ID], [Key], [Value], [Salt], [Description], [IsPassword], [IsMultiline], [ModifiedBy], [ModifiedOn]) VALUES (19, N'AdminAlerts', N'f4i5O9Aj89MLPbaEZgu0Ba2cr7Q57j4O', N'qxsGi8s13qplHPMQvpMDW39VHt8aL3YJO/umUyshob6VVfa2w+NBqdLmVPhjRAlU14U4syx3rPOPS5RW+k5nsBc7/GZvrDbl3cr3YOH8efXMQsGHV1vmdFt51KdQl9kFttCGWn5tBDLbU5GL+YW5bg==', N'A semi colon delimited string of email addresses to be notified of admin events', 0, 0, N'Stamm, Ken  ', CAST(N'2018-10-17 14:38:15.273' AS DateTime))
	INSERT [dbo].[Settings] ([ID], [Key], [Value], [Salt], [Description], [IsPassword], [IsMultiline], [ModifiedBy], [ModifiedOn]) VALUES (20, N'CharactersToDisplay', N'AUltlqSWIOs=', N'4sxRv9LppfbIcLRylCAMZn5KJWKk+qhcHEj3ZAMx8gHnBwBb0d6s4f1BuJFDvgVxYevWH78PZXlRrwzpmPpG4jn8ymct23CtvU6gY5a8EBYJ0pDHBhVcg79G7GHU8VQOQuffbFdT1lluFJE8aDdyCA==', N'The maximum number of characters to display before truncating', 0, 0, N'Stamm, Ken  ', CAST(N'2018-10-17 14:39:01.380' AS DateTime))
	INSERT [dbo].[Settings] ([ID], [Key], [Value], [Salt], [Description], [IsPassword], [IsMultiline], [ModifiedBy], [ModifiedOn]) VALUES (21, N'DefaultPageSizeForListView', N'kuCh80TGtWo=', N'zPyUeIC3KeEl5e9gKujpZnFc4LS1mPphjVvvh7miGWpkmpBe7/gGwhEj+0MlCzlRfchjZQUi5KhUFcJq1ioBLpMF0P935LvNalnvrf301iBRZ2DCmpsEA6RMHRQebPNFewAUEFcklDuFWVaMvaRtmg==', N'Number of rows to display by default before breaking list view grid into pages', 0, 0, N'Stamm, Ken  ', CAST(N'2019-03-12 03:57:01.410' AS DateTime))
	INSERT [dbo].[Settings] ([ID], [Key], [Value], [Salt], [Description], [IsPassword], [IsMultiline], [ModifiedBy], [ModifiedOn]) VALUES (22, N'DefaultPageSizeForWebPart', N'0zbii78bthw=', N'AbPFRYPf9SYpJxk5EIQUPGeLH3BrrCK7ifbVCDGMDCeZXrC6r+Y9VUcXaz7e0mXmUsNwoNiondbGEPYYHNMI6lX0nHTbuzo9R8eXpglYpRU93S1l3++4DgBvmINkEefiFH2pAD64D/nu51u2/WKRnQ==', N'Number of rows to display by default before breaking web part grid into pages', 0, 0, N'Stamm, Ken  ', CAST(N'2018-10-17 14:40:20.670' AS DateTime))
	SET IDENTITY_INSERT [dbo].[Settings] OFF
END

/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.Settings'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)