------------------------------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.views WHERE name ='EventsBySignature')
BEGIN
	DROP VIEW [EventsBySignature]
END
GO

CREATE VIEW [dbo].[EventsBySignature]
AS
	SELECT DISTINCT q.[SignatureId] as "Ip", q.[Name], (q.[EventCount]) as "EventCount" FROM
	(
		SELECT evt.[SignatureId], sig.[Name], COUNT(evt.EventId) as "EventCount" FROM [Event] as evt 
		inner join [Signature] as sig on (evt.[SignatureId] = sig.[Id])
		GROUP BY evt.[SignatureId], sig.[Name]
	) as q
	GROUP BY q.[SignatureId], q.[Name], q.[EventCount]
GO
------------------------------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.views WHERE name ='EventsByCountry')
BEGIN
	DROP VIEW [EventsByCountry]
END	
GO

CREATE VIEW [dbo].[EventsByCountry]
AS
	SELECT 
	gl.[CountryCode] AS "CountryCode"
	,cc.[ISO3] AS "CountryCode3"
	,count(*) AS "CountryCount" 
	FROM [IPHeader] AS iph
	inner join [GeoLocation] as gl on (iph.[IpSourceLocationId] = gl.[LocationId])
	inner join [CountryCodes] as cc on (gl.[CountryCode] = cc.[ISO2])
	GROUP BY gl.CountryCode, cc.[ISO3]
GO
------------------------------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.views WHERE name ='EventsByIp')
BEGIN
	DROP VIEW [EventsByIp]
END		
GO

CREATE VIEW [dbo].[EventsByIp]
AS
	SELECT DISTINCT q.[Ip] as "Ip", SUM(q.[EventCount]) as "EventCount" FROM
	(
		SELECT dbo.VarBinaryToIpString(iph.[IPSource]) as "Ip", COUNT(evt.EventId) as "EventCount" 
		FROM [IPHeader] as iph
		inner join [Event] as evt on ((iph.[EventId] = evt.[EventId]) and (iph.[SensorId] = evt.[SensorId]))
		GROUP BY iph.[IPSource]
		UNION
		SELECT dbo.VarBinaryToIpString(iph.[IPDestination]) as "Ip", COUNT(evt.EventId) as "EventCount" 
		FROM [IPHeader] as iph
		inner join [Event] as evt on ((iph.[EventId] = evt.[EventId]) and (iph.[SensorId] = evt.[SensorId]))
		GROUP BY iph.[IPDestination]
	) as q
	GROUP BY q.[Ip]
GO
------------------------------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.views WHERE name ='UniqueSourcePorts')
BEGIN
	DROP VIEW [UniqueSourcePorts]
END	
GO

CREATE VIEW [dbo].[UniqueSourcePorts]
AS
	SELECT DISTINCT q.*  FROM
		(SELECT DISTINCT
		tch.[SourcePort] as "Port",
		COUNT(*) as "PortCount"
		FROM [TCPHeader] as tch
		GROUP BY [SourcePort]
		UNION 
		SELECT DISTINCT
		udh.[SourcePort] as "Port",
		COUNT(*) as "PortCount"
		FROM [UDPHeader] as udh
		GROUP BY [SourcePort]) as q
GO
------------------------------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.views WHERE name ='UniqueDestinationPorts')
BEGIN
	DROP VIEW [UniqueDestinationPorts]
END	
GO

CREATE VIEW [dbo].[UniqueDestinationPorts]
AS
	SELECT DISTINCT q.*  FROM
		(SELECT DISTINCT
		tch.[DestinationPort] as "Port",
		COUNT(*) as "PortCount"
		FROM [TCPHeader] as tch
		GROUP BY [DestinationPort]
		UNION 
		SELECT DISTINCT
		udh.[DestinationPort] as "Port",
		COUNT(*) as "PortCount"
		FROM [UDPHeader] as udh
		GROUP BY [DestinationPort]) as q
GO
------------------------------------------------------------------------------------------------------------------------------------