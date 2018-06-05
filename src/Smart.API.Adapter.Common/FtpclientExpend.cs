using System;
using System.IO;
using System.Threading;
using Infrastructure.Common.Ftp;

namespace Smart.API.Adapter.Common
{

    #region FTP扩展类

    /// <summary>
    /// FTP扩展类
    /// </summary>
    public class FtpclientExpend : FTPclient
    {
        public int TimeOutTimes = 3;
        public int TimeOutseconds = 2;

        #region 删除本地文件

        public void DeleteLocalFile(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }

        #endregion

        #region 递归创建文件夹

        public new bool FtpCreateDirectory(string path)
        {
            string tempPath = path.EndsWith("/") ? path : path + "/";
            if (!base.FtpCreateDirectory(path))
            {
                if (path.IndexOf("/") < 0) return true;
                FtpCreateDirectory(path.Substring(0, path.LastIndexOf("/")));
                return base.FtpCreateDirectory(tempPath);
            }
            return true;
        }
        #endregion

        #region 下载文件的时候覆盖本地文件

        public bool Download(string source, string local)
        {
            try
            {
                //本地存在则删除
                DeleteLocalFile(local);
                bool boolTemp = Download(source, local, false);
                InitTry();
                return boolTemp;
            }
            catch (Exception)
            {
                if (TimeOutTimes > 0)
                {
                    TimeOutTimes--;
                    Thread.Sleep(TimeOutseconds * 1000);
                    EventConsoleMsg(string.Format("下载文件重试:source:{0},local:{1}:还剩{2}次,执行时间:{3}.", source, local, TimeOutTimes, DateTime.Now));
                    return Download(source, local);
                }
                throw;
            }
        }

        #endregion

        #region 上传(允许覆盖服务器文件)

        public new bool Upload(string source, string target)
        {
            try
            {
                if (!FtpDelete(target)) return false;
                bool boolTemp = base.Upload(source, target + ".Uploading") && FtpRename(target + ".Uploading", target);
                InitTry();
                return boolTemp;
            }
            catch (Exception)
            {
                if (TimeOutTimes > 0)
                {
                    TimeOutTimes--;
                    Thread.Sleep(TimeOutseconds * 1000);
                    EventConsoleMsg(string.Format("上传文件重试:source:{0},target:{1}:还剩{2}次,执行时间:{3}.", source, target, TimeOutTimes, DateTime.Now));
                    return Upload(source, target);
                }
                throw;
            }
        }

        #endregion

        #region 删除(已删则除返回true)

        public new bool FtpDelete(string fileName)
        {
            try
            {
                if (!FtpFileExists(fileName)) return true;
                bool boolTemp = base.FtpDelete(fileName);
                InitTry();
                return boolTemp;
            }
            catch (Exception)
            {
                if (TimeOutTimes > 0)
                {
                    TimeOutTimes--;
                    Thread.Sleep(TimeOutseconds * 1000);
                    EventConsoleMsg(string.Format("删除文件重试:fileName:{0}:还剩{1}次,执行时间:{2}.", fileName, TimeOutTimes, DateTime.Now));
                    return FtpDelete(fileName);
                }
                throw;
            }
        }

        #endregion

        public FtpclientExpend(string hostname, string username, string password)
            : base(hostname, username, password)
        {
			// 启用被动模式链接
			UsePassive = true;
        }

        public bool CheckConn()
        {
            try
            {
                bool boolTemp = FtpDirectoryExists("/");
                  InitTry();
                return boolTemp;
            }
            catch (Exception)
            {
                if (TimeOutTimes > 0)
                {
                    TimeOutTimes--;
                    Thread.Sleep(TimeOutseconds * 1000);
                   // EventConsoleMsg(string.Format("CheckConn:还剩{0}次,执行时间:{1}.", TimeOutTimes, DateTime.Now));
                    return CheckConn();
                }
                return false;
            }
        }

        /// <summary>
        /// 重置重试次数
        /// </summary>
        public void InitTry()
        {
            TimeOutTimes = 3;
        }

        public event ConsoleMsg EventConsoleMsg;

        protected virtual void OnEventConsoleMsg(string errMsg)
        {

        }

        public delegate void ConsoleMsg(string errMsg);
    }

    #endregion
}