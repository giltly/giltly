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