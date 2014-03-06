CREATE TABLE [ICMPHeader]
( 
    [Id]					    INT IDENTITY(1,1)  NOT NULL,  
	[SensorId]					NUMERIC(10,0)      NOT NULL,  
	[EventId]					NUMERIC(10,0)      NOT NULL,
	[Type]						TINYINT            NULL,
	[Code]						TINYINT            NULL,
	[Checksum]					INT,
	[ICMPId]					INT,
	[ICMPSequence]				INT,
	CONSTRAINT [PK_ICMPHeader_SensorId_EventId] PRIMARY KEY ([SensorId],[EventId]),
	CONSTRAINT [FK_ICMPHeader_Event] FOREIGN KEY ([SensorId], [EventId]) REFERENCES [Event]([SensorId],[EventId]),
)
GO
CREATE INDEX [IX_ICMPHeader_Id] ON [ICMPHeader]([Id])

GO
CREATE INDEX [IX_ICMPHeader_Type] ON [ICMPHeader]([Type])