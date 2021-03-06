IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SecurityGroupMemberships]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[SecurityGroupMemberships](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[SecurityGroupID] [int] NOT NULL,
		[UserID] [int] NOT NULL,
		[CreatedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_SecurityGroupMemberships_CreatedBy]  DEFAULT (N'System Account'),
		[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_SecurityGroupMemberships_CreatedOn]  DEFAULT (getutcdate()),
		[ModifiedBy] [nvarchar](150) NOT NULL CONSTRAINT [DF_SecurityGroupMemberships_ModifiedBy]  DEFAULT (N'System Account'),
		[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_SecurityGroupMemberships_ModifiedOn]  DEFAULT (getutcdate()),
	 CONSTRAINT [PK_SecurityGroupMemberships] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[SecurityGroupMemberships]  WITH CHECK ADD  CONSTRAINT [FK_SecurityGroupMemberships_SecurityGroups] FOREIGN KEY([SecurityGroupID])
	REFERENCES [dbo].[SecurityGroups] ([ID])
	ON DELETE CASCADE

	ALTER TABLE [dbo].[SecurityGroupMemberships] CHECK CONSTRAINT [FK_SecurityGroupMemberships_SecurityGroups]
END

/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.SecurityGroupMemberships'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)