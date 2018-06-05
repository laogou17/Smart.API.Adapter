CREATE TABLE [dbo].[TaskQueue] (
    [TaskId]             INT              IDENTITY (1, 1) NOT NULL,
    [TaskType]           TINYINT          NOT NULL,
    [Status]             TINYINT          NOT NULL,
    [Priority]           TINYINT          DEFAULT ((0)) NOT NULL,
    [Content]            XML              NOT NULL,
    [CallbackUrl]        NVARCHAR (1024)  NULL,
    [CreatedTime]        DATETIME         DEFAULT (getdate()) NOT NULL,
    [ExecutionStartTime] DATETIME         NULL,
    [ExecutionEndTime]   DATETIME         NULL,
    [rowguid]            UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_TASKQUEUE] PRIMARY KEY CLUSTERED ([TaskId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'rowguid', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TaskQueue', @level2type = N'COLUMN', @level2name = N'rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务完成时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TaskQueue', @level2type = N'COLUMN', @level2name = N'ExecutionEndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务执行时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TaskQueue', @level2type = N'COLUMN', @level2name = N'ExecutionStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TaskQueue', @level2type = N'COLUMN', @level2name = N'CreatedTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'优先级', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TaskQueue', @level2type = N'COLUMN', @level2name = N'Priority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TaskQueue', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TaskQueue', @level2type = N'COLUMN', @level2name = N'TaskType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TaskQueue', @level2type = N'COLUMN', @level2name = N'TaskId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'待执行任务队列', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TaskQueue';

