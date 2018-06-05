using Smart.API.Adapter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Smart.API.Adapter.DataAccess.Task
{
    public class TaskQueueDAL : DataBase
    {
        private static TaskQueueDAL _ProxyInstance = null;
        private static object _ProxyInstanceLock = new object();

        /// <summary>
        /// 隐藏构造函数，禁止通过 new 创建新对象
        /// </summary>
        protected TaskQueueDAL()
            : base(DbName.SmartAPIAdapterCore) { }
        /// <summary>
        /// 获取该类的唯一实例对象
        /// </summary>
        public static TaskQueueDAL ProxyInstance
        {
            get
            {
                if (_ProxyInstance == null)
                {
                    lock (_ProxyInstanceLock)
                    {
                        if (_ProxyInstance == null)
                        {
                            _ProxyInstance = new TaskQueueDAL();
                        }
                    }
                }
                return _ProxyInstance;
            }
        }


        /// <summary>
        /// 插入新任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public int Insert(string Content, int taskType)
        {

            string sql = "insert into dbo.TaskQueue (tasktype,status,priority,content,callbackurl) values ({0},0,2,'{1}','')";
            sql = string.Format(sql, taskType, Content);
            return ExecuteNoQueryBySql(sql, null);
        }

        /// <summary>
        /// 更新任务状态，重新执行
        /// </summary>
        /// <param name="ReSecond"></param>
        /// <returns></returns>
        public int ReTaskExecution(int ReSecond)
        {
            string sql = "update dbo.TaskQueue set status=0 where status=1 and DATEDIFF(SECOND,ExecutionStartTime,GETDATE())>{0}";
            sql = string.Format(sql, ReSecond);
            return ExecuteNoQueryBySql(sql, null);
        }

        /// <summary>
        /// 获取待办任务
        /// </summary>
        /// <param name="taskType"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public TaskQueueEntity GetTaskWithLock(byte taskType, byte priority)
        {
            using (DbCommand cmd = db.GetStoredProcCommand("DequeueTask"))
            {
                db.AddInParameter(cmd, "@status", DbType.Int32, 0);
                db.AddInParameter(cmd, "@taskType", DbType.Byte, taskType);
                db.AddInParameter(cmd, "@priority", DbType.Byte, priority);

                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0].ToObject<TaskQueueEntity>();
                else
                    return null;
            }
        }

        /// <summary>
        /// 设置任务执行完毕。
        /// </summary>
        /// <param name="taskId">任务编码</param>
        /// <param name="successful">任务执行结果。</param>
        /// <returns></returns>
        public bool SetTaskCompleted(int taskId, bool successful)
        {
            string query = "update [dbo].[TaskQueue] set [Status]=@Status,[ExecutionEndTime]=GETDATE() where [TaskId]=@TaskId ";
            using (DbCommand cmd = db.GetSqlStringCommand(query))
            {
                db.AddInParameter(cmd, "@TaskId", DbType.Int32, taskId);
                db.AddInParameter(cmd, "@Status", DbType.Byte,
                    successful ? (byte)TaskStatus.RanToCompletion : (byte)TaskStatus.Faulted);
                return db.ExecuteNonQuery(cmd) > 0;
            }
        }

        /// <summary>
        /// 设置任务归档
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public bool SetTaskArchived(int taskId)
        {
            string sql = @"insert into dbo.ArchivedTaskQueue (TaskId,TaskType,Status,Priority,Content,CallbackUrl,CreatedTime,ExecutionStartTime,ExecutionEndTime,ArchivedTime,rowguid) 
                           select TaskId, TaskType,Status,Priority,Content,CallbackUrl,CreatedTime,ExecutionStartTime,ExecutionEndTime,GETDATE() as ArchivedTime,rowguid from dbo.TaskQueue with(nolock) where TaskId=@TaskId;
                           delete dbo.TaskQueue where TaskId= @TaskId ";

            using (DbCommand cmd = db.GetSqlStringCommand(sql))
            {
                db.AddInParameter(cmd, "@TaskId", DbType.Int32, taskId);
                return db.ExecuteNonQuery(cmd) > 0;
            }
        }

        public bool IsFinish(int TaskId)
        {
            string query = " select top 1 Status from TaskQueue with(nolock) where TaskId=@TaskId ";
            using (DbCommand cmd = db.GetSqlStringCommand(query))
            {
                db.AddInParameter(cmd, "@TaskId", DbType.Int32, TaskId);

                int status = 0;
                object o = db.ExecuteScalar(cmd);

                if (o != null
                    && Int32.TryParse(o.ToString(), out status)
                    && (status == (int)Models.TaskStatus.RanToCompletion
                    || status == (int)Models.TaskStatus.Faulted))
                    return true;
                else
                    return false;
            }
        }
    }
}
