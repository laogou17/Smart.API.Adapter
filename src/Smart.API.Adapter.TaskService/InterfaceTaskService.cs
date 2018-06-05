using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Task;
using System.Threading.Tasks;
using Smart.API.Adapter.Common;
using Smart.API.Adapter.Biz;
using Smart.API.Adapter.Models;
using Smart.API.Adapter.TaskService.InterfaceService;

namespace Smart.API.Adapter.TaskService
{
    partial class InterfaceTaskService : ServiceBase
    {
        InterfaceTaskServiceThread taskThread = null;

        public InterfaceTaskService()
        {
            InitializeComponent();
        }

        public void Start(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            if (taskThread == null)
            {
                taskThread = new InterfaceTaskServiceThread();
                //TODO:推送时间间隔更改为可配置
                int timeSpan = 1;

                taskThread.TaskIdleTime = TimeSpan.FromSeconds(timeSpan);
                taskThread.TaskBusyTime = TimeSpan.FromMilliseconds(100);
                taskThread.TaskExecuted += taskThread_TaskExecuted;
                taskThread.TaskBuildFaulted += taskThread_TaskBuildFaulted;
                taskThread.TaskExecuting += taskThread_TaskExecuting;
                taskThread.Start();
                LogHelper.Info("InterfaceTaskService,推送服务启动成功");
            }
        }

        void taskThread_TaskExecuting(object sender, TaskExecutingEventArgs e)
        {
            
        }

        void taskThread_TaskBuildFaulted(object sender, TaskBuildEventArgs e)
        {
            if (e.Exception != null)
            {
                LogHelper.Error("InterfaceTaskService,推送服务Faulted错误："+e.Exception);
            }
        }

        void taskThread_TaskExecuted(object sender, TaskExecuteEventArgs e)
        {
            if (e.Exception != null)
            {
                LogHelper.Error("InterfaceTaskService,推送服务Executed错误：" + e.Exception);
            }
        }

        protected override void OnStop()
        {          
            if (taskThread != null)
            {
                taskThread.Stop();
                taskThread.Dispose();
                taskThread = null;
                LogHelper.Info("InterfaceTaskService,推送服务停止成功");
            }
        }


    }

    internal class InterfaceTaskServiceThread : BackgroundTaskThread
    {
        public InterfaceTaskServiceThread()
            : base("InterfaceServiceThread")
        {

        }

        /// <summary>
        /// 获取待推送的任务
        /// </summary>
        /// <returns></returns>
        protected override BackgroundTask GetTask()
        {
            TaskBLL taskBll = new TaskBLL();
            var taskQueue = taskBll.GetInterfaceTask();
            if (taskQueue != null)
            {
                var background = new BackgroundTask<TaskQueueEntity>();
                background.TaskData = taskQueue;
                return background;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 执行推送任务
        /// </summary>
        /// <param name="task"></param>
        protected override void ExecuteTask(BackgroundTask task)
        {
            if (task == null)
            {
                return;
            }

            BackgroundTask<TaskQueueEntity> taskNew = task as BackgroundTask<TaskQueueEntity>;
            if (taskNew != null && taskNew.TaskData != null)
            {
                SendServiceCreate.Instance.Send(taskNew.TaskData);
            }
        }
    }
}
