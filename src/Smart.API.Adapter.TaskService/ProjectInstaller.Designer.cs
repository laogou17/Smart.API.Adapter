namespace Smart.API.Adapter.TaskService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.InterfaceTimerService1 = new System.ServiceProcess.ServiceInstaller();
            this.InterfaceTaskService1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            // 
            // InterfaceTimerService1
            // 
            this.InterfaceTimerService1.Description = "定时推送数据到第三方";
            this.InterfaceTimerService1.DisplayName = "JieLink InterfaceTimerService";
            this.InterfaceTimerService1.ServiceName = "InterfaceTimerService";
            this.InterfaceTimerService1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // InterfaceTaskService1
            // 
            this.InterfaceTaskService1.Description = "实时推送数据到第三方";
            this.InterfaceTaskService1.DisplayName = "JieLink InterfaceTaskService";
            this.InterfaceTaskService1.ServiceName = "InterfaceTaskService";
            this.InterfaceTaskService1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.InterfaceTimerService1,
            this.InterfaceTaskService1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller InterfaceTimerService1;
        private System.ServiceProcess.ServiceInstaller InterfaceTaskService1;
    }
}