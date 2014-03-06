CREATE VIEW [dbo].[EventsBySignature]
AS
	SELECT DISTINCT q.[SignatureId] as "Ip", q.[Name], (q.[EventCount]) as "EventCount" FROM
	(
		SELECT evt.[SignatureId], sig.[Name], COUNT(evt.EventId) as "EventCount" FROM [Event] as evt 
		inner join [Signature] as sig on (evt.[SignatureId] = sig.[Id])
		GROUP BY evt.[SignatureId], sig.[Name]
	) as q
	GROUP BY q.[SignatureId], q.[Name], q.[EventCount]