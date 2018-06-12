using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.DataAccess.Core.JD
{
    public class JDBillArchivedDAL : DataBase
    {
        public JDBillArchivedDAL()
            : base(DbName.SmartAPIAdapterCore, "JDBillArchived", "logNo", false)
        { }
    }
}
