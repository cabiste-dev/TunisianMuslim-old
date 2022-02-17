using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.PlatformConfiguration;

namespace TunisiaPrayer.Services
{
    public interface IPackageInstaller
    {
        void OnCreate(string apkPath);
    }
}
