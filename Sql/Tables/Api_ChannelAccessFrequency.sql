CREATE TABLE [dbo].[Api_ChannelAccessFrequency] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [FunctionId]      INT          NOT NULL,
    [AccessId]        VARCHAR (4)  NOT NULL,
    [AccessFrequency] NVARCHAR (8) NOT NULL,
    [AccessLimit]     INT          NOT NULL,
    [AccessCount]     INT          NULL,
    [AccessTime]      DATETIME     NULL,
    CONSTRAINT [PK_Api_ChannelAccessFrequency] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后访问时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_ChannelAccessFrequency', @level2type = N'COLUMN', @level2name = N'AccessTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位时间内已累计访问的次数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_ChannelAccessFrequency', @level2type = N'COLUMN', @level2name = N'AccessCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'限定访问次数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_ChannelAccessFrequency', @level2type = N'COLUMN', @level2name = N'AccessLimit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'限定访问频率 Minute-分钟 Hour-小时 Day-天 Week-周', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_ChannelAccessFrequency', @level2type = N'COLUMN', @level2name = N'AccessFrequency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接入渠道编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_ChannelAccessFrequency', @level2type = N'COLUMN', @level2name = N'AccessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接口ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_ChannelAccessFrequency', @level2type = N'COLUMN', @level2name = N'FunctionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，自增长', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_ChannelAccessFrequency', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'Ms_Description', @value = N'Api接入渠道访问频率控制。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_ChannelAccessFrequency';

