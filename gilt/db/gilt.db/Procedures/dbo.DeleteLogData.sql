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