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
