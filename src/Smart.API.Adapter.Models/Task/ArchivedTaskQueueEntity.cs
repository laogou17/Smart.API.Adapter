
using System;

namespace Smart.API.Adapter.Models
{
	public class ArchivedTaskQueueEntity
	{
		private int m_TaskId;
		private byte m_Type;
		private byte m_TaskStatus;
		private byte m_Priority;
		private DateTime m_CreatedTime;
		private DateTime? m_ExecutionStartTime;
		private DateTime? m_ExecutionEndTime;
		private DateTime m_ArchivedTime;
		private Guid m_rowguid;

		/// <summary>
		/// 任务编码
		/// </summary>
		public int TaskId
		{
			get { return m_TaskId; }
			set { m_TaskId = value; }
		}

		/// <summary>
		/// 任务类型
		/// </summary>
		public byte Type
		{
			get { return m_Type; }
			set { m_Type = value; }
		}

		/// <summary>
		/// 任务状态
		/// </summary>
		public byte TaskStatus
		{
			get { return m_TaskStatus; }
			set { m_TaskStatus = value; }
		}

		/// <summary>
		/// 优先级
		/// </summary>
		public byte Priority
		{
			get { return m_Priority; }
			set { m_Priority = value; }
		}

		/// <summary>
		/// 任务创建时间
		/// </summary>
		public DateTime CreatedTime
		{
			get { return m_CreatedTime; }
			set { m_CreatedTime = value; }
		}

		/// <summary>
		/// 任务执行时间
		/// </summary>
		public DateTime? ExecutionStartTime
		{
			get { return m_ExecutionStartTime; }
			set { m_ExecutionStartTime = value; }
		}

		/// <summary>
		/// 任务完成时间
		/// </summary>
		public DateTime? ExecutionEndTime
		{
			get { return m_ExecutionEndTime; }
			set { m_ExecutionEndTime = value; }
		}

		/// <summary>
		/// 任务归档时间
		/// </summary>
		public DateTime ArchivedTime
		{
			get { return m_ArchivedTime; }
			set { m_ArchivedTime = value; }
		}

		/// <summary>
		/// rowguid
		/// </summary>
		public Guid rowguid
		{
			get { return m_rowguid; }
			set { m_rowguid = value; }
		}

	}
}

