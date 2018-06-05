CREATE TABLE [dbo].[Api_ChannelFunction] (
    [AccessId]   VARCHAR (4) NOT NULL,
    [FunctionId] INT         NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接入渠道拥有的接口权限。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_ChannelFunction';

