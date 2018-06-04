using Smart.API.Adapter.Common;
using Smart.API.Adapter.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Biz
{
    public class ParkBiz
    {
        public async Task<VehicleLegality> QueryVehicleLegality()
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60411/api");

                var values = new List<KeyValuePair<string, string>>();
                values.Add(new KeyValuePair<string, string>("parkLotCode", CommonSettings.ParkLotCode));
                values.Add(new KeyValuePair<string, string>("version", CommonSettings.Version));
                values.Add(new KeyValuePair<string, string>("token", CommonSettings.Token));
                var content = new FormUrlEncodedContent(values);
                //var response = await client.PostAsync("/queryVehicleLegality", content);
                //VehicleLegality result = response.Content.ToJson().FromJson<VehicleLegality>();
                var response = await client.PostAsync("/queryVehicleLegality", content);

                VehicleLegality result = new VehicleLegality();
                
                return result;
            }           
        }




    }
}
