using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Common {

	public static class IDictionaryExtentions {
		public static void Set(this IDictionary<string, string> dictionary, string key, string value) {
			if(dictionary == null) {
				dictionary = new Dictionary<string, string>();
			}
			if(dictionary.ContainsKey(key)) {
				dictionary[key] = value;
			}
			else {
				dictionary.Add(key, value);
			}
		}
		public static IDictionary<string, string> FluentAdd(this IDictionary<string, string> dictionary, string key, string value) {
			dictionary.Add(key, value);
			return dictionary;
		}

		public static Dictionary<string, string> ToDictionary(this object obj) {
			Dictionary<string, string> dic = new Dictionary<string, string>();
			if(obj == null) return dic;
			PropertyInfo[] pis = obj.GetType().GetProperties();
			for(int i = 0; i < pis.Length; i++) {
				object objValue = pis[i].GetValue(obj, null);
				objValue = (objValue == null) ? DBNull.Value : objValue;
				if(!dic.ContainsKey(pis[i].Name)) {
					dic.Add(pis[i].Name, objValue.ToString());
				}
			}
			return dic;
		}
	}

}
