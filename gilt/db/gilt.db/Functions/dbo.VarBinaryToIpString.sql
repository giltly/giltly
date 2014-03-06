CREATE FUNCTION [dbo].[VarBinaryToIpString]
(
	@param1 varbinary(16)	
)
RETURNS VARCHAR(45) 
AS
BEGIN
DECLARE @ipAddressString VARCHAR(45)

SET @ipAddressString = (SELECT TOP 1   
   CASE WHEN 4 = DATALENGTH(@param1) THEN
   CAST(ROUND( (cast(@param1 as bigint) / 16777216 ), 0, 1) AS varchar(3)) + '.' +
   CAST((ROUND( (cast(@param1 as bigint) / 65536 ), 0, 1) % 256) AS varchar(3)) + '.' +
   CAST((ROUND( (cast(@param1 as bigint) / 256 ), 0, 1) % 256) AS varchar(3)) + '.' + 
   CAST((cast(@param1 as bigint) % 256 ) AS varchar(3)) 
   ELSE
   'ipv6'
   END);

RETURN (@ipAddressString);
END;
