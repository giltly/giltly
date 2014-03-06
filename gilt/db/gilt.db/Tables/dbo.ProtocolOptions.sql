CREATE TABLE [ProtocolOptions]
( 
	[Id]			    INT IDENTITY(1,1)   NOT NULL,  
	[SensorId]			NUMERIC(10,0)		NOT NULL ,  -- FK to iphdr.sid, iphdr.cid
	[ClassificationId]  NUMERIC(10,0)		NOT NULL ,  -- or to tcphdr.sid, tcphdr.cid
	[OptionId]			NUMERIC(10,0)		NOT NULL ,
	[ProtocolId]		TINYINT				NOT NULL ,
	[Code]				TINYINT				NOT NULL ,
	[Length]			INT ,
	[Data]				VARCHAR(8000) ,
	CONSTRAINT [PK_ProtocolOptions_SensorId_ClassificationId_OptionId] PRIMARY KEY ([SensorId],[ClassificationId],[OptionId]),
	CONSTRAINT [FK_ProtocolOptions_Protocols] FOREIGN KEY ([ProtocolId]) REFERENCES [Protocols]([ProtocolId])
	--CONSTRAINT [FK_ProtocolOptions_SensorId] FOREIGN KEY ([SensorId]) REFERENCES [IPHeader]([SensorId]),
	--CONSTRAINT [FK_ProtocolOptions_ClassificationId] FOREIGN KEY ([ClassificationId]) REFERENCES [IPHeader]([ClassificationId]),
)
GO
CREATE INDEX [IX_SensorId_ClassificationId_OptionId] ON [ProtocolOptions]([SensorId],[ClassificationId],[OptionId])

GO
CREATE INDEX [IX_ProtocolOptions_Id] ON [ProtocolOptions]([Id])