using System.Collections.Generic;
using TunisiaPrayer.Models;
using TunisiaPrayer.Services;
using Xamarin.Forms;

namespace TunisiaPrayer
{
    public partial class App : Application
    {
        public static List<Rootobject> statesData { get; set; }
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
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
