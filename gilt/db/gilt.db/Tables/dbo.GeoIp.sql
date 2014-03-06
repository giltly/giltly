CREATE TABLE [GeoIp]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[StartIpNumber] VARBINARY(16) NOT NULL,
	[EndIpNumber]	VARBINARY(16) NOT NULL,
	[LocationId]	INT NOT NULL,
	CONSTRAINT [PK_GeoIp_Id] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_GeoIp_GeoLocation] FOREIGN KEY ([LocationId]) REFERENCES [GeoLocation]([LocationId])
)
GO
CREATE INDEX [IX_GeoIp_Id] ON [GeoIp] ([Id])

GO
CREATE INDEX [IX_GeoIp_LocationId] ON [GeoIp] ([LocationId])

GO
CREATE INDEX [IX_GeoIp_StartIpNumber_EndIpNumber] ON [GeoIp] ([StartIpNumber],[EndIpNumber])