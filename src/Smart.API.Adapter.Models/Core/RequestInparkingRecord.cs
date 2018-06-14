using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models.Core
{
    public class RequestPageBase
    {
        public int pageIndex
        {
            get;
            set;
        }

        public int pageSize
        {
            get;
            set;
        }   
    }

    public class ResponsePageBase
    {
        public int pageIndex
        {
            get;
            set;
        }

        public int pageSize
        {
            get;
            set;
        }
        public int totalCount
        {
            get;
            set;
        }

        public int pageCount
        {
            get;
            set;
        }
    }
    public class RequestInparkingRecord : RequestPageBase
    {
        public string plateNumber
        {
            get;
            set;
        }

        public string startTime
        {
            get;
            set;
        }

        public string endTime
        {
            get;
            set;
        }
    }

    public class ResponseInparkIngRecord : ResponsePageBase
    {
        List<InParkRecords> records;
    }


    public class InParkRecords
    {
        public string inRecordId
        {
            get;
            set;
        }

        public string parkId
        {
            get;
            set;
        }

        public string inDeviceId
        {
            get;
            set;
        }

        public string inDeviceName
        {
            get;
            set;
        }

        public string inTime
        {
            get;
            set;
        }

        public string plateNumber
        {
            get;
            set;
        }

        public string plateColor
        {
            get;
            set;
        }

        public string inImage
        {
            get;
            set;
        }

        public string stationOperator
        {
            get;
            set;
        }

        public string sealName
        {
            get;
            set;
        }
    }
    
}
