using System;
using System.Threading.Tasks;
using Octokit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Net;
using TunisiaPrayer.Services;
using System.ComponentModel;
using Xamarin.CommunityToolkit.Extensions;

namespace TunisiaPrayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatesPage : ContentPage
    {
        public bool UpdateAvailable { get; set; } = false;
        public string LatestReleaseInfo { get; set; }
        public string UpdateButton { get; set; } = "Update";
        IDeviceInfoService deviceService;
        IPackageInstaller packageInstaller;
        private string _fileUri;
        private string _newVersionTag;
        public UpdatesPage()
        {
            InitializeComponent();
            BindingContext = this;
            deviceService = DependencyService.Get<IDeviceInfoService>();
            packageInstaller = DependencyService.Get<IPackageInstaller>();
        }


        protected override async void OnAppearing()
        {
            await CheckForUpdates();
        }

        private async Task CheckForUpdates()
        {

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await this.DisplayToastAsync("no internet connection :(", 5000);
                return;
            }

            var client = new GitHubClient(new ProductHeaderValue("tunisiaPrayerApp"));
            //yes that's the repo's id, it's public
            var releases = await client.Repository.Release.GetAll(451233074);
            rel.Text = releases[0].Name;

            if (releases[0].TagName != VersionTracking.CurrentVersion)
            {
                UpdateAvailable = true;
                _newVersionTag = releases[0].TagName;
                OnPropertyChanged(nameof(UpdateAvailable));
                ReleaseChanges(releases[0].Body);
            }
            else
            {
                rel.Text = "No updates Available";
            }
        }

        void ReleaseChanges(string release)
        {
            LatestReleaseInfo = release;
            OnPropertyChanged(nameof(LatestReleaseInfo));
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await this.DisplayToastAsync("no internet connection :(", 5000);
                return;
            }

            if (await StoragePermissionDenied())
            {
                return;
            }
            string appName = $"TunisiaPrayer-{deviceService.Architecture}-v{_newVersionTag}.apk";
            Uri url = new Uri($"https://github.com/cabiste69/TunisiaPrayer/releases/download/{_newVersionTag}/{appName}");

            GetFileUri(appName);
            UpdateButton = "Downloading...";
            UpdateAvailable = false;
            OnPropertyChanged(nameof(UpdateAvailable));
            OnPropertyChanged(nameof(UpdateButton));
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFileAsync(url, _fileUri);
            myWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(InstallUpdate);
        }

        private void InstallUpdate(object sender, AsyncCompletedEventArgs e)
        {
            UpdateButton = "Installing...";
            OnPropertyChanged(nameof(UpdateButton));
            packageInstaller.InstallApk(_fileUri);
        }

        //quite confusing but it's correct
        private async Task<bool> StoragePermissionDenied()
        {
            PermissionStatus permissionWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            PermissionStatus permissionRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (permissionWrite != PermissionStatus.Granted)
            {
                permissionWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }
            if (permissionRead != PermissionStatus.Granted)
            {
                permissionRead = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            return permissionWrite != PermissionStatus.Granted && permissionRead != PermissionStatus.Granted;

        }
        private void GetFileUri(string appName)
        {
            _fileUri = $"{deviceService.PublicExternalFolder}/{appName}";
        }

    }
}