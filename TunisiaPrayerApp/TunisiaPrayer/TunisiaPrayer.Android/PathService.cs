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
        private Context context { get; set; }
        public string InternalFolder
        {
            get
            {
                return Android.App.Application.Context.FilesDir.AbsolutePath;
            }
        }

        public string PublicExternalFolder
        {
            get
            {
                return Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
            }
        }

        public string PrivateExternalFolder
        {
            get
            {
                return Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath;
            }
        }
    }
}