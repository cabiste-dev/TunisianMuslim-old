[assembly: Xamarin.Forms.Dependency(typeof(TunisiaPrayer.Droid.InstallApkSessionApi))]
namespace TunisiaPrayer.Droid
{
    using System;
    using System.IO;

    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Android.Widget;
    using TunisiaPrayer.Services;
    using Xamarin.Essentials;
    using Xamarin.Forms;


    [Activity(Label = "InstallApkSessionApi", LaunchMode = LaunchMode.SingleTop)]
    public class InstallApkSessionApi : Activity, IPackageInstaller
    {
        private static readonly string PACKAGE_INSTALLED_ACTION = $"{AppInfo.PackageName}.apis.content.SESSION_API_PACKAGE_INSTALLED";

        public void OnCreate(string apkPath)
        {
            Intent unKnownSourceIntent = new Intent(Android.Provider.Settings.ActionManageUnknownAppSources).SetData((Android.Net.Uri)string.Format("package:%s", AppInfo.PackageName));

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                //original condition -> !Activity.GetPackageManager().canRequestPackageInstalls()
                if (!Android.App.Application.Context.PackageManager.CanRequestPackageInstalls())
                {
                    //original parameters -> unKnownSourceIntent, Constant.UNKNOWN_RESOURCE_INTENT_REQUEST_CODE
                    StartActivityForResult(unKnownSourceIntent, 15);
                }
            }
            var bundle = PackageManager.GetApplicationInfo(PackageName, Android.Content.PM.PackageInfoFlags.Providers).MetaData;
            Android.Net.Uri uri = Android.Net.Uri.FromFile(new Java.IO.File(apkPath));
            Intent promptInstall = new Intent(Intent.ActionView).SetDataAndType(uri, "application/vnd.android.package-archive");
            promptInstall.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(promptInstall);


            //Install(apkPath);
        }

        private void Install(string apkPath)
        {
            PackageInstaller.Session session = null;
            try
            {
                PackageInstaller packageInstaller = Android.App.Application.Context.PackageManager.PackageInstaller;
                PackageInstaller.SessionParams @params = new PackageInstaller.SessionParams(PackageInstallMode.FullInstall);
                int sessionId = packageInstaller.CreateSession(@params);
                session = packageInstaller.OpenSession(sessionId);
                long apkSize = new FileInfo(apkPath).Length;
                AddApkToInstallSession(apkPath, session, apkSize);
                // Create an install status receiver.
                Context context = Android.App.Application.Context;
                //the error is no longer here
                Intent intent = new Intent(context, typeof(InstallApkSessionApi));
                intent.SetAction(PACKAGE_INSTALLED_ACTION);
                PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, intent, 0);
                IntentSender statusReceiver = pendingIntent.IntentSender;

                // Commit the session (this will start the installation workflow).
                session.Commit(statusReceiver);
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException("Couldn't install package", ex);
            }
            catch
            {
                if (session != null)
                {
                    session.Abandon();
                }

                throw;
            }
        }


        private void AddApkToInstallSession(string assetName, PackageInstaller.Session session, long apkSize)
        {
            // It's recommended to pass the file size to openWrite(). Otherwise installation may fail
            // if the disk is almost full.
            using Stream packageInSession = session.OpenWrite("package", 0, apkSize);
            using Stream @is = File.Open(assetName, FileMode.Open);
            byte[] buffer = new byte[16384];
            int n;
            while ((n = @is.Read(buffer)) > 0)
            {
                packageInSession.Write(buffer, 0, n);
            }
        }

        // Note: this Activity must run in singleTop launchMode for it to be able to receive the intent
        // in onNewIntent().
        protected override void OnNewIntent(Intent intent)
        {
            Bundle extras = intent.Extras;
            if (PACKAGE_INSTALLED_ACTION.Equals(intent.Action))
            {
                PackageInstallStatus status = (PackageInstallStatus)extras.GetInt(PackageInstaller.ExtraStatus);
                string message = extras.GetString(PackageInstaller.ExtraStatusMessage);
                switch (status)
                {
                    case PackageInstallStatus.PendingUserAction:
                        // This test app isn't privileged, so the user has to confirm the install.
                        Intent confirmIntent = (Intent)extras.Get(Intent.ExtraIntent);
                        this.StartActivity(confirmIntent);
                        break;
                    case PackageInstallStatus.Success:
                        Toast.MakeText(this, "Install succeeded!", ToastLength.Short).Show();
                        break;
                    case PackageInstallStatus.Failure:
                    case PackageInstallStatus.FailureAborted:
                    case PackageInstallStatus.FailureBlocked:
                    case PackageInstallStatus.FailureConflict:
                    case PackageInstallStatus.FailureIncompatible:
                    case PackageInstallStatus.FailureInvalid:
                    case PackageInstallStatus.FailureStorage:
                        Toast.MakeText(this, "Install failed! " + status + ", " + message,
                                ToastLength.Short).Show();
                        break;
                    default:
                        Toast.MakeText(this, "Unrecognized status received from installer: " + status,
                                ToastLength.Short).Show();
                        break;
                }
            }
        }
    }
}