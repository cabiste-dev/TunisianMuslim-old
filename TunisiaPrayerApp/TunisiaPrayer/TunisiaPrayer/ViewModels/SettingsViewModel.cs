using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using TunisiaPrayer.Models;
using Xamarin.Essentials;

namespace TunisiaPrayer.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {

        public List<Rootobject> states { get; set; }
        public List<Delegation> delegates { get; set; }
        public string selectedState { get; set; } = App.statesData[App.selectedStateIndex].NameEn;
        public string selectedDelegate { get; set; } = App.statesData[App.selectedStateIndex].Delegations[App.selectedDelegateIndex].NameEn;

        public SettingsViewModel()
        {
            //populate the elements
            states= App.statesData;
            delegates = App.statesData[App.selectedStateIndex].Delegations;
        }



        //update the delegates on state change
        public byte SelectedStateIndex
        {
            get { return App.selectedStateIndex; }
            set
            {
                App.selectedStateIndex = value;
                delegates = App.statesData[value].Delegations;
                selectedDelegate = App.statesData[value].Delegations[0].NameEn;
                OnPropertyChanged(nameof(delegates));
                OnPropertyChanged(nameof(selectedDelegate));
                selectedState = App.statesData[value].NameEn;
                Preferences.Set("selectedStateIndex", value);
            }
        }

        public byte SelectedDelegateIndex
        {
            get { return App.selectedDelegateIndex; }
            set
            {
                App.selectedDelegateIndex = value;
                selectedDelegate = App.statesData[App.selectedStateIndex].Delegations[value].NameEn;
                Preferences.Set("selectedDelegate", value);

            }
        }
    }
}
