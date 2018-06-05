-- =============================================
-- Description:	计算某个接入渠道接口访问频率是否超过限定配置值
-- =============================================
-- 未超过限制返回0,超过限制返回1
CREATE PROCEDURE [dbo].[ComputingAccessFrequency] 
	@accessId varchar(4), --接入渠道编码
	@functionCode nvarchar(128) --访问接口名称
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Define the local variables
	declare @Id int;
	declare @Frequency nvarchar(8);
	declare @AccessLimit int;
	declare @AccessCount int;
	declare @AccessTime datetime;

	declare @OverflowTime datetime;
	declare @CurrentTime datetime = getdate();
	

    -- Select the match record to local variables
	select @Id=a.[Id],@Frequency=a.[AccessFrequency],@AccessLimit=a.[AccessLimit],
			@AccessCount=ISNULL(a.[AccessCount],0),@AccessTime=a.[AccessTime]
	from [dbo].[Api_ChannelAccessFrequency] a with(nolock)
	inner join (select [FunctionId] from [dbo].[Api_Function] with(nolock) 
					where [FunctionCode] = @functionCode
	) b on a.[FunctionId] = b.[FunctionId]
	where a.[AccessId] = @accessId

	-- Return false as default, if there is not configured any values.
	if(@Id is null)
	begin
		return 0;
	end

	-- Process the first access.
	if(@AccessTime is null)
	begin
		-- record the first access time and set count value 1
		update [dbo].[Api_ChannelAccessFrequency] set [AccessCount] = 1,[AccessTime] = GETDATE() where [Id] = @Id;
		-- return false by the frist access.
		return 0; 
	end
	 
	-- Computing the overflow time by the access frequency.				
	set @OverflowTime = case(@Frequency) 
							when 'Minute' then dateadd(minute,1,@AccessTime)
							when 'Hour' then dateadd(hour,1,@AccessTime)
							when 'Day' then dateadd(day,1,@AccessTime)
							when 'Week' then dateadd(week,1,@AccessTime)
							else dateadd(SECOND,1,@CurrentTime)
						end;

	-- Reset access count & access time when the access frequency overflow.
	if(@CurrentTime>=@OverflowTime)
	begin
		update [dbo].[Api_ChannelAccessFrequency] set [AccessCount] = 1,[AccessTime] = GETDATE() where [Id] = @Id; 
		return 0;
	end
		 
	-- increment the access count.
	set @AccessCount = @AccessCount+1;
	update [dbo].[Api_ChannelAccessFrequency] set [AccessCount] = @AccessCount where [Id] = @Id; 
		 
	-- Determine if the access count overflow.
	if(@AccessCount>@AccessLimit)
	begin
		return 1;
	end
	else
	begin
		return 0;
	end
END