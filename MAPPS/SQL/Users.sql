IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Users](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[ClearanceTypeID] [int] NOT NULL CONSTRAINT [DF_UserProfiles_ClearanceTypeID]  DEFAULT ((1)),
		[OrganizationID] [int] NOT NULL CONSTRAINT [DF_UserProfiles_OrganizationID]  DEFAULT ((1)),
		[PayGradeID] [int] NOT NULL CONSTRAINT [DF_UserProfiles_PayGradeID]  DEFAULT ((1)),
		[PersonnelCategoryID] [int] NOT NULL CONSTRAINT [DF_UserProfiles_PersonnelCategoryID]  DEFAULT ((1)),
		[UserName] [nvarchar](400) NOT NULL CONSTRAINT [DF_Users_UserName]  DEFAULT (''),
		[EDIPI] [nvarchar](50) NOT NULL CONSTRAINT [DF_Users_EmployeeID]  DEFAULT (''),
		[ADObjectGuid] [nvarchar](50) NOT NULL CONSTRAINT [DF_Users_ObjectGuid]  DEFAULT (''),
		[SPObjectGuid] [nvarchar](50) NOT NULL CONSTRAINT [DF_Users_SPObjectGuid]  DEFAULT (''),
		[UserProfileRecordID] [int] NOT NULL CONSTRAINT [DF_Users_UserProfileRecordID]  DEFAULT ((0)),
		[LastName] [nvarchar](150) NOT NULL CONSTRAINT [DF_UserProfiles_LastName]  DEFAULT (''),
		[FirstName] [nvarchar](150) NOT NULL CONSTRAINT [DF_UserProfiles_FirstName]  DEFAULT (''),
		[MiddleInitial] [nvarchar](50) NOT NULL CONSTRAINT [DF_UserProfiles_MiddleInitial]  DEFAULT (''),
		[GenerationalQualifier] [nvarchar](50) NOT NULL CONSTRAINT [DF_UserProfiles_GenerationalQualifier]  DEFAULT (''),
		[PreferredName] [nvarchar](256) NOT NULL CONSTRAINT [DF_UserProfiles_PreferredName]  DEFAULT (''),
		[DutyTitle] [nvarchar](255) NOT NULL CONSTRAINT [DF_Users_DutyTitle]  DEFAULT (''),
		[SeniorStaff] [bit] NOT NULL CONSTRAINT [DF_Users_SeniorStaff]  DEFAULT ((0)),
		[Salt] [nvarchar](256) NOT NULL CONSTRAINT [DF_UserProfiles_Salt]  DEFAULT (''),
		[CreatedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_UserProfiles_CreatedBy]  DEFAULT (N'System Account'),
		[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_UserProfiles_CreatedOn]  DEFAULT (getutcdate()),
		[ModifiedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_UserProfiles_ModifiedBy]  DEFAULT (N'System Account'),
		[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_UserProfiles_ModifiedOn]  DEFAULT (getutcdate()),
	 CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_ClearanceTypes] FOREIGN KEY([ClearanceTypeID])
	REFERENCES [dbo].[ClearanceTypes] ([ID])

	ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_ClearanceTypes]

	ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Organizations] FOREIGN KEY([OrganizationID])
	REFERENCES [dbo].[Organizations] ([ID])

	ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Organizations]

	ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_PayGrades] FOREIGN KEY([PayGradeID])
	REFERENCES [dbo].[PayGrades] ([ID])

	ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_PayGrades]

	ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_PersonnelCategories] FOREIGN KEY([PersonnelCategoryID])
	REFERENCES [dbo].[PersonnelCategories] ([ID])

	ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_PersonnelCategories]
END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'SeniorStaff' AND Object_ID = Object_ID(N'[dbo].[Users]'))
BEGIN
	ALTER TABLE [dbo].[Users] ADD [SeniorStaff] [bit] NOT NULL CONSTRAINT [DF_Users_SeniorStaff]  DEFAULT ((0))
END


/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.Users'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)


