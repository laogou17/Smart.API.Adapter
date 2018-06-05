#if WebUI
namespace NEOCRM.Web.Models.DTO
#else
namespace NEOCRM.Models.DTO
#endif
{
    public class FromBodyDataModel : ApiRequestBase
    {
        public string data { set; get; }
    }

    public class FromBodyDataModel<T> : FromBodyDataModel
    {
        public new T data { set; get; }
    }

}
