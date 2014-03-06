CREATE TABLE [SignatureClassification]
(
	[Id]							INT IDENTITY(1,1) NOT NULL,
	[ClassificationId]				NUMERIC(10,0)	  NOT NULL,
	[Name]							VARCHAR(60)       NOT NULL,
	[Description]					VARCHAR(255)      NOT NULL,
	[DefaultPriority]				TINYINT NULL, 
    CONSTRAINT [PK_SignatureClassification_ClassificationId] PRIMARY KEY ([ClassificationId])
)
GO
CREATE INDEX [IX_SignatureClassification_Id] ON [SignatureClassification]([Id])

GO
CREATE INDEX [IX_SignatureClassification_ClassificationId] ON [SignatureClassification]([ClassificationId])

GO
CREATE INDEX [IX_SignatureClassification_Name] ON [SignatureClassification]([Name])
