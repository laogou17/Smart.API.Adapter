CREATE TABLE [dbo].[Api_Channel] (
    [AccessId]      VARCHAR (50)    NOT NULL,
    [ChannelName]   NVARCHAR (128)  NOT NULL,
    [ChannelType]   INT             CONSTRAINT [DF__Api_Chann__Chann__31B762FC] DEFAULT ((0)) NOT NULL,
    [ContactUser]   NVARCHAR (16)   NOT NULL,
    [ContactMobile] VARCHAR (11)    NOT NULL,
    [Remark]        NVARCHAR (1024) NULL,
    [EnableStatus]  BIT             CONSTRAINT [DF__Api_Chann__Enabl__32AB8735] DEFAULT ((1)) NOT NULL,
    [Creator]       NVARCHAR (16)   NOT NULL,
    [CreateTime]    DATETIME        NOT NULL,
    [ModifyUser]    NVARCHAR (16)   NULL,
    [ModifyTime]    DATETIME        NOT NULL,
    CONSTRAINT [PK_Api_Channel] PRIMARY KEY CLUSTERED ([AccessId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Channel', @level2type = N'COLUMN', @level2name = N'ModifyTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Channel', @level2type = N'COLUMN', @level2name = N'ModifyUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Channel', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Channel', @level2type = N'COLUMN', @level2name = N'Creator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'启用状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Channel', @level2type = N'COLUMN', @level2name = N'EnableStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Channel', @level2type = N'COLUMN', @level2name = N'Remark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Channel', @level2type = N'COLUMN', @level2name = N'ContactMobile';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Channel', @level2type = N'COLUMN', @level2name = N'ContactUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接入渠道类型 0-自有渠道 1-第三方渠道', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Channel', @level2type = N'COLUMN', @level2name = N'ChannelType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接入渠道名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Channel', @level2type = N'COLUMN', @level2name = N'ChannelName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接入渠道编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Channel', @level2type = N'COLUMN', @level2name = N'AccessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Api接入渠道信息。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Channel';

