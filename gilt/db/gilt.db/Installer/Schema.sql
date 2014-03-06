IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Schema]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Schema] 
	( 
		[Id]				NUMERIC(10,0) NOT NULL,
		[ParseTime]			DATETIME      NOT NULL DEFAULT GETDATE(),
		CONSTRAINT [PK_Schema_Id] PRIMARY KEY ([Id])
	)
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Schema_Id' AND object_id = OBJECT_ID('Schema'))
BEGIN
	CREATE INDEX [IX_Schema_Id] ON [Schema] ([Id])
END
----------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountryCodes]') AND type in (N'U'))
BEGIN
	CREATE TABLE [CountryCodes]
	(
		[Id] INT IDENTITY(1,1)   NOT NULL,  
		[ISO2] VARCHAR(2) NOT NULL,
		[ISO3] VARCHAR(3),
		[Name] VARCHAR(80) NOT NULL,
		[PrintableName] VARCHAR(80) NOT NULL,
		[Code] INT 
		CONSTRAINT [PK_CountryCodes_Id]     PRIMARY KEY ([ISO2])
	)
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_CountryCodes_Id' AND object_id = OBJECT_ID('CountryCodes'))
BEGIN
	CREATE INDEX [IX_CountryCodes_Id] ON [CountryCodes] ([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_CountryCodes_ISO2' AND object_id = OBJECT_ID('CountryCodes'))
BEGIN
	CREATE INDEX [IX_CountryCodes_ISO2] ON [CountryCodes] ([ISO2])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Flags]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Flags]
	(
		[Id]		  INT IDENTITY(1,1)   NOT NULL,  
		[Number]      TINYINT,
		[RES1]        INT,
		[RES2]        INT,
		[URG]         INT,
		[ACK]         INT,
		[PSH]         INT,
		[RST]         INT,
		[SYN]         INT,
		[FIN]         INT,
		[Valid]       INT,
		[Description] VARCHAR(255)	
		CONSTRAINT [PK_Flags_Number] PRIMARY KEY ([Number]),
	)
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Flags_Id' AND object_id = OBJECT_ID('Flags'))
BEGIN
	CREATE INDEX [IX_Flags_Id] ON [Flags] ([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Flags_Number' AND object_id = OBJECT_ID('Flags'))
BEGIN
	CREATE INDEX [IX_Flags_Number] ON [Flags] ([Number])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Encoding]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Encoding]
	( 
		[Id]		INT IDENTITY(0,1)   NOT NULL,  
		[Text]		VARCHAR(50)			NOT NULL ,
		CONSTRAINT [PK_Encoding_Id] PRIMARY KEY ([Id])
	)
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Encoding_Id' AND object_id = OBJECT_ID('Encoding'))
BEGIN
	CREATE INDEX [IX_Encoding_Id] ON [Encoding] ([Id])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Detail]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Detail]
	( 
		[Id]					INT IDENTITY(0,1)   NOT NULL,  
		[Text]					VARCHAR(50)			NOT NULL ,
		CONSTRAINT [PK_Detail_Id] PRIMARY KEY ([Id])
	)
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Detail_Id' AND object_id = OBJECT_ID('Detail'))
BEGIN
	CREATE INDEX [IX_Detail_Id] ON [Detail] ([Id])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Protocols]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Protocols]
	(
		[Id]				  INT IDENTITY(1,1) NOT NULL,    
		[ProtocolId]		  TINYINT			NOT NULL,    
		[Name]				  VARCHAR(50)		NOT NULL,    
		[Description]		  VARCHAR(50),
		CONSTRAINT [PK_Protocols_Id] PRIMARY KEY ([Id]),		
		UNIQUE ([ProtocolId])
	)
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Protocols_Id' AND object_id = OBJECT_ID('Protocols'))
BEGIN
	CREATE INDEX [IX_Protocols_Id] ON [Protocols]([Id])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProtocolOptions]') AND type in (N'U'))
BEGIN
	CREATE TABLE [ProtocolOptions]
	( 
		[Id]			    INT IDENTITY(1,1)   NOT NULL,  
		[SensorId]			NUMERIC(10,0)		NOT NULL ,  -- FK to iphdr.sid, iphdr.cid
		[ClassificationId]  NUMERIC(10,0)		NOT NULL ,  -- or to tcphdr.sid, tcphdr.cid
		[OptionId]			NUMERIC(10,0)		NOT NULL ,
		[ProtocolId]		TINYINT				NOT NULL ,
		[Code]				TINYINT				NOT NULL ,
		[Length]			INT ,
		[Data]				VARCHAR(8000) ,
		CONSTRAINT [PK_ProtocolOptions_SensorId_ClassificationId_OptionId] PRIMARY KEY ([SensorId],[ClassificationId],[OptionId]),
		CONSTRAINT [FK_ProtocolOptions_Protocols] FOREIGN KEY ([ProtocolId]) REFERENCES [Protocols]([ProtocolId])
	)
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_SensorId_ClassificationId_OptionId' AND object_id = OBJECT_ID('ProtocolOptions'))
BEGIN
	CREATE INDEX [IX_SensorId_ClassificationId_OptionId] ON [ProtocolOptions]([SensorId],[ClassificationId],[OptionId])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_ProtocolOptions_Id' AND object_id = OBJECT_ID('ProtocolOptions'))
