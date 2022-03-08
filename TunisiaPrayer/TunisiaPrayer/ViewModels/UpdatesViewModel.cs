using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace TunisiaPrayer.ViewModels
{
    public class UpdatesViewModel : BaseViewModel
    {

        public ICommand UpdateCommand { get; set; }

        public UpdatesViewModel()
        {
            //UpdateCommand = new AsyncCommand(Download);
        }


        //async Task Download()
        //{
        //    IsDownloading = "downloading";
        //    OnPropertyChanged(nameof(IsDownloading));
        //    Uri url = new Uri("https://github.com/cabiste69/TunisiaPrayer/releases/download/1.0.0/TunisiaPrayer-1.0.apk");
        //    WebClient myWebClient = new WebClient();
        //    string savePath = FileSystem.CacheDirectory;
        //    myWebClient.DownloadFileAsync(url, $"{savePath}.test.apk");

        //}
    }
}
