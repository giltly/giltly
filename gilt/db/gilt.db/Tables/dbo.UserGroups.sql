CREATE TABLE [UserGroups]
(
    [Id]				  INT IDENTITY(1,1)   NOT NULL,  
	[Name]				  VARCHAR(255),
	[Description]		  VARCHAR(512),
	CONSTRAINT [PK_UserGroups_Id] PRIMARY KEY ([Id]),
)
GO
CREATE INDEX [IX_UserGroups_Id] ON [UserGroups] ([Id])

GO
CREATE INDEX [IX_UserGroups_Name] ON [UserGroups] ([Name])