CREATE TABLE [Reference]
(
	[Id]				NUMERIC(10,0) IDENTITY(1,1) NOT NULL,
	[SystemId]			NUMERIC(10,0)               NOT NULL,
	[Tag]				VARCHAR(8000)               NOT NULL,
	CONSTRAINT [PK_Reference_Id] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Reference_ReferenceSystem]	FOREIGN KEY ([SystemId]) REFERENCES [ReferenceSystem]([Id]),
)
GO
CREATE INDEX [IX_Reference_Id] ON [Reference] ([Id])

GO
CREATE INDEX [IX_Reference_SystemId] ON [Reference] ([SystemId])
