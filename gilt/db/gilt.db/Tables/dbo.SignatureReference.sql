CREATE TABLE [SignatureReference]
(
	[SignatureId]		NUMERIC(10,0) NOT NULL,  
	[SequenceId]		NUMERIC(10,0) NOT NULL,
	[ReferenceId]		NUMERIC(10,0) NOT NULL,  
	CONSTRAINT [PK_SignatureReference_SignatureId_SequenceId]  PRIMARY KEY([SignatureId], [SequenceId])	,
	CONSTRAINT [FK_SignatureReference_Signature] FOREIGN KEY ([SignatureId]) REFERENCES [Signature]([Id]),
	CONSTRAINT [FK_SignatureReference_Reference] FOREIGN KEY ([ReferenceId]) REFERENCES [Reference]([Id]),
)
GO
CREATE INDEX [IX_SignatureReference_ReferenceId] ON [SignatureReference] ([ReferenceId])