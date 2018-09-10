﻿CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_Id PRIMARY KEY,
	[Code] VARCHAR(10) NOT NULL CONSTRAINT UC_Code UNIQUE,
	[Name] VARCHAR(256) NOT NULL,
	[Photo] VARBINARY(MAX) NULL,
	[Price] MONEY NULL CONSTRAINT CK_Price CHECK (Price > 0),
	[LastUpdated] TIMESTAMP
)
