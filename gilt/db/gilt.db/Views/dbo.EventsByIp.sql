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