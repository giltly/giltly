CREATE TABLE [Sensor]
( 
	[Id]						NUMERIC(10,0) IDENTITY(0,1) NOT NULL ,
	[Hostname]					VARCHAR(100) ,
	[Interface]					VARCHAR(100) ,
	[Filter]					VARCHAR(100) ,
	[DetailId]					INT DEFAULT 1,                        
	[EncodingId]				INT DEFAULT 1	
	CONSTRAINT [PK_Sensor_Id] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Sensor_Detail] FOREIGN KEY ([DetailId]) REFERENCES [Detail]([Id]),
	CONSTRAINT [FK_Sensor_Encoding] FOREIGN KEY ([EncodingId]) REFERENCES [Encoding]([Id]),
)
GO
CREATE INDEX [IX_Sensor_Id] ON [Sensor] ([Id])

GO
CREATE INDEX [IX_Sensor_DetailId] ON [Sensor] ([DetailId])

GO
CREATE INDEX [IX_Sensor_EncodingId] ON [Sensor] ([EncodingId])