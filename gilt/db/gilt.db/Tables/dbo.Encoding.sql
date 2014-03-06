CREATE TABLE [Encoding]
( 
	[Id]		INT IDENTITY(0,1)   NOT NULL,  
    [Text]		VARCHAR(50)			NOT NULL ,
    CONSTRAINT [PK_Encoding_Id] PRIMARY KEY ([Id])
)
GO
CREATE INDEX [IX_Encoding_Id] ON [Encoding] ([Id])
