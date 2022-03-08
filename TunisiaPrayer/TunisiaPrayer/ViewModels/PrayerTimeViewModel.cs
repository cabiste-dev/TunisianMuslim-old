using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TunisiaPrayer.Services;

namespace TunisiaPrayer.ViewModels
{

    public class PrayerTimeViewModel : BaseViewModel
    {
        public string TimeNow { get; set; }
        public List<string> prayersTime { get; set; }
        public ICommand RefreshTime { get; }

        public PrayerTimeViewModel()
        {
            RefreshTime = new AsyncCommand(SetTimes);
            if (App.statesData != null)
            {
                SetTimes();
            }
        }

        public string areaSelected { get; set; }

        void setArea()
        {
            AreaSelected = App.statesData[App.selectedStateIndex].NameEn + ", " + App.statesData[App.selectedStateIndex].Delegations[App.selectedDelegateIndex].NameEn;
        }
        public string AreaSelected
        {
            get
            {
                return areaSelected;
            }
            set
            {
                areaSelected = value;
                OnPropertyChanged(nameof(AreaSelected));
            }
        }

        public async Task SetTimes()
        {
            prayersTime = await Prayers.GetTime(App.statesData[App.selectedStateIndex].Id, App.statesData[App.selectedStateIndex].Delegations[App.selectedDelegateIndex].Id);
            TimeNow = DateTime.Now.ToString("dd-MM-yyyy");
            setArea();
            OnPropertyChanged(nameof(AreaSelected));
            OnPropertyChanged(nameof(prayersTime));
        }
    }

}
