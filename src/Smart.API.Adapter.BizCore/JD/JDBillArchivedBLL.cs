using Smart.API.Adapter.DataAccess.Core.JD;
using Smart.API.Adapter.Models.Core.JD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.BizCore.JD
{
    public class JDBillArchivedBLL
    {
        JDBillArchivedDAL dal = new JDBillArchivedDAL();
        /// <summary>
        /// 插入JD账单归档数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(JDBillModel model)
        {
            return dal.Insert<JDBillModel>(model);
        } 
    }
}
