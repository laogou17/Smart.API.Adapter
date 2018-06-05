-- =============================================
-- Description:	从任务数据库队列中提取一条未处理状态的任务
--              并将其处理状态更新为处理中
-- =============================================
CREATE PROCEDURE [dbo].[DequeueTask](@status tinyint,@priority tinyint)
AS
BEGIN

    declare @taskId int;
	declare @output table(
		[TaskId] INT,
		 [TaskType] int,
		[Content] XML,           
		[CallbackUrl] NVARCHAR (1024)
	);			

	update top(1) [dbo].TaskQueue with(updlock,readpast)
	set [Status] = 1,[ExecutionStartTime] = GETDATE()
	output inserted.[TaskId]
			,inserted.[TaskType]
			,inserted.[Content]
			,inserted.[CallbackUrl] into @output
	from [dbo].TaskQueue 
	where   [Status] = @status 
			and [Priority] = @priority;

	select * from @output;
END