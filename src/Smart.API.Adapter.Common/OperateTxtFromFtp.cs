using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Infrastructure.Common.Ftp;

namespace Smart.API.Adapter.Common {
	#region 从FTP 获取、上传txt

	/// <summary>
	/// ftp 操作类
	/// </summary>
	public class OperateTxtFromFtp {
		/// <summary>
		/// ftp 扩展类
		/// </summary>
		public readonly FtpclientExpend FtpClient;

		/// <summary>
		/// 需要处理ftp文件路径
		/// </summary>
		public string Path;
		/// <summary>
		/// 本地路径
		/// </summary>
		public string LocalPath;
		/// <summary>
		/// 已处理文件夹路径
		/// </summary>
		public string DealPath;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ftpClient">ftp扩展类</param>
		/// <param name="path">需要处理ftp文件路径</param>
		/// <param name="localPath">本地路径</param>
		public OperateTxtFromFtp(FtpclientExpend ftpClient, string path, string localPath) {
			FtpClient = ftpClient;
			Path = path;
			LocalPath = localPath;
		}

		#region 读取文本

		/// <summary>
		/// 
		/// </summary>
		/// <param name="files">FTPfileInfo文件集合</param>
		/// <param name="dataHandler">处理每一行数据的方法</param>
		public void ReadTxt(List<FTPfileInfo> files, Action<string[]> dataHandler) {
			foreach(FTPfileInfo file in files) {
				string newFileName = file.Filename + ".deal";
				string newFileFullName = Path + "/" + newFileName;
				string dealFileFullName = DealPath + "/" + newFileName;
				try {
					FtpClient.FtpDelete(newFileFullName);//如果有已处理的文件，就删除

					//重命名,要么返回true,要么报错，报错则改回来。如果reName之前，文件被其他用户打开，其他用户保存的时候，将出现2个文件（deal、txt）
					if(FtpClient.FtpRename(Path + "/" + file.Filename, newFileFullName)) {
						//下载到客户端指定位置 (要么返回true,要么报错) ,客户端已存在文件则先删除, 掉线重试
						if(FtpClient.Download(newFileFullName, LocalPath + file.Filename)) {
							using(var fs = new FileStream(LocalPath + file.Filename, FileMode.Open, FileAccess.Read)) {
								using(var sr = new StreamReader(fs, Encoding.UTF8)) {
									List<string> batchSavePackage = new List<string>();
									while(sr.Peek() >= 0) {
										batchSavePackage.Add(sr.ReadLine());
										if(batchSavePackage.Count == 999) {
											dataHandler(batchSavePackage.ToArray());
											batchSavePackage.Clear();
										}
									}
									if(batchSavePackage.Count > 0) {
										dataHandler(batchSavePackage.ToArray());
									}
									//dataHandler("end");//标记结束
								}
							}
							FtpClient.FtpDelete(dealFileFullName);//删除已处理过的文件
							FtpClient.FtpRename(newFileFullName, dealFileFullName);// 已处理的文件 移动到此处,要么返回true,要么报错
						}
					}
				}
				catch {
					FtpClient.FtpRename(newFileFullName, Path + "/" + file.Filename);//有异常则改回去
					throw;
				}

			}
		}

		/// <summary>
		/// 获取指定FTP目录下的文本文件集合。
		/// </summary>
		/// <returns>文本文件集合,按照时间排序</returns>
		/// <exception cref="System.Net.WebException">FTP连接不上或者Path没有提前确认是否有效</exception>
		public List<FTPfileInfo> GetTxtFiles() {
			return FtpClient.ListDirectoryDetail(Path)
				   .FindAll(info => info.Extension.ToUpper() == "TXT").OrderBy(x => x.FileDateTime).ToList();
		}

		#endregion

		#region 追加写入文本

		/// <summary>
		/// 追加写入文本
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fileName"></param>
		/// <param name="dataHandler"></param>
		/// <param name="list"></param>
		public void WriteTxt<T>(string fileName, Func<T, string> dataHandler, List<T> list) where T : new() {
			using(var fs = File.OpenWrite(fileName)) {
				fs.Position = fs.Length;
				using(var sw = new StreamWriter(fs, Encoding.UTF8)) {
					foreach(var item in list) {
						string text = dataHandler(item);
						sw.WriteLine(text);
					}
				}
			}
		}
		#endregion
	}
	#endregion
}
