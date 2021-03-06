IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServerAddresses]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[ServerAddresses](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[ServerID] [int] NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](350) NOT NULL,
		[CreatedBy] [nvarchar](150) NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[ModifiedBy] [nvarchar](150) NOT NULL,
		[ModifiedOn] [datetime] NOT NULL,
	 CONSTRAINT [PK_ServerAddresses] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[ServerAddresses] ADD  CONSTRAINT [DF_ServerAddresses_Description]  DEFAULT ('') FOR [Description]
	ALTER TABLE [dbo].[ServerAddresses] ADD  CONSTRAINT [DF_ServerAddresses_CreatedBy]  DEFAULT (N'System Account') FOR [CreatedBy]
	ALTER TABLE [dbo].[ServerAddresses] ADD  CONSTRAINT [DF_ServerAddresses_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
	ALTER TABLE [dbo].[ServerAddresses] ADD  CONSTRAINT [DF_ServerAddresses_ModifiedBy]  DEFAULT (N'System Account') FOR [ModifiedBy]
	ALTER TABLE [dbo].[ServerAddresses] ADD  CONSTRAINT [DF_ServerAddresses_ModifiedOn]  DEFAULT (getutcdate()) FOR [ModifiedOn]
	ALTER TABLE [dbo].[ServerAddresses]  WITH CHECK ADD  CONSTRAINT [FK_ServerAddresses_Servers] FOREIGN KEY([ServerID])
	REFERENCES [dbo].[Servers] ([ID])
	ON DELETE CASCADE

	ALTER TABLE [dbo].[ServerAddresses] CHECK CONSTRAINT [FK_ServerAddresses_Servers]
END

/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.ServerAddresses'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)