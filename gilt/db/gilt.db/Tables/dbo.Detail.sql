CREATE TABLE [Detail]
( 
	[Id]					INT IDENTITY(0,1)   NOT NULL,  
    [Text]					VARCHAR(50)			NOT NULL ,
    CONSTRAINT [PK_Detail_Id] PRIMARY KEY ([Id])
)
GO
CREATE INDEX [IX_Detail_Id] ON [Detail] ([Id])