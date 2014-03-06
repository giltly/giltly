CREATE TABLE [Protocols]
(
	[Id]				  INT IDENTITY(1,1) NOT NULL,    
	[ProtocolId]		  TINYINT			NOT NULL,    
	[Name]				  VARCHAR(50)		NOT NULL,    
	[Description]		  VARCHAR(50),
	CONSTRAINT [PK_Protocols_Id] PRIMARY KEY ([Id]),		
	UNIQUE ([ProtocolId])
)
GO
CREATE INDEX [IX_Protocols_Id] ON [Protocols]([Id])