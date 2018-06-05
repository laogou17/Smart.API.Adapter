namespace Smart.API.Adapter.Models {
	/// <summary>
	/// 后台异步任务类型。
	/// </summary>
	public enum TaskType : byte {
		/// <summary>
		/// 入场识别记录
		/// </summary>
		InRecognizingData = 0,

        /// <summary>
        /// 入场过闸记录
        /// </summary>
        InData = 1,

        /// <summary>
        /// 出场识别记录
        /// </summary>
        OutRecognizingData = 2,

        /// <summary>
        /// 出场过闸记录
        /// </summary>
        OutData = 3,
		
	}

	/// <summary>
	/// 后台异步任务状态。
	/// </summary>
	public enum TaskStatus : byte {
		/// <summary>
		/// 新建
		/// </summary>
		Created = 0,
		/// <summary>
		/// 运行中
		/// </summary>
		Running = 1,
		/// <summary>
		/// 执行成功
		/// </summary>
		RanToCompletion = 2,
		/// <summary>
		/// 执行失败
		/// </summary>
		Faulted = 3,
		/// <summary>
		/// 已归档
		/// </summary>
		Archived = 4
	}

	/// <summary>
	/// 表示后台异步任务优先级。
	/// </summary>
	public enum TaskPriority : byte {
		/// <summary>
		/// 将任务安排在具有任何其他优先级之后。
		/// </summary>
		Lowest = 0,
		/// <summary>
		/// 将任务安排在具有 Normal 优先级之后，在具有 Lowest 优先级之前。
		/// </summary>
		BelowNormal = 1,
		/// <summary>
        /// 将任务安排在具有 AboveNormal 优先级之后，在具有 BelowNormal 优先级之前。默认情况下，具有 Normal 优先级。
		/// </summary>
		Normal = 2,
		/// <summary>
		/// 将任务安排在具有 Highest 优先级之后，在具有 Normal 优先级之前。
		/// </summary>
		AboveNormal = 3,
		/// <summary>
		/// 将任务安排在具有任何其他优先级之前。
		/// </summary>
		Highest = 4
	}
}
