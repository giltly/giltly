CREATE TABLE [ReferenceSystem]
(
	[Id]		NUMERIC(10,0) IDENTITY(1,1) NOT NULL,
	[Name]		VARCHAR(20),
	[URL]		VARCHAR(512) NULL, 
    CONSTRAINT [PK_ReferenceSystem_Id] PRIMARY KEY ([Id])
)
GO
CREATE INDEX [IX_ReferenceSystem_Id] ON [ReferenceSystem] ([Id])