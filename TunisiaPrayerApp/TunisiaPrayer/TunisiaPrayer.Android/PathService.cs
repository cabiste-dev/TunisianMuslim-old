using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
                return Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).ToString();
            }
        }

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