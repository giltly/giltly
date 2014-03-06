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