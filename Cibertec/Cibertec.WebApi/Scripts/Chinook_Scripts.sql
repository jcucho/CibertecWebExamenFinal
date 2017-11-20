CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[Roles] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Email], [FirstName], [LastName], [Password], [Roles]) VALUES (1, N'juvega@gmail.com', N'Julio', N'Velarde', N'12345678', N'Admin')
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  StoredProcedure [dbo].[ValidateUser]    Script Date: 19/11/2017 12:38:24 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ValidateUser] 
	@email varchar(100),
	@password varchar(20)
AS
BEGIN
	SELECT [Email]
      ,[FirstName]
      ,[LastName]
      ,[Password]
      ,[Roles]
  FROM [dbo].[User]
  WHERE Email=@email AND [Password] = @password
END
GO

CREATE PROCEDURE [dbo].[CustomerPagedList] --1,30
@startRow int,
@endRow int
AS
BEGIN
SELECT [CustomerId]
      ,[FirstName]
      ,[LastName]
      ,isnull([Company],'') as [Company]
      ,isnull([Address],'') as [Address]
      ,isnull([City],'') as [City]
      ,isnull([State],'') as [State]
      ,isnull([Country],'') as [Country]
      ,isnull([PostalCode],'') as [PostalCode]
      ,isnull([Phone],'') as [Phone]
      ,isnull([Fax],'') as [Fax]
      ,isnull([Email],'') as [Email]
      ,[SupportRepId]
  FROM (
	SELECT ROW_NUMBER() OVER ( ORDER BY [CustomerId] ) AS RowNum,
	 [CustomerId]
      ,[FirstName]
      ,[LastName]
      ,[Company]
      ,[Address]
      ,[City]
      ,[State]
      ,[Country]
      ,[PostalCode]
      ,[Phone]
      ,[Fax]
      ,[Email]
      ,[SupportRepId]
	 FROM [dbo].[Customer]  
  ) AS RowConstrainedResult
WHERE RowNum >= @startRow
AND RowNum <= @endRow
ORDER BY RowNum
END