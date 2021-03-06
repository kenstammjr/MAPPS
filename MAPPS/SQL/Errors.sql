IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Errors]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Errors](
		[ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
		[DateOccurred] [datetime] NOT NULL,
		[Class] [varchar](255) NOT NULL,
		[Method] [varchar](255) NOT NULL,
		[RecordID] [int] NOT NULL,
		[UserName] [nvarchar](50) NOT NULL,
		[UserMachineIP] [nvarchar](50) NOT NULL,
		[ServerName] [nvarchar](50) NOT NULL,
		[ExceptionMessage] [varchar](1000) NOT NULL,
		[ExceptionType] [varchar](1000) NOT NULL,
		[ExceptionSource] [varchar](1000) NOT NULL,
		[ExceptionStackTrace] [varchar](8000) NOT NULL,
		[Comment] [nvarchar](200) NOT NULL,
	 CONSTRAINT [PK_Errors] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET ANSI_PADDING OFF
	ALTER TABLE [dbo].[Errors] ADD  CONSTRAINT [DF_Errors_DateOccurred]  DEFAULT (getdate()) FOR [DateOccurred]
END

/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.Errors'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn = getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)
