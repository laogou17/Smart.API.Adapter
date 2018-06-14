CREATE TABLE [dbo].[ParkDic](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KeyStr] [varchar](50) NULL,
	[ValueStr] [varchar](100) NULL,
 CONSTRAINT [PK_ParkDic] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


insert into [ParkDic] (KeyStr,ValueStr) values ('Version','0')
insert into [ParkDic] (KeyStr,ValueStr) values ('OverFlowCount','0')
