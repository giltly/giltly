CREATE TABLE [Services]
(
	[Id]		    INT IDENTITY(1,1)  NOT NULL,  
    [Port]          INT			 NOT NULL, 
    [ProtocolId]    TINYINT		 NOT NULL,
    [Name]			VARCHAR(50)  NOT NULL,
    [Description]	VARCHAR(255) NOT NULL,
	CONSTRAINT [PK_Services_Id] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Services_Protocol] FOREIGN KEY ([ProtocolId]) REFERENCES [Protocols]([ProtocolId]),
)
GO
CREATE INDEX [IX_Services_Id] ON [Services] ([Id])

