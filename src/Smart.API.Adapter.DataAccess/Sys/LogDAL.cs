using NEOCRM.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NEOCRM.DataAccess.Sys
{
    /// <summary>
    /// 日志管理
    /// </summary>
    public class LogDAL : DataBase
    {
        /// <summary>
        /// 初始化连接字符串
        /// </summary>
        public LogDAL() : base(DbName.CRMLog) { } 

        /// <summary>
        /// 接口日志分页
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="interfaceName">接口名称</param>
        /// <param name="sort">排序字段</param>
        /// <param name="createdBeginTime">创建开始时间</param>
        /// <param name="createdEndTime">创建结束时间</param>
        /// <param name="totalCount">总页数</param>
        /// <returns></returns>
        public ICollection<InterfaceLog> GetInterfaceLogPageList(int pageIndex, int pageSize, string interfaceName
            , string sort, DateTime? createdBeginTime, DateTime? createdEndTime, out int totalCount)
        {
            try
            {

                int numIndex1, numIndex2;
                //开始
                numIndex1 = pageIndex * pageSize - pageSize + 1;
                //结束
                numIndex2 = pageIndex * pageSize;

				string sql = @"select row_number() over(order by " + (string.IsNullOrEmpty(sort) ? "logid desc" : sort) + " ) as rownum,* from InterfaceLog with(nolock) where 1=1 ";

                //条件
                List<SqlParameter> parameterListOne = new List<SqlParameter>();
                List<SqlParameter> parameterListTwo = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(interfaceName))
                {
                    sql += " and interfaceName=@interfaceName";
                    parameterListOne.Add(new SqlParameter("@interfaceName", interfaceName));
                    parameterListTwo.Add(new SqlParameter("@interfaceName", interfaceName));
                }
                if (createdBeginTime != null && createdEndTime != null)
                {
                    sql += " and CreatedTime between '" + createdBeginTime.ToString() + "' and '" + createdEndTime.Value.AddDays(1).ToString() + "'";
                }

                //计算总页数
                DataTable dt = GetDataTableBySqlString("select count(*) from (" + sql + ") t", parameterListOne);
                if (dt.Rows.Count > 0)
                    totalCount = Convert.ToInt32(dt.Rows[0][0]);
                else
                    totalCount = 0;

                ICollection<InterfaceLog> list = GetEnityListBySqlString<InterfaceLog>("select * from (" + sql + ") as t where  rownum between "
                    + numIndex1 + " and " + numIndex2, parameterListTwo);

                return list;

            }
            catch 
            {
                throw ;
            }

        }

        /// <summary>
        /// 服务运行日志分页
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="applicationName">服务名称</param>
        /// <param name="sort">排序字段</param>
        /// <param name="createdBeginTime">创建开始时间</param>
        /// <param name="createdEndTime">创建结束时间</param>
        /// <param name="totalCount">总页数</param>
        /// <returns></returns>
        public ICollection<ServiceLog> GetServiceLogPageList(int pageIndex, int pageSize, string applicationName
            , string sort, DateTime? createdBeginTime, DateTime? createdEndTime, out int totalCount)
        {
            try
            {

                int numIndex1, numIndex2;
                //开始
                numIndex1 = pageIndex * pageSize - pageSize + 1;
                //结束
                numIndex2 = pageIndex * pageSize;

				string sql = @"select row_number() over(order by " + (string.IsNullOrEmpty(sort) ? "logid desc" : sort) + " ) as rownum,* from ServiceLog with(nolock) where 1=1 ";

                //条件
                List<SqlParameter> parameterListOne = new List<SqlParameter>();
                List<SqlParameter> parameterListTwo = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(applicationName))
                {
                    sql += " and applicationName=@applicationName";
                    parameterListOne.Add(new SqlParameter("@applicationName", applicationName));
                    parameterListTwo.Add(new SqlParameter("@applicationName", applicationName));
                }
                if (createdBeginTime != null && createdEndTime != null)
                {
                    sql += " and CreatedTime between '" + createdBeginTime.ToString() + "' and '" + createdEndTime.Value.AddDays(1).ToString() + "'";
                }

                //计算总页数
                DataTable dt = GetDataTableBySqlString("select count(*) from (" + sql + ") t", parameterListOne);
                if (dt.Rows.Count > 0)
                    totalCount = Convert.ToInt32(dt.Rows[0][0]);
                else
                    totalCount = 0;

                ICollection<ServiceLog> list = GetEnityListBySqlString<ServiceLog>("select * from (" + sql + ") as t where  rownum between "
                    + numIndex1 + " and " + numIndex2, parameterListTwo);

                return list;

            }
            catch 
            {
                throw ;
            }

        }

        /// <summary>
        /// 异常日志分页
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="applicationName">服务名称</param>
        /// <param name="sort">排序字段</param>
        /// <param name="createdBeginTime">创建开始时间</param>
        /// <param name="createdEndTime">创建结束时间</param>
        /// <param name="totalCount">总页数</param>
        /// <returns></returns>
        public ICollection<ExceptionLog> GetExceptionLogPageList(int pageIndex, int pageSize, string applicationName
            , string sort, DateTime? createdBeginTime, DateTime? createdEndTime, out int totalCount)
        {
            try
            {

                int numIndex1, numIndex2;
                //开始
                numIndex1 = pageIndex * pageSize - pageSize + 1;
                //结束
                numIndex2 = pageIndex * pageSize;

                string sql = @"select row_number() over(order by " + (string.IsNullOrEmpty(sort) ? "logid desc" : sort) + " ) as rownum,* from ExceptionLog with(nolock) where 1=1 ";

                //条件
                List<SqlParameter> parameterListOne = new List<SqlParameter>();
                List<SqlParameter> parameterListTwo = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(applicationName))
                {
                    sql += " and applicationName=@applicationName";
                    parameterListOne.Add(new SqlParameter("@applicationName", applicationName));
                    parameterListTwo.Add(new SqlParameter("@applicationName", applicationName));
                }
                if (createdBeginTime != null && createdEndTime != null)
                {
                    sql += " and CreatedTime between '" + createdBeginTime.ToString() + "' and '" + createdEndTime.Value.AddDays(1).ToString() + "'";
                }

                //计算总页数
                DataTable dt = GetDataTableBySqlString("select count(*) from (" + sql + ") t", parameterListOne);
                if (dt.Rows.Count > 0)
                    totalCount = Convert.ToInt32(dt.Rows[0][0]);
                else
                    totalCount = 0;

                ICollection<ExceptionLog> list = GetEnityListBySqlString<ExceptionLog>("select * from (" + sql + ") as t where  rownum between "
                    + numIndex1 + " and " + numIndex2, parameterListTwo);

                return list;

            }
            catch 
            {
                throw ;
            }

        }

    }
}
