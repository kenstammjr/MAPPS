IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transactions]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Transactions](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Category] [nvarchar](50) NOT NULL,
		[Type] [nvarchar](50) NOT NULL,
		[Action] [nvarchar](max) NOT NULL,
		[ResourceID] [nvarchar](50) NOT NULL,
		[CreatedBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_Transactions_CreatedBy]  DEFAULT (N'System Account'),
		[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_Transactions_CreatedOn]  DEFAULT (getutcdate()),
	 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

/**  Update Table Version **/
DECLARE @Name NVARCHAR(100)
DECLARE @Version NVARCHAR(50)

SET @Name = 'dbo.Transactions'
SET @Version = '1.0.0'

IF EXISTS (SELECT Name FROM dbo.TableVersions  WHERE Name = @Name)
	UPDATE dbo.TableVersions
	   SET Version = @Version,
		   UpdatedOn= getutcdate()
     WHERE Name = @Name
ELSE
	INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)