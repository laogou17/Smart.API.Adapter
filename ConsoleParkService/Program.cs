using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Smart.API.Adapter.Biz;

namespace ConsoleParkService
{
    /// <summary>
    /// 心跳检测服务
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            HeartService heartService = new HeartService();
            heartService.Start();
            Console.ReadKey();
        }
    }
}
