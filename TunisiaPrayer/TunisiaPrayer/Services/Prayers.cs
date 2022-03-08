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
        public static async Task<List<string>> GetTime(short stateId, short delegateId)
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
    }
}
