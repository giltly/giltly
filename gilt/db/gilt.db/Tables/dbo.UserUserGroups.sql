CREATE TABLE [UserUserGroups]
(    
	[UserId]		INT NOT NULL,
	[UserGroupId]	INT NOT NULL,
	CONSTRAINT [PK_UserUserGroups] PRIMARY KEY CLUSTERED ([UserId],[UserGroupId]),
	CONSTRAINT [UQ_UserUserGroupReversePK] UNIQUE ([UserGroupId], [UserId]),
	CONSTRAINT [FK_UserUserGroups_User]	FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
	CONSTRAINT [FK_UserUserGroups_UserGroups]	FOREIGN KEY ([UserGroupId]) REFERENCES [UserGroups]([Id])
)
GO
CREATE INDEX [IX_UserUserGroups_UserId_RoleId] ON [UserUserGroups] ([UserId],[UserGroupId])
