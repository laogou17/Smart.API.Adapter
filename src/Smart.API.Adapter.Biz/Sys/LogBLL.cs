using NEOCRM.DataAccess.Sys;
using NEOCRM.Models;
using System;
using System.Collections.Generic;

namespace NEOCRM.Biz.Sys {
	public class LogBLL {

		//private LogDAL dal = null;

		//public LogBLL()
		//{
		//	this.dal = new LogDAL();
		//}
		 
		///// <summary>
		///// 接口日志分页
		///// </summary>
		///// <param name="pageIndex">当前页</param>
		///// <param name="pageSize">页大小</param>
		///// <param name="interfaceName">接口名称</param>
		///// <param name="sort">排序字段</param>
		///// <param name="createdBeginTime">创建开始时间</param>
		///// <param name="createdEndTime">创建结束时间</param>
		///// <param name="totalCount">总页数</param>
		///// <returns></returns>
		//public ICollection<InterfaceLog> GetInterfaceLogPageList(int pageIndex, int pageSize, string interfaceName
		//	, string sort, DateTime? createdBeginTime, DateTime? createdEndTime, out int totalCount)
		//{

		//	return dal.GetInterfaceLogPageList(pageIndex, pageSize, interfaceName, sort, createdBeginTime, createdEndTime, out totalCount);

		//}

		///// <summary>
		///// 服务运行日志分页
		///// </summary>
		///// <param name="pageIndex">当前页</param>
		///// <param name="pageSize">页大小</param>
		///// <param name="applicationName">服务名称</param>
		///// <param name="sort">排序字段</param>
		///// <param name="createdBeginTime">创建开始时间</param>
		///// <param name="createdEndTime">创建结束时间</param>
		///// <param name="totalCount">总页数</param>
		///// <returns></returns>
		//public ICollection<ServiceLog> GetServiceLogPageList(int pageIndex, int pageSize, string applicationName
		//	, string sort, DateTime? createdBeginTime, DateTime? createdEndTime, out int totalCount)
		//{

		//	return dal.GetServiceLogPageList(pageIndex, pageSize, applicationName, sort, createdBeginTime, createdEndTime, out totalCount);

		//}

		///// <summary>
		///// 异常日志分页
		///// </summary>
		///// <param name="pageIndex">当前页</param>
		///// <param name="pageSize">页大小</param>
		///// <param name="applicationName">服务名称</param>
		///// <param name="sort">排序字段</param>
		///// <param name="createdBeginTime">创建开始时间</param>
		///// <param name="createdEndTime">创建结束时间</param>
		///// <param name="totalCount">总页数</param>
		///// <returns></returns>
		//public ICollection<ExceptionLog> GetExceptionLogPageList(int pageIndex, int pageSize, string applicationName
		//	, string sort, DateTime? createdBeginTime, DateTime? createdEndTime, out int totalCount)
		//{

		//	return dal.GetExceptionLogPageList(pageIndex, pageSize, applicationName, sort, createdBeginTime, createdEndTime, out totalCount);

		//}
	}
}
