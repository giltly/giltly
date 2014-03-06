IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VarBinaryToIpString]') AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
BEGIN
	DROP FUNCTION [dbo].[VarBinaryToIpString]
END
GO

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
GO
------------------------------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'DeleteEventData')
BEGIN
	DROP PROCEDURE [dbo].[DeleteEventData]
END
GO

CREATE PROCEDURE [dbo].[DeleteEventData]
AS
	BEGIN TRANSACTION

	DELETE FROM [IPHeader]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting IPHeader in DeleteEventData.', 16, 1)
		RETURN
	END

	DELETE FROM [ICMPHeader]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting ICMPHeader in DeleteEventData.', 16, 1)
		RETURN
	END

	DELETE FROM [TCPHeader]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting TCPHeader in DeleteEventData.', 16, 1)
		RETURN
	END

	DELETE FROM [UDPHeader]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting UDPHeader in DeleteEventData.', 16, 1)
		RETURN
	END

	DELETE FROM [Data]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting Data in DeleteEventData.', 16, 1)
		RETURN
	END

	DELETE FROM [EventComments]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting EventComments in DeleteEventData.', 16, 1)
		RETURN
	END	
	
	DELETE FROM [Event]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting Event in DeleteEventData.', 16, 1)
		RETURN
	END

	DELETE FROM [Sensor]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting Sensor in DeleteEventData.', 16, 1)
		RETURN
	END

	DELETE FROM [Signature]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting Signature in DeleteEventData.', 16, 1)
		RETURN
	END

	DELETE FROM [SignatureClassification]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting SignatureClassification in DeleteEventData.', 16, 1)
		RETURN
	END

	DELETE FROM [Reference]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting Reference in DeleteEventData.', 16, 1)
		RETURN
	END

	DELETE FROM [ReferenceSystem]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting ReferenceSystem in DeleteEventData.', 16, 1)
		RETURN
	END

	DELETE FROM [SignatureReference]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting SignatureReference in DeleteEventData.', 16, 1)
		RETURN
	END
	COMMIT TRANSACTION
RETURN
GO
------------------------------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'DeleteGeoData')
BEGIN
	DROP PROCEDURE [dbo].[DeleteGeoData]
END
GO

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
GO
------------------------------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'DeleteLogData')
BEGIN
	DROP PROCEDURE [dbo].[DeleteLogData]
END
GO

CREATE PROCEDURE [dbo].[DeleteLogData]
AS
	BEGIN TRANSACTION

	DELETE FROM [LogHistory]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting LogHistory in DeleteLogData.', 16, 1)
		RETURN
	END

	DELETE FROM [LogEntry]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting LogEntry in DeleteLogData.', 16, 1)
		RETURN
	END

	COMMIT TRANSACTION
	RETURN
GO
------------------------------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'DeleteSnortData')
BEGIN
	DROP PROCEDURE [dbo].[DeleteSnortData]
END
GO

CREATE PROCEDURE [dbo].[DeleteSnortData]
AS
	BEGIN TRANSACTION

	DELETE FROM [Signature]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting Signature in DeleteSnortData.', 16, 1)
		RETURN
	END

	DELETE FROM [SignatureClassification]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting SignatureClassification in DeleteSnortData.', 16, 1)
		RETURN
	END

	DELETE FROM [Reference]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting Reference in DeleteSnortData.', 16, 1)
		RETURN
	END

	DELETE FROM [ReferenceSystem]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting ReferenceSystem in DeleteSnortData.', 16, 1)
		RETURN
	END

	DELETE FROM [SignatureReference]
	IF @@ERROR <> 0
	BEGIN    
		ROLLBACK    
		RAISERROR ('Error in deleting SignatureReference in DeleteSnortData.', 16, 1)
		RETURN
	END

	COMMIT TRANSACTION
	RETURN
GO
------------------------------------------------------------------------------------------------------------------------------------