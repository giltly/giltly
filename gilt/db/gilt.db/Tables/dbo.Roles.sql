CREATE TABLE [Roles]
(
    [Id]				  INT IDENTITY(1,1)   NOT NULL,  
	[Name]				  VARCHAR(255),
	[Description]		  VARCHAR(512),
	CONSTRAINT [PK_Roles_Id] PRIMARY KEY ([Id]),
)
GO
CREATE INDEX [IX_Roles_Id] ON [Roles] ([Id])

GO
CREATE INDEX [IX_Roles_Name] ON [Roles] ([Name])