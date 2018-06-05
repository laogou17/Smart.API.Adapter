CREATE TABLE [dbo].[Api_Function] (
    [FunctionId]       INT             IDENTITY (1, 1) NOT NULL,
    [FunctionCode]     NVARCHAR (128)  NOT NULL,
    [FunctionName]     NVARCHAR (128)  NOT NULL,
    [FunctionCategory] NVARCHAR (128)  NOT NULL,
    [ProviderType]     INT             NOT NULL,
    [EnableStatus]     BIT             CONSTRAINT [DF__Api_Funct__Enabl__367C1819] DEFAULT ((1)) NOT NULL,
    [Remark]           NVARCHAR (1024) NULL,
    CONSTRAINT [PK_Api_Function] PRIMARY KEY CLUSTERED ([FunctionId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Function', @level2type = N'COLUMN', @level2name = N'Remark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'启用状态 0-未启用 1-启用（默认）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Function', @level2type = N'COLUMN', @level2name = N'EnableStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接口类型(用于区分不同版本)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Function', @level2type = N'COLUMN', @level2name = N'ProviderType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接口类别', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Function', @level2type = N'COLUMN', @level2name = N'FunctionCategory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接口名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Function', @level2type = N'COLUMN', @level2name = N'FunctionName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接口编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Function', @level2type = N'COLUMN', @level2name = N'FunctionCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接口ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Function', @level2type = N'COLUMN', @level2name = N'FunctionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Api接口信息。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Api_Function';

