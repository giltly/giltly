CREATE TABLE [GeoLocation]
(
	[Id]			INT	IDENTITY(1,1)	NOT NULL,
	[LocationId]	INT					NOT NULL,
	[CountryCode]	VARCHAR(2)			NOT NULL,
	[RegionCode]	VARCHAR(2)			NOT NULL,
	[City]			VARCHAR(50),
	[PostalCode]	VARCHAR(20)			NOT NULL,
	[Latitude]		FLOAT,
	[Longitude]		FLOAT,
	[DmaCode]		INT,
	[AreaCode]		INT,
	CONSTRAINT [PK_GeoLocation_LocationId] PRIMARY KEY ([LocationId]),		
	CONSTRAINT [FK_GeoLocation_CountryCodes] FOREIGN KEY ([CountryCode]) REFERENCES [CountryCodes]([ISO2])
)
GO

CREATE INDEX [IX_GeoLocation_Id] ON [GeoLocation] ([Id])

GO
CREATE INDEX [IX_GeoLocation_LocationId] ON [GeoLocation] ([LocationId])

GO
CREATE INDEX [IX_GeoLocation_CountryCode] ON [GeoLocation] ([CountryCode])

GO
CREATE INDEX [IX_GeoLocation_RegionCode] ON [GeoLocation] ([RegionCode])

GO
CREATE INDEX [IX_GeoLocation_PostalCode] ON [GeoLocation] ([PostalCode])

GO
CREATE INDEX [IX_GeoLocation_City] ON [GeoLocation] ([City])

GO
CREATE INDEX [IX_GeoLocation_AreaCode] ON [GeoLocation] ([AreaCode])