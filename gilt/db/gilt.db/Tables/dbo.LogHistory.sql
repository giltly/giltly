CREATE TABLE [LogHistory]
(
	[Id]					INT IDENTITY(1,1)   NOT NULL,  
	[CurrentLogEntryId]		INT					NULL, 
	[CurrentOffsetBytes]	INT					NULL,
	CONSTRAINT [PK_LogHistory_Id]       PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LogHistory_LogEntry] FOREIGN KEY ([CurrentLogEntryId]) REFERENCES [LogEntry]([Id]),	
)
GO
CREATE INDEX [IX_LogHistory_Id] ON [LogHistory]([Id])