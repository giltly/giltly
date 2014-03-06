CREATE TABLE [Signature]
(
	[Id]					NUMERIC(10,0) IDENTITY(1,1) NOT NULL ,
	[Name]					VARCHAR(255)  NOT NULL,
	[ClassificationId]		NUMERIC(10,0) NOT NULL,
	[Priority]				NUMERIC(10,0) DEFAULT 0,
	[Revision]				NUMERIC(10,0) DEFAULT 0,
	[SignatureIdInternal]	NUMERIC(10,0),
	[GeneratorId]			NUMERIC(10,0),
	CONSTRAINT [PK_Signature_Id] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Signature_SignatureClassification] FOREIGN KEY ([ClassificationId]) REFERENCES [SignatureClassification]([ClassificationId]),
)
GO
CREATE INDEX [IX_Signature_Id] ON [Signature] ([Id])

GO
CREATE INDEX [IX_Signature_Name] ON [Signature] ([Name])
