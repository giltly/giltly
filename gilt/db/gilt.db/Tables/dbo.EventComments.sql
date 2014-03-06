CREATE TABLE [EventComments]
( 
    [Id]					INT IDENTITY(1,1)   NOT NULL,  
	[EventId]				NUMERIC(10,0)		NOT NULL,  
	[SensorId]				NUMERIC(10,0)		NOT NULL,
	[Comment]				VARCHAR(512)		NOT NULL,  	
	[CreatedBy]				INT					NOT NULL,
	[CreatedOn]				DATETIME			NOT NULL,
	CONSTRAINT [FK_EventComments_Event]		FOREIGN KEY ([SensorId], [EventId])		REFERENCES [Event]([SensorId],[EventId]),
	CONSTRAINT [FK_EventComments_User]		FOREIGN KEY ([CreatedBy])				REFERENCES [Users]([Id]),
	CONSTRAINT [PK_EventComments_EventId_SensorId]		PRIMARY KEY ([SensorId],[EventId]) 
)
GO
CREATE INDEX [IX_EventComments_Id] ON [EventComments] ([Id])

GO
CREATE INDEX [IX_EventComments_Comment] ON [EventComments] ([Comment])

GO
CREATE INDEX [IX_EventComments_CreatedBy] ON [EventComments] ([CreatedBy])

GO
CREATE INDEX [IX_EventComments_CreatedOn] ON [EventComments] ([CreatedOn])
