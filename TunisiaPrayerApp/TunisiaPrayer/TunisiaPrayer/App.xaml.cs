using System.Collections.Generic;
using TunisiaPrayer.Models;
using TunisiaPrayer.Services;
using Xamarin.Forms;
using Xamarin.Essentials;
using System;

namespace TunisiaPrayer
{
    public partial class App : Application
    {
        public static List<Rootobject> statesData { get; set; }
        public static byte selectedStateIndex { get; set; }
        public static byte selectedDelegate { get; set; }
        public App()
        {
            InitializeComponent();
            
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            StateService something = new StateService();
            statesData = await something.LoadData();
            LoadPrefrences();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        void LoadPrefrences()
        {
            selectedStateIndex = Convert.ToByte(Preferences.Get("selectedStateIndex",0));
            selectedDelegate = Convert.ToByte(Preferences.Get("selectedDelegate", 0));
        }
    }
}
