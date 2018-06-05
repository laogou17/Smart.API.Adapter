using Smart.API.Adapter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.TaskService
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.RegisterLog4Config(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\Log4net.config");
            //LogHelper.Error("1235");
            LogHelper.Info("1235");
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += currentDomain_UnhandledException;
            if (System.Environment.UserInteractive)
            {
                if (args != null && args.Length > 0 && args[0] != null && "/dev".Equals(args[0], StringComparison.OrdinalIgnoreCase))
                {
                    RunUserInteractiveService();
                    Console.WriteLine("启用dev调试模式成功");
                    Console.Read();
                }
                else
                {
                    Console.WriteLine("请通过安装Windows服务运行此应用");
                    Console.Read();
                }
            }
            else
            {
                RunBackgroundService();
            }
        }

        static void currentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = (Exception)e.ExceptionObject;
            if (System.Environment.UserInteractive)
            {
                Console.WriteLine("there is an error occurred:{0}", error.Message);
            }
            else
            {
                //写日志
            }
        }

        static void RunUserInteractiveService()
        {
            //TODO:
        }

        static void RunBackgroundService()
        {
            //TODO:
        }
    }
}
