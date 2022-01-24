using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TunisiaPrayer.Services;

namespace TunisiaPrayer.ViewModels
{

    public class PrayerTimeViewModel : BaseViewModel
    {
        public string TimeNow { get; set; }
        public List<string> prayersTime { get; set; }
        //public StackLayout stackLayout { get; set; }
        public ICommand RefreshTime { get; }

        public PrayerTimeViewModel()
        {
            RefreshTime = new AsyncCommand(SetTimes);
            //BindingContext = this;
        }
        public async Task SetTimes()
        {
            prayersTime = await Prayers.GetTime(361, 634);
            TimeNow = DateTime.Now.ToString("dd-MM-yyyy");
            OnPropertyChanged(nameof(prayersTime));
        }



        void OnRefresh()
        {
            SetTimes();
        }
    }

}
