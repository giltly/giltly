CREATE TABLE [LogEntry]
(
	[Id]			INT IDENTITY(1,1)   NOT NULL,  
	[FileName]		varchar(512)		NOT NULL,
	[CreatedOn]		datetime			NOT NULL,
	[ModifiedOn]	datetime			NOT NULL,
	[SizeBytes]		INT					NOT NULL,
	[StartedOn]		datetime			NULL,
	[FinishedOn]	datetime			NULL,
	CONSTRAINT [PK_LogEntry_Id]       PRIMARY KEY ([Id])
)
GO
CREATE INDEX [IX_LogEntry_Id] ON [LogEntry]([Id])