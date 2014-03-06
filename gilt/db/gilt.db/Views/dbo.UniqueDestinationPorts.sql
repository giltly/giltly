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
