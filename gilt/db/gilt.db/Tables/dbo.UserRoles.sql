CREATE TABLE [UserRoles]
(    
	[UserId]	INT NOT NULL,
	[RoleId]	INT NOT NULL,
	CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([UserId],[RoleId]),
	CONSTRAINT [UQ_UserRolesReversePK] UNIQUE ([RoleId], [UserId]),
	CONSTRAINT [FK_UserRoles_User]	FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
	CONSTRAINT [FK_UserRoles_Role]	FOREIGN KEY ([RoleId]) REFERENCES [Roles]([Id])
)
GO
CREATE INDEX [IX_UserRoles_UserId_RoleId] ON [UserRoles] ([UserId],[RoleId])
