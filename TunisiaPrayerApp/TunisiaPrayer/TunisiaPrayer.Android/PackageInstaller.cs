namespace TunisiaPrayer.Droid
{
    using System;
    using System.IO;

    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Android.Widget;

    [Activity(Label = "InstallApkSessionApi", LaunchMode = LaunchMode.SingleTop)]
    public class InstallApkSessionApi : Activity
    {
        private static readonly string PACKAGE_INSTALLED_ACTION =
                "com.example.android.apis.content.SESSION_API_PACKAGE_INSTALLED";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.install_apk_session_api);

            // Watch for button clicks.
            Button button = this.FindViewById<Button>(Resource.Id.install);
            button.Click += this.Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            PackageInstaller.Session session = null;
            try
            {
                PackageInstaller packageInstaller = this.PackageManager.PackageInstaller;
                PackageInstaller.SessionParams @params = new PackageInstaller.SessionParams(
                        PackageInstallMode.FullInstall);
                int sessionId = packageInstaller.CreateSession(@params);
                session = packageInstaller.OpenSession(sessionId);
                this.AddApkToInstallSession("HelloActivity.apk", session);

                // Create an install status receiver.
                Context context = this;
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


        private void AddApkToInstallSession(string assetName, PackageInstaller.Session session)
        {
            // It's recommended to pass the file size to openWrite(). Otherwise installation may fail
            // if the disk is almost full.
            using Stream packageInSession = session.OpenWrite("package", 0, -1);
            using Stream @is = this.Assets.Open(assetName);
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