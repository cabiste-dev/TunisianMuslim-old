using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TunisiaPrayer.Models;
using Xamarin.Forms;

namespace TunisiaPrayer.ViewModels
{
    [QueryProperty(nameof(ChapterId), nameof(ChapterId))]
    public class ChapterViewModel : BaseViewModel
    {
        private int _chapterId;
        private VerseRootobject _verses;
        public VerseRootobject Verses
        {
            get { return _verses; }
            set
            {
                _verses = value;
            }
        }
        public int ChapterId
        {
            get { return _chapterId; }
            set
            {
                if (_chapterId != value)
                {
                    _chapterId = value;
                    LoadChapter(value);
                }
            }
        }

        private async void LoadChapter(int id)
        {
            IsBusy = true;
            string url = $"https://api.quran.com/api/v4/quran/verses/uthmani?chapter_number={id}";
            var client = new HttpClient();
            string response = await client.GetStringAsync(url);
            Verses = JsonConvert.DeserializeObject<VerseRootobject>(response);
            OnPropertyChanged(nameof(Verses));
            IsBusy = false;
        }
    }
}
