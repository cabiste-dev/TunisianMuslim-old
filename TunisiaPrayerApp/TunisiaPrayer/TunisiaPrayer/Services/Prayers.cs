using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TunisiaPrayer.Services
{
    public class Prayers
    {
        public static async Task<Prayer> GetTime(int stateId, int delegateId)
        {
            string url = "https://www.meteo.tn/horaire_gouvernorat/" + DateTime.Now.ToString("yyyy-MM-dd") + $"/{stateId}/{delegateId}";
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            Prayer Items = new Prayer();
            var client = new HttpClient(clientHandler);
            string response = await client.GetStringAsync(url);
            JsonSerializer jsonSerializer = new JsonSerializer();
            JsonReader jsonReader;
            using (TextReader ts = new StringReader(response))
            {
                jsonReader = new JsonTextReader(ts);
                Items = jsonSerializer.Deserialize<Prayer>(jsonReader);
            }

            return Items;
        }

        public static async Task<Prayer> GetTimeExperimental()
        {
            string url = "https://www.meteo.tn/horaire_gouvernorat/" + DateTime.Now.ToString("yyyy-MM-dd") + "/361/634";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.GetAsync("");
            Prayer result = new Prayer();
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Prayer>(content);

            }
            return await Task.FromResult(result);
        }
    }
}
