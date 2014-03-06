CREATE TABLE [Search]
( 
	[Id]	    		INT IDENTITY(1,1) NOT NULL,  
	[Name]				VARCHAR(64) NOT NULL,
	[CreatedBy]			INT NOT NULL,
	[CreatedOn]			DATETIME DEFAULT GETDATE() NOT NULL,
	[SourceIp]          VARBINARY(16),
	[DestinationIp]     VARBINARY(16),
	[SignatureId]       NUMERIC(10,0),
	[SignatureClassificationId]       NUMERIC(10,0),
	[PacketType]        VARCHAR(4),
	[SourcePort]        INT,
	[DestinationPort]   INT,
	[StartSearch]       DATETIME,
	[EndSearch]         DATETIME,
	CONSTRAINT [PK_Search_Id] PRIMARY KEY ([Id]),
	CONSTRAINT [UC_Search] UNIQUE ([Name]),
	CONSTRAINT [FK_Search_Signature]	FOREIGN KEY ([SignatureId])	REFERENCES [Signature]([Id]),
	CONSTRAINT [FK_Search_SignatureClassification]	FOREIGN KEY ([SignatureClassificationId])	REFERENCES [SignatureClassification]([ClassificationId]),
	CONSTRAINT [FK_Search_CreatedBy]	FOREIGN KEY ([CreatedBy])	REFERENCES [Users]([Id])		
)
GO
CREATE INDEX [IX_Search_Id] ON [Search]([Id])

GO
CREATE INDEX [IX_Search_Name] ON [Search]([Name])

GO
CREATE INDEX [IX_Search_CreatedBy] ON [Search]([CreatedBy])

GO
CREATE INDEX [IX_Search_CreatedOn] ON [Search]([CreatedOn])

GO
CREATE INDEX [IX_Search_SourceIp] ON [Search]([SourceIp])

GO
CREATE INDEX [IX_Search_DetinationIp] ON [Search]([DestinationIp])

GO
CREATE INDEX [IX_Search_SignatureId] ON [Search]([SignatureId])

GO
CREATE INDEX [IX_Search_SignatureClassificationId] ON [Search]([SignatureClassificationId])

GO
CREATE INDEX [IX_Search_PacketType] ON [Search]([PacketType])

GO
CREATE INDEX [IX_Search_SourcePort] ON [Search]([SourcePort])

GO
CREATE INDEX [IX_Search_DestinationPort] ON [Search]([DestinationPort])

GO
CREATE INDEX [IX_Search_StartSearch] ON [Search]([StartSearch])

GO
CREATE INDEX [IX_Search_EndSearch] ON [Search]([EndSearch])


