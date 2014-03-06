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
GO
CREATE INDEX [IX_Flags_Id] ON [Flags] ([Id])

GO
CREATE INDEX [IX_Flags_Number] ON [Flags] ([Number])