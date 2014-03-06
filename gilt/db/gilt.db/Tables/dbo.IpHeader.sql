CREATE TABLE [IPHeader]
( 
    [Id]						INT IDENTITY(1,1)	NOT NULL,  
	[SensorId]					NUMERIC(10,0)		NOT NULL,
	[EventId]					NUMERIC(10,0)		NOT NULL,
	[IPSource]					VARBINARY(16)			NULL,
	[IPDestination]				VARBINARY(16)			NULL,
	[IPSourceLocationId]		INT						NULL,
	[IPDestinationLocationId]	INT						NULL,
	[Version]					TINYINT,
	[HeaderLength]				TINYINT,
	[TOS]						TINYINT,
	[Length]					INT,
	[Identification]			INT,
	[Flags]						TINYINT,
	[Offset]					INT,
	[TTL]						TINYINT,
	[ProtocolId]				TINYINT NOT NULL ,
	[CheckSum]					INT,
	CONSTRAINT [PK_SensorId_EventId]				PRIMARY KEY ([SensorId],[EventId]),
	CONSTRAINT [FK_IPHeader_Event]					FOREIGN KEY ([SensorId], [EventId])		REFERENCES [Event]([SensorId],[EventId]),
	CONSTRAINT [FK_IPHeader_SourceLocation]			FOREIGN KEY ([IPSourceLocationId])		REFERENCES [GeoLocation]([LocationId]),
	CONSTRAINT [FK_IPHeader_DestinationLocation]	FOREIGN KEY ([IPDestinationLocationId]) REFERENCES [GeoLocation]([LocationId]),
)
GO
CREATE INDEX [IX_IPHeader_Id] ON [IPHeader]([Id])

GO
CREATE INDEX [IX_IPHeader_IPSource] ON [IPHeader]([IPSource])

GO
CREATE INDEX [IX_IPHeader_IPDestination] ON [IPHeader]([IPDestination])

GO
CREATE INDEX [IX_IPHeader_IPSourceLocationId] ON [IPHeader]([IPSourceLocationId])

GO
CREATE INDEX [IX_IPHeader_IPDestinationLocationId] ON [IPHeader]([IPDestinationLocationId])