BEGIN
	CREATE INDEX [IX_ProtocolOptions_Id] ON [ProtocolOptions]([Id])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Services]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Services]
	(
		[Id]		    INT IDENTITY(1,1)  NOT NULL,  
		[Port]          INT			 NOT NULL, 
		[ProtocolId]    TINYINT		 NOT NULL,
		[Name]			VARCHAR(50)  NOT NULL,
		[Description]	VARCHAR(255) NOT NULL,
		CONSTRAINT [PK_Services_Id] PRIMARY KEY ([Id]),
		CONSTRAINT [FK_Services_Protocol] FOREIGN KEY ([ProtocolId]) REFERENCES [Protocols]([ProtocolId]),
	)
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Services_Id' AND object_id = OBJECT_ID('Services'))
BEGIN
	CREATE INDEX [IX_Services_Id] ON [Services] ([Id])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LogEntry]') AND type in (N'U'))
BEGIN
	CREATE TABLE [LogEntry]
	(
		[Id]			INT IDENTITY(1,1)   NOT NULL,  
		[FileName]		varchar(512)		NOT NULL,
		[CreatedOn]		datetime			NOT NULL,
		[ModifiedOn]	datetime			NOT NULL,
		[SizeBytes]		INT					NOT NULL,
		[StartedOn]		datetime			NULL,
		[FinishedOn]	datetime			NULL,
		CONSTRAINT [PK_LogEntry_Id]       PRIMARY KEY ([Id])
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_LogEntry_Id' AND object_id = OBJECT_ID('LogEntry'))
BEGIN
	CREATE INDEX [IX_LogEntry_Id] ON [LogEntry]([Id])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LogHistory]') AND type in (N'U'))
