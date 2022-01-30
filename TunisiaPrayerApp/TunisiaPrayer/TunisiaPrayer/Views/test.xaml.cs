using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TunisiaPrayer.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace TunisiaPrayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class test : ContentPage
    {
        public test()
        {
            //initialize the view
            InitializeComponent();
            //set it so i can bind variables from this file to the view file
            BindingContext = this;
            //populate the elements
            statePicker.ItemsSource = App.statesData;
            delegatePicker.ItemsSource = App.statesData[App.selectedState].Delegations;

        }

        //update the delegates on state change
        public int SelectedState { 
            get { return App.selectedState; } 
            set 
            { 
                App.selectedState = value;
                delegatePicker.ItemsSource = App.statesData[App.selectedState].Delegations;
                OnPropertyChanged(nameof(delegatePicker.ItemsSource));
            } 
        }

        public int SelectedDelegate
        {
            get { return App.selectedDelegate; }
            set
            {
                App.selectedDelegate = value;
            }
        }

        private async Task hi()
        {
            List<Rootobject> deserializedProduct = new List<Rootobject>();
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(test)).Assembly;
            Stream stream = assembly.GetManifestResourceStream($"TunisiaPrayer.states.json");
            string text = "";
            using (var reader = new StreamReader(stream))
            {
                text = await reader.ReadToEndAsync();
            }

            deserializedProduct = JsonConvert.DeserializeObject<List<Rootobject>>(text);

            //OnPropertyChanged(nameof(DirectoryDisplayLabel.Text));
        }
    }
}