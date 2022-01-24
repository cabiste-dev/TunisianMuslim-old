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
            public List<string> prayersTime { get; set; }
            public ICommand RefreshTime { get; }

            public PrayerTimeViewModel()
            {
                RefreshTime = new Command(OnRefresh);
                BindingContext = this;
            }
            public async void SetTimes()
            {
                prayersTime = await Prayers.GetTime(361 , 634);
                TimeNow = DateTime.Now.ToString("dd-MM-yyyy");
                OnPropertyChanged(nameof(prayersTime));
            }

            

            void OnRefresh()
            {
                SetTimes();
            }
        }
    
}
