CREATE TABLE [CountryCodes]
(
	[Id] INT IDENTITY(1,1)   NOT NULL,  
	[ISO2] VARCHAR(2) NOT NULL,
	[ISO3] VARCHAR(3),
	[Name] VARCHAR(80) NOT NULL,
	[PrintableName] VARCHAR(80) NOT NULL,
	[Code] INT 
	CONSTRAINT [PK_CountryCodes_Id]     PRIMARY KEY ([ISO2])
)
GO
CREATE INDEX [IX_CountryCodes_Id] ON [CountryCodes] ([Id])

GO
CREATE INDEX [IX_CountryCodes_ISO2] ON [CountryCodes] ([ISO2])
