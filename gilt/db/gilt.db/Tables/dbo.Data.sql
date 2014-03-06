CREATE TABLE [Data]
( 
    [Id]					INT IDENTITY(1,1)   NOT NULL,  
	[SensorId]			    NUMERIC(10,0)		NOT NULL,  
	[EventId]			    NUMERIC(10,0)		NOT NULL,
	[Payload]				VARCHAR(8000)	
	CONSTRAINT [PK_Id]     PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Data_Sensor] FOREIGN KEY ([SensorId], [EventId]) REFERENCES [Event]([SensorId],[EventId]),	
)
GO
CREATE INDEX [IX_Data_SensorId_EventId] ON [Data] ([SensorId], [EventId])