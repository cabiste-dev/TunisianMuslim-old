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
        public static async Task<List<string>> GetTime(int stateId, int delegateId)
        {
            string url = "https://www.meteo.tn/horaire_gouvernorat/" + DateTime.Now.ToString("yyyy-MM-dd") + $"/{stateId}/{delegateId}";

            //this line of code is unsecure but it's the only way to get data from this stupid site
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

            return new List<string>() { Items.data.sobh, Items.data.dhohr, Items.data.aser, Items.data.magreb, Items.data.isha };
        }

        public static async Task<List<string>> GetTimeExperimental(int stateId, int delegateId)
        {
            string url = "https://www.meteo.tn/horaire_gouvernorat/" + DateTime.Now.ToString("yyyy-MM-dd") + $"/{stateId}/{delegateId}";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            //this line of code is unsecure but it's the only way to get data from this stupid site
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpResponseMessage response = await client.GetAsync(url);
            Prayer result = new Prayer();
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Prayer>(content);

            }
            return new List<string>() { result.data.sobh, result.data.dhohr, result.data.aser, result.data.magreb, result.data.isha };
        }
    }
}
