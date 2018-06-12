using Smart.API.Adapter.DataAccess.Core.JD;
using Smart.API.Adapter.Models.Core.JD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.BizCore.JD
{
    public class JDBillBLL
    {
        JDBillDAL dal = new JDBillDAL();

        /// <summary>
        /// 插入JD账单数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(JDBillModel model)
        {
            model.QrCode = string.IsNullOrWhiteSpace(model.QrCode) ? "" : model.QrCode;
            model.Cost = string.IsNullOrWhiteSpace(model.Cost) ? "" : model.Cost;
            model.CreatTime = DateTime.Now;
            return dal.Insert<JDBillModel>(model);
        }

        /// <summary>
        /// 根据LogNo获取JD账单
        /// </summary>
        /// <param name="sLogNo"></param>
        /// <returns></returns>
        public JDBillModel GetJDBillByLogNo(string sLogNo)
        {
            return dal.GetJDBillByLogNo(sLogNo);
        }

        public bool Update(JDBillModel model)
        {
            return dal.Update<JDBillModel>(model, model.LogNo);
        }

        public bool Delete(JDBillModel model)
        {
            return dal.DeleteByKey(model.LogNo);
        }
    }
}
