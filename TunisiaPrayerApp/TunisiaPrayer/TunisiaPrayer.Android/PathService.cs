using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TunisiaPrayer.Services;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(TunisiaPrayer.Droid.PathService))]
namespace TunisiaPrayer.Droid
{
    public class PathService : IPathService
    {
        private Context context = Android.App.Application.Context;
        public string PublicExternalFolder
        {
            get
            {
                //return context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).ToPath().ToString();
                return Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads).ToString();
            }
        }

        //public async void install(string filePath)
        //{

        //    Java.IO.File file = new Java.IO.File(filePath);
        //    if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
        //    {
        //        Uri apkUri = FileProvider.GetUriForFile(context, context.ApplicationContext.PackageName + ".provider", file);
        //        Intent intentS = new Intent(Intent.ActionInstallPackage);
        //        intentS.SetData(apkUri);
        //        intentS.SetFlags(ActivityFlags.GrantReadUriPermission);
        //        context.StartActivity(intentS);
        //    }
        //    else
        //    {
        //        Uri apkUri = Uri.FromFile(file);
        //        Intent intentS = new Intent(Intent.ActionView);
        //        intentS.SetDataAndType(apkUri, "application/vnd.android.package-archive");
        //        intentS.SetFlags(ActivityFlags.NewTask);
        //        context.StartActivity(intentS);
        //    }
        //}


        //public static Android.Net.Uri GetPathUri(Context context, string filePath)
        //{
        //    Android.Net.Uri uri;
        //    if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
        //    {
        //        string packageName = context.PackageName;
        //        uri = FileProvider.GetUriForFile(context, packageName + ".fileProvider", new File(filePath));
        //    }
        //    else
        //    {
        //        uri = Android.Net.Uri.FromFile(new File(filePath));
        //    }
        //    return uri;
        //}

        //public string PrivateExternalFolder
        //{
        //    get
        //    {
        //        return Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath;
        //    }
        //}
        //public string InternalFolder
        //{
        //    get
        //    {
        //        return Android.App.Application.Context.FilesDir.AbsolutePath;
        //    }
        //}

    }
}