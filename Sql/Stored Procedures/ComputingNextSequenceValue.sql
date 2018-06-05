/*
 * 调用该存储过程，返回指定序列的下一个值，该值由序列当前值加上序列步长所得。
 * 当下一个值大于序列允许的最大值后，该序列将被重置为初始值加上序列步长。
 * 如果指定的序列不存在时，将会抛出异常。
*/
CREATE PROCEDURE [dbo].[ComputingNextSequenceValue]
	@name varchar(50) -- 序列名称
AS
	DECLARE @SequenceId INT;
	DECLARE @InitValue INT;
	DECLARE @CurrentValue BIGINT;
	DECLARE @MaxValue BIGINT;
	DECLARE @Step INT;

	BEGIN TRANSACTION
	 
	SELECT @SequenceId = SequenceId,
			@InitValue = InitValue,
			@CurrentValue = CurrentValue,
			@MaxValue = MaxValue,
			@Step = Step
	FROM [dbo].[Sequence] WITH(ROWLOCK,XLOCK)
	WHERE [Name] = @name;

	IF(@SequenceId IS NULL)
	BEGIN
		ROLLBACK TRANSACTION;
		DECLARE @ERRMSG NVARCHAR(50);
		SET @ERRMSG = N'指定的序列 “' + @name + N'” 不存在。';
		RAISERROR(@ERRMSG,16,1);
		RETURN;
	END
	
	IF(@CurrentValue < @InitValue)
	BEGIN
		SET @CurrentValue = @InitValue;
	END

	SET @CurrentValue = @CurrentValue + @Step;

	IF((@MaxValue > 0) AND (@CurrentValue > @MaxValue))
	BEGIN
		SET @CurrentValue = @InitValue + @Step;
	END

	UPDATE [dbo].[Sequence] 
	SET [CurrentValue] = @CurrentValue
	WHERE [SequenceId] = @SequenceId;

	COMMIT TRANSACTION;
	SELECT @CurrentValue;