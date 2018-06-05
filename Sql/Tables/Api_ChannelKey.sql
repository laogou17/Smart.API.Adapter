CREATE TABLE [dbo].[Api_ChannelKey] (
    [AccessId]  VARCHAR (50)     NOT NULL,
    [AccessKey] UNIQUEIDENTIFIER NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接入渠道密钥', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_ChannelKey', @level2type = N'COLUMN', @level2name = N'AccessKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接入渠道编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_ChannelKey', @level2type = N'COLUMN', @level2name = N'AccessId';


GO
EXECUTE sp_addextendedproperty @name = N'Ms_description', @value = N'Api接入渠道密钥信息。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_ChannelKey';

