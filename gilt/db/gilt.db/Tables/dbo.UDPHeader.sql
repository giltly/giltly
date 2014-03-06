CREATE TABLE [UDPHeader]
( 
	[Id]	    		INT IDENTITY(1,1)   NOT NULL,  
	[SensorId]			NUMERIC(10,0)		NOT NULL,  
	[EventId]			NUMERIC(10,0)		NOT NULL,
	[SourcePort]		INT					NOT NULL,
	[DestinationPort]   INT					NOT NULL,
	[Length]			INT,
	[CheckSum]			INT,	
	CONSTRAINT [PK_UDPHeader_SensorId_EventId] PRIMARY KEY ([SensorId],[EventId]),
	CONSTRAINT [FK_UDPHeader_Event] FOREIGN KEY ([SensorId], [EventId]) REFERENCES [Event]([SensorId],[EventId]),
)
GO
CREATE INDEX [IX_UDPHeader_Id] ON [UDPHeader]([Id])

GO
CREATE INDEX [IX_UDPHeader_SourcePort] ON [UDPHeader]([SourcePort])

GO
CREATE INDEX [IX_UDPHeader_DestinationPort] ON [UDPHeader]([DestinationPort])