CREATE TABLE [Schema] 
( 
	[Id]				NUMERIC(10,0) NOT NULL,
    [ParseTime]			DATETIME      NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [PK_Schema_Id] PRIMARY KEY ([Id])
)
GO
CREATE INDEX [IX_Schema_Id] ON [Schema] ([Id])