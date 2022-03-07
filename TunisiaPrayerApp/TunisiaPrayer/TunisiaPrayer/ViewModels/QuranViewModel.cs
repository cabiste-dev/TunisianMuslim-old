using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace TunisiaPrayer.ViewModels
{
    public class QuranViewModel : BaseViewModel
    {
        public ChaptersRootobject chapters { get; set; }
        public QuranViewModel()
        {
            LoadQuran();
        }

        async void LoadQuran()
        {
            string url = "https://api.quran.com/api/v4/chapters?language=en";
            var client = new HttpClient();
            string response = await client.GetStringAsync(url);
            chapters = JsonConvert.DeserializeObject<ChaptersRootobject>(response);

        }
    }
}
