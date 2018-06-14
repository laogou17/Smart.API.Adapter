using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Smart.API.Adapter.Biz;

namespace Smart.API.Adapter.TaskService
{
    partial class InterfaceTimerService : ServiceBase
    {
        public InterfaceTimerService()
        {
            InitializeComponent();
        }

        public void Start(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            //启动服务：初始化包括心跳和更新车位总数            
            HeartService.GetInstance().Start();
        }

        protected override void OnStop()
        {
            // TODO:  在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
