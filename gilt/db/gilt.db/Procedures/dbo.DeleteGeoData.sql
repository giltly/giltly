CREATE PROCEDURE [dbo].[DeleteGeoData]
AS

BEGIN TRANSACTION

DELETE FROM [GeoIp]
IF @@ERROR <> 0
BEGIN    
	ROLLBACK    
	RAISERROR ('Error in deleting GeoIp in DeleteGeoData.', 16, 1)
	RETURN
END

DELETE FROM [GeoLocation]
IF @@ERROR <> 0
BEGIN    
	ROLLBACK    
	RAISERROR ('Error in deleting GeoLocation in DeleteGeoData.', 16, 1)
	RETURN
END

COMMIT TRANSACTION
RETURN