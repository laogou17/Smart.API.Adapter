
using System;

namespace Smart.API.Adapter.Models
{
    public class TaskQueueEntity
    {
        private int m_TaskId;
        private TaskType m_TaskType;
        private TaskStatus m_Status;
        private TaskPriority m_Priority;
        private DateTime m_CreatedTime;
        private DateTime? m_ExecutionStartTime;
        private DateTime? m_ExecutionEndTime;
        private Guid m_rowguid;
        private string m_Content;
        private string m_CallbackUrl;

        public TaskQueueEntity()
        {
        }

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
        public TaskType TaskType
        {
            get { return m_TaskType; }
            set { m_TaskType = value; }
        }

        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        /// <summary>
        /// 优先级
        /// </summary>
        public TaskPriority Priority
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
        /// rowguid
        /// </summary>
        public Guid rowguid
        {
            get { return m_rowguid; }
            set { m_rowguid = value; }
        }

        /// <summary>
        /// 任务执行数据
        /// </summary>
        public string Content
        {
            get { return m_Content; }
            set { m_Content = value; }
        }

        /// <summary>
        /// 回调地址
        /// </summary>
        public string CallbackUrl
        {
            get { return m_CallbackUrl; }
            set { m_CallbackUrl = value; }
        }
    }

    public class TaskQueueEntity<T> : TaskQueueEntity
    {
        public T Data
        {
            get
            {
                if (string.IsNullOrEmpty(Content))
                {
                    return default(T);
                }
                return Content.ToObject<T>();
            }
        }
    }
}

