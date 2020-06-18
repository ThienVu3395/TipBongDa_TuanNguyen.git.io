using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Common
{
    public class API
    {
        public static List<T> GetListAPI<T>(string url)
        {
            List<T> result = new List<T>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(url).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var data = response.Content.ReadAsStringAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    JArray jsonResponse = JArray.Parse(data);

                    foreach (var item in jsonResponse)
                    {
                        T rowsResult = item.ToObject<T>();
                        result.Add(rowsResult);
                    }
                    //T json = (T)JsonConvert.DeserializeObject(data);
                    return result;
                }
                else
                {
                    throw new Exception("Error while call api");
                }
            }
        }
    }
}
