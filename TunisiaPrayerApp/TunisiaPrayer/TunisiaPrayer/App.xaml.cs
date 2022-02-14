using System.Collections.Generic;
using TunisiaPrayer.Models;
using TunisiaPrayer.Services;
using Xamarin.Forms;
using Xamarin.Essentials;
using System;
using System.Threading.Tasks;

namespace TunisiaPrayer
{
    public partial class App : Application
    {
        public static List<Rootobject> statesData { get; set; }
        public static byte selectedStateIndex { get; set; }
        public static byte selectedDelegateIndex { get; set; }
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            StateService something = new StateService();
            statesData = await something.LoadData();
            await LoadPrefrences();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        async Task LoadPrefrences()
        {
            selectedStateIndex = Convert.ToByte(Preferences.Get("selectedStateIndex", 0));
            selectedDelegateIndex = Convert.ToByte(Preferences.Get("selectedDelegate", 0));
        }
    }
}
