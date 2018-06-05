CREATE TABLE [dbo].[Sequence] (
    [SequenceId]   INT          IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (50) NOT NULL,
    [InitValue]    BIGINT       DEFAULT ((0)) NOT NULL,
    [CurrentValue] BIGINT       DEFAULT ((0)) NOT NULL,
    [MaxValue]     BIGINT       DEFAULT ((0)) NOT NULL,
    [Step]         INT          DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SEQUENCE] PRIMARY KEY CLUSTERED ([SequenceId] ASC),
    CONSTRAINT [IX_Sequence_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'序列步长', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sequence', @level2type = N'COLUMN', @level2name = N'Step';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'序列最大值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sequence', @level2type = N'COLUMN', @level2name = N'MaxValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'序列当前值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sequence', @level2type = N'COLUMN', @level2name = N'CurrentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'序列初始值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sequence', @level2type = N'COLUMN', @level2name = N'InitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'序列名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sequence', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'序列主键', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sequence', @level2type = N'COLUMN', @level2name = N'SequenceId';

