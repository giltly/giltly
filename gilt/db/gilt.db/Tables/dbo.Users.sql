CREATE TABLE [Users]
( 
	[Id]	    		INT IDENTITY(1,1) NOT NULL,  
	[Guid]              UNIQUEIDENTIFIER NOT NULL,
	[CreatedOn]			DATETIME DEFAULT GETDATE(),
	[Password]			VARCHAR (128) NOT NULL,
	[Email]				VARCHAR (512) NULL,
	[FirstName]			VARCHAR (512) NULL,
	[LastName]			VARCHAR (512) NULL,
	[ActiveSearch]	    INT NULL,
	CONSTRAINT [FK_Users_Search]	FOREIGN KEY ([ActiveSearch])	REFERENCES [Search]([Id]),	
	CONSTRAINT [PK_Users_Id] PRIMARY KEY ([Id]),
	CONSTRAINT [UC_Email] UNIQUE ([Email])
)
GO
CREATE INDEX [IX_Users_Id] ON [Users]([Id])

GO
CREATE INDEX [IX_Users_Guid] ON [Users]([Guid])

GO
CREATE INDEX [IX_Users_CreatedOn] ON [Users]([CreatedOn])

GO
CREATE INDEX [IX_Users_Email] ON [Users]([Email])

GO
CREATE INDEX [IX_Users_FirstName] ON [Users]([FirstName])

GO
CREATE INDEX [IX_Users_LastName] ON [Users]([LastName])

GO
CREATE INDEX [IX_Users_ActiveSearch] ON [Users]([ActiveSearch])