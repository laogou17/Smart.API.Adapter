using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Smart.API.Adapter.Models.DTO.JD
{
    [XmlRoot("JDParkConfig")]
    public class JDParkConfig
    {
        [XmlArray("JDTimerConifg")]
        [XmlArrayItem("JDTimer")]
        public List<JDTimer> LJDTime;
    }


    [XmlRootAttribute("JDTimer")]
    public class JDTimer
    {
        [XmlAttribute("key")]
        public string key
        {
            get;
            set;
        }

        [XmlAttribute("name")]
        public string name
        {
            get;
            set;
        }

        [XmlAttribute("ReConnectCount")]
        public int ReConnectCount
        {
            get;
            set;
        }

        [XmlAttribute("FailTimeSpan")]
        public int FailTimeSpan
        {
            get;
            set;
        }

        [XmlAttribute("ExceptionTimeSpan")]
        public int ExceptionTimeSpan
        {
            get;
            set;
        }

        [XmlAttribute("UnavailableTimeSpan")]
        public int UnavailableTimeSpan
        {
            get;
            set;
        }

        [XmlAttribute("RePostCount")]
        public int RePostCount
        {
            get;
            set;
        }

        [XmlAttribute("SendEmail")]
        public bool SendEmail
        {
            get;
            set;
        }
    }


    public class JDPostInfo
    {
        /// <summary>
        /// 已经重试的次数
        /// </summary>
        public int ReCount
        {
            get;
            set;
        }

        /// <summary>
        /// 重试时间
        /// </summary>
        public DateTime ReTime
        {
            get;
            set;
        }

        /// <summary>
        /// 是否再次重试，用于间隔1小时再重试的
        /// </summary>
        public bool IsReTry
        {
            get;
            set;
        }

        /// <summary>
        /// 重试的业务类型
        /// </summary>
        public string ReType
        {
            get;
            set;
        }
    }
}
