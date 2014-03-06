CREATE TABLE NLog(    
	[Id]				INT IDENTITY(1,1) NOT NULL,
    [TimeStamp]			DATETIME ,
    [LogLevel]			VARCHAR(16),
    [Logger]			VARCHAR(64), 
    [Message]			VARCHAR(8000),
    [MachineName]		VARCHAR(32),
    [UserName]			VARCHAR(64), 
    [Method]			VARCHAR(512),
    [Thread]			VARCHAR(100),
    [Exception]			VARCHAR(8000),
    [StackTrace]		VARCHAR(8000),
	CONSTRAINT [PK_NLog_Id]     PRIMARY KEY ([Id])
)

GO
CREATE INDEX [IX_NLog_Id] ON [NLog] ([Id])

GO
CREATE INDEX [IX_NLog_TimeStamp] ON [NLog] ([TimeStamp])

GO
CREATE INDEX [IX_NLog_LogLevel] ON [NLog] ([LogLevel])

GO
CREATE INDEX [IX_NLog_Logger] ON [NLog] ([Logger])

GO
CREATE INDEX [IX_NLog_MachineName] ON [NLog] ([MachineName])

GO
CREATE INDEX [IX_NLog_UserName] ON [NLog] ([UserName])

GO
CREATE INDEX [IX_NLog_Method] ON [NLog] ([Method])