using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using TunisiaPrayer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TunisiaPrayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuranPage : ContentPage
    {
        public ObservableRangeCollection<Chapter> chaps { get; set; } = new ObservableRangeCollection<Chapter>();
        public ChaptersRootobject test { get; set; }
        public ICommand RefreshCom { get; set; }
        public QuranPage()
        {
            InitializeComponent();
            BindingContext = this;
            RefreshCom = new AsyncCommand(LoadQuran);
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
        public async Task LoadQuran()
        {

            IsBusy = true;
            test = new ChaptersRootobject();
            string url = "https://api.quran.com/api/v4/chapters?language=en";
            var client = new HttpClient();
            string response = await client.GetStringAsync(url);
            test = JsonConvert.DeserializeObject<ChaptersRootobject>(response);
            OnPropertyChanged(nameof(test));
            IsBusy = false;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ChapterPage)}?{nameof(ChapterViewModel.ChapterId)}={e.SelectedItemIndex + 1}");
        }

        //opens the tapped chapter in a new view to read it
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //deselcts the tapped item 
            ((ListView)sender).SelectedItem = null;

        }

        //to download the selcted chapters
        private void MenuItem_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}