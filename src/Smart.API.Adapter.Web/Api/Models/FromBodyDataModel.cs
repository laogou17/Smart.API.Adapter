namespace Smart.API.Adapter.Web.Api {
	public class FromBodyDataModel : ApiRequestBase {
		public string data { set; get; }
	}

	public class FromBodyDataModel<T> : FromBodyDataModel {
		public new T data { set; get; }
	}
}
