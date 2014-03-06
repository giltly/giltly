CREATE TABLE [Event]
( 
    [Id]					INT IDENTITY(1,1)   NOT NULL,  
	[EventId]				NUMERIC(10,0)		NOT NULL,  
	[SensorId]				NUMERIC(10,0)		NOT NULL,  	
	[SignatureId]			NUMERIC(10,0)		NOT NULL,  
	[TimeStamp]				DATETIME			NOT NULL ,
	CONSTRAINT [PK_Event_SensorId_EventId]		PRIMARY KEY ([SensorId],[EventId]), 
    CONSTRAINT [FK_Event_Sensor]	FOREIGN KEY ([SensorId])	REFERENCES [Sensor]([Id]),	
	CONSTRAINT [FK_Event_Signature] FOREIGN KEY ([SignatureId]) REFERENCES [Signature]([Id]),
)
GO
CREATE INDEX [IX_Event_Id] ON [Event] ([Id])

GO
CREATE INDEX [IX_Event_Signature] ON [Event] ([SignatureId])

GO
CREATE INDEX [IX_Event_Timestamp] ON [Event] ([TimeStamp])

