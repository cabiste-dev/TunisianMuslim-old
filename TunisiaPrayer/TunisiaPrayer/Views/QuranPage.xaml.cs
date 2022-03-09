using MvvmHelpers;
using Newtonsoft.Json;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TunisiaPrayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuranPage : ContentPage
    {
        public ObservableRangeCollection<Chapter> chaps { get; set; } = new ObservableRangeCollection<Chapter>();
        public QuranPage()
        {
            InitializeComponent();
            BindingContext = this;
            LoadQuran();
        }

        public ObservableRangeCollection<Chapter> Chapters
        {
            get { return chaps; }
            set
            {
                if (value != chaps)
                {
                    chaps = value;
                }
            }
        }
        public async void LoadQuran()
        {
            ChaptersRootobject chapters = new ChaptersRootobject();
            string url = "https://api.quran.com/api/v4/chapters?language=en";
            var client = new HttpClient();
            string response = await client.GetStringAsync(url);
            chapters = JsonConvert.DeserializeObject<ChaptersRootobject>(response);
            for (byte i = 0; i < 114; i++)
            {
                Chapters.Add(new Chapter() { bismillah_pre = chapters.chapters[i].bismillah_pre, id = chapters.chapters[i].id, name_arabic = chapters.chapters[i].name_arabic, name_simple = chapters.chapters[i].name_simple, translated_name = chapters.chapters[i].translated_name, verses_count = chapters.chapters[i].verses_count });
            }
        }
    }
}