BEGIN
	CREATE TABLE [LogHistory]
	(
		[Id]					INT IDENTITY(1,1)   NOT NULL,  
		[CurrentLogEntryId]		INT					NULL, 
		[CurrentOffsetBytes]	INT					NULL,
		CONSTRAINT [PK_LogHistory_Id]       PRIMARY KEY ([Id]),
		CONSTRAINT [FK_LogHistory_LogEntry] FOREIGN KEY ([CurrentLogEntryId]) REFERENCES [LogEntry]([Id]),	
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_LogHistory_Id' AND object_id = OBJECT_ID('LogHistory'))
BEGIN
	CREATE INDEX [IX_LogHistory_Id] ON [LogHistory]([Id])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sensor]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Sensor]
	( 
		[Id]						NUMERIC(10,0) IDENTITY(0,1) NOT NULL ,
		[Hostname]					VARCHAR(100) ,
		[Interface]					VARCHAR(100) ,
		[Filter]					VARCHAR(100) ,
		[DetailId]					INT DEFAULT 1,                        
		[EncodingId]				INT DEFAULT 1	
		CONSTRAINT [PK_Sensor_Id] PRIMARY KEY ([Id]),
		CONSTRAINT [FK_Sensor_Detail] FOREIGN KEY ([DetailId]) REFERENCES [Detail]([Id]),
		CONSTRAINT [FK_Sensor_Encoding] FOREIGN KEY ([EncodingId]) REFERENCES [Encoding]([Id]),
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Sensor_Id' AND object_id = OBJECT_ID('Sensor'))
BEGIN
	CREATE INDEX [IX_Sensor_Id] ON [Sensor] ([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Sensor_DetailId' AND object_id = OBJECT_ID('Sensor'))
BEGIN
	CREATE INDEX [IX_Sensor_DetailId] ON [Sensor] ([DetailId])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Sensor_EncodingId' AND object_id = OBJECT_ID('Sensor'))
BEGIN
	CREATE INDEX [IX_Sensor_EncodingId] ON [Sensor] ([EncodingId])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GeoLocation]') AND type in (N'U'))
BEGIN
	CREATE TABLE [GeoLocation]
	(
		[Id]			INT	IDENTITY(1,1)	NOT NULL,
		[LocationId]	INT					NOT NULL,
		[CountryCode]	VARCHAR(2)			NOT NULL,
		[RegionCode]	VARCHAR(2)			NOT NULL,
		[City]			VARCHAR(50),
		[PostalCode]	VARCHAR(20)			NOT NULL,
		[Latitude]		FLOAT,
		[Longitude]		FLOAT,
		[DmaCode]		INT,
		[AreaCode]		INT,
		CONSTRAINT [PK_GeoLocation_LocationId] PRIMARY KEY ([LocationId]),		
		CONSTRAINT [FK_GeoLocation_CountryCodes] FOREIGN KEY ([CountryCode]) REFERENCES [CountryCodes]([ISO2])
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_GeoLocation_Id' AND object_id = OBJECT_ID('GeoLocation'))
BEGIN
	CREATE INDEX [IX_GeoLocation_Id] ON [GeoLocation] ([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_GeoLocation_CountryCode' AND object_id = OBJECT_ID('GeoLocation'))
BEGIN
	CREATE INDEX [IX_GeoLocation_CountryCode] ON [GeoLocation] ([CountryCode])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_GeoLocation_RegionCode' AND object_id = OBJECT_ID('GeoLocation'))
BEGIN
	CREATE INDEX [IX_GeoLocation_RegionCode] ON [GeoLocation] ([RegionCode])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_GeoLocation_PostalCode' AND object_id = OBJECT_ID('GeoLocation'))
BEGIN
	CREATE INDEX [IX_GeoLocation_PostalCode] ON [GeoLocation] ([PostalCode])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_GeoLocation_City' AND object_id = OBJECT_ID('GeoLocation'))
BEGIN
	CREATE INDEX [IX_GeoLocation_City] ON [GeoLocation] ([City])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_GeoLocation_AreaCode' AND object_id = OBJECT_ID('GeoLocation'))
BEGIN
	CREATE INDEX [IX_GeoLocation_AreaCode] ON [GeoLocation] ([AreaCode])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GeoIp]') AND type in (N'U'))
BEGIN
	CREATE TABLE [GeoIp]
	(
		[Id] INT IDENTITY(1,1) NOT NULL,
		[StartIpNumber] VARBINARY(16) NOT NULL,
		[EndIpNumber]	VARBINARY(16) NOT NULL,
		[LocationId]	INT NOT NULL,
		CONSTRAINT [PK_GeoIp_Id] PRIMARY KEY ([Id]),
		CONSTRAINT [FK_GeoIp_GeoLocation] FOREIGN KEY ([LocationId]) REFERENCES [GeoLocation]([LocationId])
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_GeoIp_Id' AND object_id = OBJECT_ID('GeoIp'))
BEGIN
	CREATE INDEX [IX_GeoIp_Id] ON [GeoIp] ([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_GeoIp_LocationId' AND object_id = OBJECT_ID('GeoIp'))
BEGIN
	CREATE INDEX [IX_GeoIp_LocationId] ON [GeoIp] ([LocationId])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_GeoIp_StartIpNumber_EndIpNumber' AND object_id = OBJECT_ID('GeoIp'))
BEGIN
	CREATE INDEX [IX_GeoIp_StartIpNumber_EndIpNumber] ON [GeoIp] ([StartIpNumber],[EndIpNumber])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReferenceSystem]') AND type in (N'U'))
BEGIN
	CREATE TABLE [ReferenceSystem]
	(
		[Id]		NUMERIC(10,0) IDENTITY(1,1) NOT NULL,
		[Name]		VARCHAR(20),
		[URL]		VARCHAR(512) NULL, 
		CONSTRAINT [PK_ReferenceSystem_Id] PRIMARY KEY ([Id])
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_ReferenceSystem_Id' AND object_id = OBJECT_ID('ReferenceSystem'))
BEGIN
	CREATE INDEX [IX_ReferenceSystem_Id] ON [ReferenceSystem] ([Id])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Reference]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Reference]
	(
		[Id]				NUMERIC(10,0) IDENTITY(1,1) NOT NULL,
		[SystemId]			NUMERIC(10,0)               NOT NULL,
		[Tag]				VARCHAR(8000)               NOT NULL,
		CONSTRAINT [PK_Reference_Id] PRIMARY KEY ([Id]),
		CONSTRAINT [FK_Reference_ReferenceSystem]	FOREIGN KEY ([SystemId]) REFERENCES [ReferenceSystem]([Id]),
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Reference_Id' AND object_id = OBJECT_ID('Reference'))
BEGIN
	CREATE INDEX [IX_Reference_Id] ON [Reference] ([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Reference_SystemId' AND object_id = OBJECT_ID('Reference'))
BEGIN
	CREATE INDEX [IX_Reference_SystemId] ON [Reference] ([SystemId])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SignatureClassification]') AND type in (N'U'))
BEGIN
	CREATE TABLE [SignatureClassification]
	(
		[Id]							INT IDENTITY(1,1) NOT NULL,
		[ClassificationId]				NUMERIC(10,0)	  NOT NULL,
		[Name]							VARCHAR(60)       NOT NULL,
		[Description]					VARCHAR(255)      NOT NULL,
		[DefaultPriority]				TINYINT NULL, 
		CONSTRAINT [PK_SignatureClassification_ClassificationId] PRIMARY KEY ([ClassificationId])
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_SignatureClassification_Id' AND object_id = OBJECT_ID('SignatureClassification'))
BEGIN
	CREATE INDEX [IX_SignatureClassification_Id] ON [SignatureClassification]([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_SignatureClassification_ClassificationId' AND object_id = OBJECT_ID('SignatureClassification'))
BEGIN
	CREATE INDEX [IX_SignatureClassification_ClassificationId] ON [SignatureClassification]([ClassificationId])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_SignatureClassification_Name' AND object_id = OBJECT_ID('SignatureClassification'))
BEGIN
	CREATE INDEX [IX_SignatureClassification_Name] ON [SignatureClassification]([Name])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Signature]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Signature]
	(
		[Id]					NUMERIC(10,0) IDENTITY(1,1) NOT NULL ,
		[Name]					VARCHAR(255)  NOT NULL,
		[ClassificationId]		NUMERIC(10,0) NOT NULL,
		[Priority]				NUMERIC(10,0) DEFAULT 0,
		[Revision]				NUMERIC(10,0) DEFAULT 0,
		[SignatureIdInternal]	NUMERIC(10,0),
		[GeneratorId]			NUMERIC(10,0),
		CONSTRAINT [PK_Signature_Id] PRIMARY KEY ([Id]),
		CONSTRAINT [FK_Signature_SignatureClassification] FOREIGN KEY ([ClassificationId]) REFERENCES [SignatureClassification]([ClassificationId]),
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Signature_Id' AND object_id = OBJECT_ID('Signature'))
BEGIN
	CREATE INDEX [IX_Signature_Id] ON [Signature] ([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Signature_Id' AND object_id = OBJECT_ID('Signature'))
BEGIN
	CREATE INDEX [IX_Signature_Id] ON [Signature] ([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Signature_Name' AND object_id = OBJECT_ID('Signature'))
BEGIN
	CREATE INDEX [IX_Signature_Name] ON [Signature] ([Name])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SignatureReference]') AND type in (N'U'))
BEGIN
	CREATE TABLE [SignatureReference]
	(
		[SignatureId]		NUMERIC(10,0) NOT NULL,  
		[SequenceId]		NUMERIC(10,0) NOT NULL,
		[ReferenceId]		NUMERIC(10,0) NOT NULL,  
		CONSTRAINT [PK_SignatureReference_SignatureId_SequenceId]  PRIMARY KEY([SignatureId], [SequenceId])	,
		CONSTRAINT [FK_SignatureReference_Signature] FOREIGN KEY ([SignatureId]) REFERENCES [Signature]([Id]),
		CONSTRAINT [FK_SignatureReference_Reference] FOREIGN KEY ([ReferenceId]) REFERENCES [Reference]([Id]),
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_SignatureReference_ReferenceId' AND object_id = OBJECT_ID('SignatureReference'))
BEGIN
	CREATE INDEX [IX_SignatureReference_ReferenceId] ON [SignatureReference] ([ReferenceId])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Event]
	( 
		[Id]					INT IDENTITY(1,1)   NOT NULL,  
		[EventId]				NUMERIC(10,0)		NOT NULL,  
		[SensorId]				NUMERIC(10,0)		NOT NULL,  	
		[SignatureId]			NUMERIC(10,0)		NOT NULL,  
		[TimeStamp]				DATETIME			NOT NULL,		
		CONSTRAINT [PK_Event_SensorId_EventId]		PRIMARY KEY ([SensorId],[EventId]), 
		CONSTRAINT [FK_Event_Sensor]	FOREIGN KEY ([SensorId])	REFERENCES [Sensor]([Id]),	
		CONSTRAINT [FK_Event_Signature] FOREIGN KEY ([SignatureId]) REFERENCES [Signature]([Id]),
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Event_Id' AND object_id = OBJECT_ID('Event'))
BEGIN
	CREATE INDEX [IX_Event_Id] ON [Event] ([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Event_Signature' AND object_id = OBJECT_ID('Event'))
BEGIN
	CREATE INDEX [IX_Event_Signature] ON [Event] ([SignatureId])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Event_Timestamp' AND object_id = OBJECT_ID('Event'))
BEGIN
	CREATE INDEX [IX_Event_Timestamp] ON [Event] ([TimeStamp])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Data]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Data]
	( 
		[Id]					INT IDENTITY(1,1)   NOT NULL,  
		[SensorId]			    NUMERIC(10,0)		NOT NULL,  
		[EventId]			    NUMERIC(10,0)		NOT NULL,
		[Payload]				VARCHAR(8000)	
		CONSTRAINT [PK_Id]     PRIMARY KEY ([Id]),
		CONSTRAINT [FK_Data_Sensor] FOREIGN KEY ([SensorId], [EventId]) REFERENCES [Event]([SensorId],[EventId]),	
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Data_SensorId_EventId' AND object_id = OBJECT_ID('Data'))
BEGIN
	CREATE INDEX [IX_Data_SensorId_EventId] ON [Data] ([SensorId], [EventId])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TCPHeader]') AND type in (N'U'))
BEGIN
	CREATE TABLE [TCPHeader]
	( 
		[Id]				INT IDENTITY(1,1)  NOT NULL,  
		[SensorId]			NUMERIC(10,0)      NOT NULL,  
		[EventId]			NUMERIC(10,0)      NOT NULL,
		[SourcePort]		INT                NOT NULL,
		[DestinationPort]   INT                NOT NULL,
		[Sequence]			NUMERIC(10,0),
		[ACK]				NUMERIC(10,0),
		[Offset]			TINYINT,
		[Reserved]			TINYINT,
		[Flags]				TINYINT            NULL,
		[Window]			INT,
		[CheckSum]			INT,
		[Urgent]			INT,
		CONSTRAINT [PK_TCPHeader_SensorId_EventId] PRIMARY KEY ([SensorId],[EventId]),
		CONSTRAINT [FK_TCPHeader_Event] FOREIGN KEY ([SensorId], [EventId]) REFERENCES [Event]([SensorId],[EventId]),
		CONSTRAINT [FK_TCPHeader_Flags] FOREIGN KEY ([Flags]) REFERENCES [Flags]([Number]),
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_TCPHeader_Id' AND object_id = OBJECT_ID('TCPHeader'))
BEGIN
	CREATE INDEX [IX_TCPHeader_Id] ON [TCPHeader]([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_TCPHeader_SourcePort' AND object_id = OBJECT_ID('TCPHeader'))
BEGIN
	CREATE INDEX [IX_TCPHeader_SourcePort] ON [TCPHeader]([SourcePort])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_TCPHeader_DestinationPort' AND object_id = OBJECT_ID('TCPHeader'))
BEGIN
	CREATE INDEX [IX_TCPHeader_DestinationPort] ON [TCPHeader]([DestinationPort])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_TCPHeader_Flags' AND object_id = OBJECT_ID('TCPHeader'))
BEGIN
	CREATE INDEX [IX_TCPHeader_Flags] ON [TCPHeader]([Flags])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UDPHeader]') AND type in (N'U'))
BEGIN
	CREATE TABLE [UDPHeader]
	( 
		[Id]	    		INT IDENTITY(1,1)   NOT NULL,  
		[SensorId]			NUMERIC(10,0)		NOT NULL,  
		[EventId]			NUMERIC(10,0)		NOT NULL,
		[SourcePort]		INT					NOT NULL,
		[DestinationPort]   INT					NOT NULL,
		[Length]			INT,
		[CheckSum]			INT,	
		CONSTRAINT [PK_UDPHeader_SensorId_EventId] PRIMARY KEY ([SensorId],[EventId]),
		CONSTRAINT [FK_UDPHeader_Event] FOREIGN KEY ([SensorId], [EventId]) REFERENCES [Event]([SensorId],[EventId]),
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_UDPHeader_Id' AND object_id = OBJECT_ID('UDPHeader'))
BEGIN
	CREATE INDEX [IX_UDPHeader_Id] ON [UDPHeader]([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_UDPHeader_SourcePort' AND object_id = OBJECT_ID('UDPHeader'))
BEGIN
	CREATE INDEX [IX_UDPHeader_SourcePort] ON [UDPHeader]([SourcePort])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_UDPHeader_DestinationPort' AND object_id = OBJECT_ID('UDPHeader'))
BEGIN
	CREATE INDEX [IX_UDPHeader_DestinationPort] ON [UDPHeader]([DestinationPort])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICMPHeader]') AND type in (N'U'))
BEGIN
	CREATE TABLE [ICMPHeader]
	( 
		[Id]					    INT IDENTITY(1,1)  NOT NULL,  
		[SensorId]					NUMERIC(10,0)      NOT NULL,  
		[EventId]					NUMERIC(10,0)      NOT NULL,
		[Type]						TINYINT            NULL,
		[Code]						TINYINT            NULL,
		[Checksum]					INT,
		[ICMPId]					INT,
		[ICMPSequence]				INT,
		CONSTRAINT [PK_ICMPHeader_SensorId_EventId] PRIMARY KEY ([SensorId],[EventId]),
		CONSTRAINT [FK_ICMPHeader_Event] FOREIGN KEY ([SensorId], [EventId]) REFERENCES [Event]([SensorId],[EventId]),
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_ICMPHeader_Id' AND object_id = OBJECT_ID('ICMPHeader'))
BEGIN
	CREATE INDEX [IX_ICMPHeader_Id] ON [ICMPHeader]([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_ICMPHeader_Type' AND object_id = OBJECT_ID('ICMPHeader'))
BEGIN
	CREATE INDEX [IX_ICMPHeader_Type] ON [ICMPHeader]([Type])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IPHeader]') AND type in (N'U'))
BEGIN
	CREATE TABLE [IPHeader]
	( 
		[Id]						INT IDENTITY(1,1)	NOT NULL,  
		[SensorId]					NUMERIC(10,0)		NOT NULL,
		[EventId]					NUMERIC(10,0)		NOT NULL,
		[IPSource]					VARBINARY(16)			NULL,
		[IPDestination]				VARBINARY(16)			NULL,
		[IPSourceLocationId]		INT						NULL,
		[IPDestinationLocationId]	INT						NULL,
		[Version]					TINYINT,
		[HeaderLength]				TINYINT,
		[TOS]						TINYINT,
		[Length]					INT,
		[Identification]			INT,
		[Flags]						TINYINT,
		[Offset]					INT,
		[TTL]						TINYINT,
		[ProtocolId]				TINYINT NOT NULL ,
		[CheckSum]					INT,
		CONSTRAINT [PK_SensorId_EventId]				PRIMARY KEY ([SensorId], [EventId]),
		CONSTRAINT [FK_IPHeader_Event]					FOREIGN KEY ([SensorId], [EventId])		REFERENCES [Event]([SensorId],[EventId]),
		CONSTRAINT [FK_IPHeader_SourceLocation]			FOREIGN KEY ([IPSourceLocationId])		REFERENCES [GeoLocation]([LocationId]),
		CONSTRAINT [FK_IPHeader_DestinationLocation]	FOREIGN KEY ([IPDestinationLocationId]) REFERENCES [GeoLocation]([LocationId]),
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_IPHeader_Id' AND object_id = OBJECT_ID('IPHeader'))
BEGIN
	CREATE INDEX [IX_IPHeader_Id] ON [IPHeader]([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_IPHeader_IPSource' AND object_id = OBJECT_ID('IPHeader'))
BEGIN
	CREATE INDEX [IX_IPHeader_IPSource] ON [IPHeader]([IPSource])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_IPHeader_IPDestination' AND object_id = OBJECT_ID('IPHeader'))
BEGIN
	CREATE INDEX [IX_IPHeader_IPDestination] ON [IPHeader]([IPDestination])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_IPHeader_IPSourceLocationId' AND object_id = OBJECT_ID('IPHeader'))
BEGIN
	CREATE INDEX [IX_IPHeader_IPSourceLocationId] ON [IPHeader]([IPSourceLocationId])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_IPHeader_IPDestinationLocationId' AND object_id = OBJECT_ID('IPHeader'))
BEGIN
	CREATE INDEX [IX_IPHeader_IPDestinationLocationId] ON [IPHeader]([IPDestinationLocationId])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Search]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Search]
	( 
		[Id]	    		INT IDENTITY(1,1) NOT NULL,  
		[Name]				VARCHAR(64) NOT NULL,
		[CreatedBy]			INT NOT NULL,
		[CreatedOn]			DATETIME DEFAULT GETDATE() NOT NULL,
		[SourceIp]          VARBINARY(16),
		[DestinationIp]     VARBINARY(16),
		[SignatureId]       NUMERIC(10,0),
		[SignatureClassificationId]       NUMERIC(10,0),
		[PacketType]        VARCHAR(4),
		[SourcePort]        INT,
		[DestinationPort]   INT,
		[StartSearch]       DATETIME,
		[EndSearch]         DATETIME,
		CONSTRAINT [PK_Search_Id] PRIMARY KEY ([Id]),
		CONSTRAINT [UC_Search] UNIQUE ([Name]),
		CONSTRAINT [FK_Search_Signature]	FOREIGN KEY ([SignatureId])	REFERENCES [Signature]([Id]),
		CONSTRAINT [FK_Search_SignatureClassification]	FOREIGN KEY ([SignatureClassificationId])	REFERENCES [SignatureClassification]([ClassificationId]),
		--CONSTRAINT [FK_Search_CreatedBy]	FOREIGN KEY ([CreatedBy])	REFERENCES [Users]([Id])		
	)	
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Search_Id' AND object_id = OBJECT_ID('Search'))
BEGIN
	CREATE INDEX [IX_Search_Id] ON [Search]([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Search_Name' AND object_id = OBJECT_ID('Search'))
BEGIN
	CREATE INDEX [IX_Search_Name] ON [Search]([Name])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Search_CreatedBy' AND object_id = OBJECT_ID('Search'))
BEGIN
	CREATE INDEX [IX_Search_CreatedBy] ON [Search]([CreatedBy])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Search_CreatedOn' AND object_id = OBJECT_ID('Search'))
BEGIN
	CREATE INDEX [IX_Search_CreatedOn] ON [Search]([CreatedOn])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Search_SourceIp' AND object_id = OBJECT_ID('Search'))
BEGIN
	CREATE INDEX [IX_Search_SourceIp] ON [Search]([SourceIp])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Search_DetinationIp' AND object_id = OBJECT_ID('Search'))
BEGIN
	CREATE INDEX [IX_Search_DetinationIp] ON [Search]([DestinationIp])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Search_SignatureId' AND object_id = OBJECT_ID('Search'))
BEGIN
	CREATE INDEX [IX_Search_SignatureId] ON [Search]([SignatureId])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Search_SignatureClassificationId' AND object_id = OBJECT_ID('Search'))
BEGIN
	CREATE INDEX [IX_Search_SignatureClassificationId] ON [Search]([SignatureClassificationId])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Search_PacketType' AND object_id = OBJECT_ID('Search'))
BEGIN
	CREATE INDEX [IX_Search_PacketType] ON [Search]([PacketType])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Search_SourcePort' AND object_id = OBJECT_ID('Search'))
BEGIN
	CREATE INDEX [IX_Search_SourcePort] ON [Search]([SourcePort])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Search_DestinationPort' AND object_id = OBJECT_ID('Search'))
BEGIN
	CREATE INDEX [IX_Search_DestinationPort] ON [Search]([DestinationPort])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Search_StartSearch' AND object_id = OBJECT_ID('Search'))
BEGIN
	CREATE INDEX [IX_Search_StartSearch] ON [Search]([StartSearch])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Search_EndSearch' AND object_id = OBJECT_ID('Search'))
BEGIN
	CREATE INDEX [IX_Search_EndSearch] ON [Search]([EndSearch]) 
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Users]
	( 
		[Id]	    		INT IDENTITY(1,1) NOT NULL,  
		[Guid]              UNIQUEIDENTIFIER NOT NULL,
		[CreatedOn]			DATETIME DEFAULT GETDATE(),
		[Password]			VARCHAR (128) NOT NULL,
		[Email]				VARCHAR (512) NULL,
		[FirstName]			VARCHAR (512) NULL,
		[LastName]			VARCHAR (512) NULL,
		[ActiveSearch]	    INT NULL,
		CONSTRAINT [FK_Users_Search]	FOREIGN KEY ([ActiveSearch])	REFERENCES [Search]([Id]),	
		CONSTRAINT [PK_Users_Id] PRIMARY KEY ([Id]),
		CONSTRAINT [UC_Email] UNIQUE ([Email])
	)	
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Users_Id' AND object_id = OBJECT_ID('Users'))
BEGIN
	CREATE INDEX [IX_Users_Id] ON [Users]([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Users_Guid' AND object_id = OBJECT_ID('Users'))
BEGIN
	CREATE INDEX [IX_Users_Guid] ON [Users]([Guid])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Users_CreatedOn' AND object_id = OBJECT_ID('Users'))
BEGIN
	CREATE INDEX [IX_Users_CreatedOn] ON [Users]([CreatedOn])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Users_Email' AND object_id = OBJECT_ID('Users'))
BEGIN
	CREATE INDEX [IX_Users_Email] ON [Users]([Email])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Users_FirstName' AND object_id = OBJECT_ID('Users'))
BEGIN
	CREATE INDEX [IX_Users_FirstName] ON [Users]([FirstName])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Users_LastName' AND object_id = OBJECT_ID('Users'))
BEGIN
	CREATE INDEX [IX_Users_LastName] ON [Users]([LastName])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Users_ActiveSearch' AND object_id = OBJECT_ID('Users'))
BEGIN
	CREATE INDEX [IX_Users_ActiveSearch] ON [Users]([ActiveSearch])
END 
------------------------------------------------------------------------------------------------------------------------------------
IF NOT EXISTS 
(
    SELECT * FROM sys.foreign_key_columns fk 
    INNER JOIN sys.columns pc ON pc.object_id = fk.parent_object_id AND pc.column_id = fk.parent_column_id 
    INNER JOIN sys.columns rc ON rc.object_id = fk.referenced_object_id AND rc.column_id = fk.referenced_column_id
    WHERE fk.parent_object_id = object_id('Search') AND pc.name = 'CreatedBy'
    AND fk.referenced_object_id = object_id('Users') AND rc.NAME = 'Id'
)
BEGIN
	ALTER TABLE [Search] ADD CONSTRAINT FK_Search_CreatedBy FOREIGN KEY ([CreatedBy]) REFERENCES [Users]([Id])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NLog]') AND type in (N'U'))
BEGIN
	CREATE TABLE NLog(    
		[Id]				INT IDENTITY(1,1) NOT NULL,
		[TimeStamp]			DATETIME ,
		[LogLevel]			VARCHAR(16),
		[Logger]			VARCHAR(64), 
		[Message]			VARCHAR(8000),
		[MachineName]		VARCHAR(32),
		[UserName]			VARCHAR(64), 
		[Method]			VARCHAR(512),
		[Thread]			VARCHAR(100),
		[Exception]			VARCHAR(8000),
		[StackTrace]		VARCHAR(8000),
		CONSTRAINT [PK_NLog_Id]     PRIMARY KEY ([Id])
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_NLog_Id' AND object_id = OBJECT_ID('NLog'))
BEGIN
	CREATE INDEX [IX_NLog_Id] ON [NLog] ([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_NLog_TimeStamp' AND object_id = OBJECT_ID('NLog'))
BEGIN
	CREATE INDEX [IX_NLog_TimeStamp] ON [NLog] ([TimeStamp])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_NLog_LogLevel' AND object_id = OBJECT_ID('NLog'))
BEGIN
	CREATE INDEX [IX_NLog_LogLevel] ON [NLog] ([LogLevel])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_NLog_Logger' AND object_id = OBJECT_ID('NLog'))
BEGIN
	CREATE INDEX [IX_NLog_Logger] ON [NLog] ([Logger])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_NLog_MachineName' AND object_id = OBJECT_ID('NLog'))
BEGIN
	CREATE INDEX [IX_NLog_MachineName] ON [NLog] ([MachineName])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_NLog_UserName' AND object_id = OBJECT_ID('NLog'))
BEGIN
	CREATE INDEX [IX_NLog_UserName] ON [NLog] ([UserName])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_NLog_Method' AND object_id = OBJECT_ID('NLog'))
BEGIN
	CREATE INDEX [IX_NLog_Method] ON [NLog] ([Method])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
	CREATE TABLE [Roles]
	(
		[Id]		  INT IDENTITY(1,1)   NOT NULL,  
		[Name]		  		  VARCHAR(255),
		[Description]		  VARCHAR(512),
		CONSTRAINT [PK_Roles_Id] PRIMARY KEY ([Id]),
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Roles_Id' AND object_id = OBJECT_ID('Roles'))
BEGIN
	CREATE INDEX [IX_Roles_Id] ON [Roles] ([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Roles_Name' AND object_id = OBJECT_ID('Roles'))
BEGIN
	CREATE INDEX [IX_Roles_Name] ON [Roles] ([Name])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_Roles_Description' AND object_id = OBJECT_ID('Roles'))
BEGIN
	CREATE INDEX [IX_Roles_Description] ON [Roles] ([Description])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRoles]') AND type in (N'U'))
BEGIN
	CREATE TABLE [UserRoles]
	(    
		[UserId]	INT NOT NULL,
		[RoleId]	INT NOT NULL,
		CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([UserId],[RoleId]),
		CONSTRAINT [UQ_UserRolesReversePK] UNIQUE ([RoleId], [UserId]),
		CONSTRAINT [FK_UserRoles_User]	FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
		CONSTRAINT [FK_UserRoles_Role]	FOREIGN KEY ([RoleId]) REFERENCES [Roles]([Id])
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_UserRoles_UserId_RoleId' AND object_id = OBJECT_ID('UserRoles'))
BEGIN
	CREATE INDEX [IX_UserRoles_UserId_RoleId] ON [UserRoles] ([UserId],[RoleId])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGroups]') AND type in (N'U'))
BEGIN
	CREATE TABLE [UserGroups]
	(
		[Id]				  INT IDENTITY(1,1)   NOT NULL,  
		[Name]				  VARCHAR(255),
		[Description]		  VARCHAR(512),
		CONSTRAINT [PK_UserGroups_Id] PRIMARY KEY ([Id]),
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_UserGroups_Id' AND object_id = OBJECT_ID('UserGroups'))
BEGIN
	CREATE INDEX [IX_UserGroups_Id] ON [UserGroups] ([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_UserGroups_Name' AND object_id = OBJECT_ID('UserGroups'))
BEGIN
	CREATE INDEX [IX_UserGroups_Name] ON [UserGroups] ([Name])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserUserGroups]') AND type in (N'U'))
BEGIN
	CREATE TABLE [UserUserGroups]
	(    
		[UserId]		INT NOT NULL,
		[UserGroupId]	INT NOT NULL,
		CONSTRAINT [PK_UserUserGroups] PRIMARY KEY CLUSTERED ([UserId],[UserGroupId]),
		CONSTRAINT [UQ_UserUserGroupReversePK] UNIQUE ([UserGroupId], [UserId]),
		CONSTRAINT [FK_UserUserGroups_User]	FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
		CONSTRAINT [FK_UserUserGroups_UserGroups]	FOREIGN KEY ([UserGroupId]) REFERENCES [UserGroups]([Id])
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_UserUserGroups_UserId_RoleId' AND object_id = OBJECT_ID('UserUserGroups'))
BEGIN
	CREATE INDEX [IX_UserUserGroups_UserId_RoleId] ON [UserUserGroups] ([UserId],[UserGroupId])
END
------------------------------------------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventComments]') AND type in (N'U'))
BEGIN
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
		CONSTRAINT [PK_EventComments_Id]		PRIMARY KEY ([Id]) 
	)
END	

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_EventComments_Id' AND object_id = OBJECT_ID('EventComments'))
BEGIN
	CREATE INDEX [IX_EventComments_Id] ON [EventComments] ([Id])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_EventComments_Comment' AND object_id = OBJECT_ID('EventComments'))
BEGIN
	CREATE INDEX [IX_EventComments_Comment] ON [EventComments] ([Comment])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_EventComments_CreatedBy' AND object_id = OBJECT_ID('EventComments'))
BEGIN
	CREATE INDEX [IX_EventComments_CreatedBy] ON [EventComments] ([CreatedBy])
END

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_EventComments_CreatedOn' AND object_id = OBJECT_ID('EventComments'))
BEGIN
	CREATE INDEX [IX_EventComments_CreatedOn] ON [EventComments] ([CreatedOn])
END
------------------------------------------------------------------------------------------------------------------------------------







