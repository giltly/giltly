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