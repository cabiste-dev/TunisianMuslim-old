[assembly: Xamarin.Forms.Dependency(typeof(TunisiaPrayer.Droid.InstallApkSessionApi))]
namespace TunisiaPrayer.Droid
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using TunisiaPrayer.Services;
    using Xamarin.Essentials;


    [Activity(Label = "InstallApkSessionApi", LaunchMode = LaunchMode.SingleTop)]
    public class InstallApkSessionApi : Activity, IPackageInstaller
    {
        private Android.Net.Uri _uri;
        public void InstallApk(string apkPath)
        {
            //if (!PermissionGranted()) { return; }
            SetUri(apkPath);
            StartInstallation();
        }

        private void StartInstallation()
        {
            Intent promptInstall = new Intent(Intent.ActionView, _uri).SetDataAndType(_uri, "application/vnd.android.package-archive");
            promptInstall.AddFlags(ActivityFlags.NewTask);
            promptInstall.AddFlags(ActivityFlags.GrantReadUriPermission);
            Application.Context.StartActivity(promptInstall);
        }

        private void SetUri(string apkPath)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                //this is the format for android API 26 or above
                _uri = FileProvider.GetUriForFile(Application.Context, AppInfo.PackageName + ".provider", new Java.IO.File(apkPath));
            }
            else
            {
                _uri = Android.Net.Uri.FromFile(new Java.IO.File(apkPath));
            }
        }

        private bool PermissionGranted()
        {
            bool result = true;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                if (!Android.App.Application.Context.PackageManager.CanRequestPackageInstalls())
                {
                    //ask the user if they want to grant the permission for the app or no
                    Intent requestPackageInstallPerrmission = new Intent(Android.Provider.Settings.ActionManageUnknownAppSources, Android.Net.Uri.Parse("package:" + Android.App.Application.Context.PackageName)).SetFlags(ActivityFlags.NewTask);
                    Application.Context.StartActivity(requestPackageInstallPerrmission);
                }
            }
            return result;
        }
    }
}