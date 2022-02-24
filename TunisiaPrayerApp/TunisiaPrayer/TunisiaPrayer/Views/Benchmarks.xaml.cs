using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TunisiaPrayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Benchmarks : ContentPage
    {
        private int mins = 0, totalMilisecs = 0, milisecs = 0;
        public Benchmarks()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string PrayUrl = "https://www.meteo.tn/horaire_gouvernorat/" + DateTime.Now.ToString("yyyy-MM-dd") + $"/361/634";
            //string SteamUrl = "https://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid=440&count=3&maxlength=300&format=json";

            var timer = new Timer();
            timer.Interval = 1; // 1 milliseconds  
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            for (int i = 0; i < 10; i++)
            {
                StringReader_Bench(PrayUrl);
            }
            Average(1);
            Task.Delay(2000);
            for (int i = 0; i < 10; i++)
            {
                DirectDeserialization_Bench(PrayUrl);
            }
            Average(2);
            timer.Stop();
        }

        private void Average(int i)
        {
            if (i == 1)
            {
                Bench1.Text += $"\n Average: {TimeSpan.FromMilliseconds(totalMilisecs / 10)}";
                return;
            }
            Bench2.Text += $"\n Average: {TimeSpan.FromMilliseconds(totalMilisecs / 10)}";
        }

        private void ClearBenches(object sender, EventArgs e)
        {
            Bench1.Text = "benchmark 1 results";
            Bench2.Text = "benchmark 2 results";
        }
        private void PrintResults(int i)
        {
            if (i == 1)
            {
                Bench1.Text += $"\n {TimeSpan.FromMilliseconds(milisecs)}";
                milisecs = 0;
                return;
            }
            Bench2.Text += $"\n {TimeSpan.FromMilliseconds(milisecs)}";
            milisecs = 0;
        }

        void StringReader_Bench(string url)
        {

            //this line of code is unsecure but it's the only way to get data from this stupid site
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            Prayer Items = new Prayer();
            var client = new HttpClient(clientHandler);
            string response = client.GetStringAsync(url).Result;
            JsonSerializer jsonSerializer = new JsonSerializer();
            JsonReader jsonReader;
            using (TextReader ts = new StringReader(response))
            {
                jsonReader = new JsonTextReader(ts);
                Items = jsonSerializer.Deserialize<Prayer>(jsonReader);
            }
            var x = new List<string>() { Items.data.sobh, Items.data.dhohr, Items.data.aser, Items.data.magreb, Items.data.isha };
            PrintResults(1);
        }
        void DirectDeserialization_Bench(string url)
        {
            //this line of code is unsecure but it's the only way to get data from this stupid site
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = client.GetAsync(url).Result;
            Prayer result = new Prayer();
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<Prayer>(content);
            }
            var x = new List<string>() { result.data.sobh, result.data.dhohr, result.data.aser, result.data.magreb, result.data.isha };
            PrintResults(2);
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            milisecs++;
            totalMilisecs++;
        }
    }
}