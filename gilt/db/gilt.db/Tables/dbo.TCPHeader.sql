CREATE TABLE [TCPHeader]
( 
    [Id]				INT IDENTITY(1,1)  NOT NULL,  
	[SensorId]			NUMERIC(10,0)      NOT NULL,  
	[EventId]			NUMERIC(10,0)      NOT NULL,
	[SourcePort]		INT                NOT NULL,
	[DestinationPort]   INT                NOT NULL,
	[Sequence]			NUMERIC(10,0),
	[ACK]				NUMERIC(10,0),
	[Offset]			TINYINT,
	[Reserved]			TINYINT,
	[Flags]				TINYINT            NULL,
	[Window]			INT,
	[CheckSum]			INT,
	[Urgent]			INT,
	CONSTRAINT [PK_TCPHeader_SensorId_EventId] PRIMARY KEY ([SensorId],[EventId]),
	CONSTRAINT [FK_TCPHeader_Event] FOREIGN KEY ([SensorId], [EventId]) REFERENCES [Event]([SensorId],[EventId]),
	CONSTRAINT [FK_TCPHeader_Flags] FOREIGN KEY ([Flags]) REFERENCES [Flags]([Number]),

)
GO
CREATE INDEX [IX_TCPHeader_Id] ON [TCPHeader]([Id])

GO
CREATE INDEX [IX_TCPHeader_SourcePort] ON [TCPHeader]([SourcePort])

GO
CREATE INDEX [IX_TCPHeader_DestinationPort] ON [TCPHeader]([DestinationPort])

GO
CREATE INDEX [IX_TCPHeader_Flags] ON [TCPHeader]([Flags])