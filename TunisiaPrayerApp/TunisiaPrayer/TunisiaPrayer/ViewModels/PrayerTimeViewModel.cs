using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TunisiaPrayer.Services;
using Xamarin.Forms;

namespace TunisiaPrayer.ViewModels
{
   
        public class PrayerTimeViewModel : BindableObject
        {
            public string TimeNow { get; set; }
            public string sobh { get; set; }
            public string dhohr { get; set; }
            public string aser { get; set; }
            public string magreb { get; set; }
            public string isha { get; set; }
            public ICommand RefreshTime { get; }

            public PrayerTimeViewModel()
            {
                RefreshTime = new Command(OnRefresh);
                BindingContext = this;
            }
            public async void SetTimes()
            {
                Prayer rootobject = await Prayers.GetTime(1, 1);
                TimeNow = DateTime.Now.ToString("dd-MM-yyyy");
                sobh = rootobject.data.sobh;
                dhohr = rootobject.data.dhohr;
                aser = rootobject.data.aser;
                magreb = rootobject.data.magreb;
                isha = rootobject.data.isha;
                OnPropertyChanged(nameof(sobh));
            }

            void tet()
            {
                sobh = "test";
                OnPropertyChanged();
            }

            void OnRefresh()
            {
                sobh = "...";
                dhohr = "...";
                aser = "...";
                magreb = "...";
                isha = "...";
                SetTimes();
            }
        }
    
}
