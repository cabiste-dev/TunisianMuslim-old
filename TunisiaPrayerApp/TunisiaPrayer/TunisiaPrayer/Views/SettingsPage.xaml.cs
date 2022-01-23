using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TunisiaPrayer.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TunisiaPrayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            GetJSON();

        }

        private void GetJSON()
        {
            StateData stateData = new StateData();
            //string jsonFileName = "states.json";
            var assembly = typeof(SettingsPage).GetTypeInfo().Assembly;
            var x = Directory.GetFiles(assembly.GetName().FullName);
            //stateData = JsonConvert.DeserializeObject<StateData>($"{assembly.GetName().Name}.{jsonFileName}");
            //Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            delegatePicker.ItemsSource = x;
            //using (var reader = new StreamReader(stream))
            //{
            //    var jsonString = reader.ReadToEnd();

            //    //Converting JSON Array Objects into generic list    
            //    stateData = JsonConvert.DeserializeObject<StateData>(jsonString);
            //}
            //Binding listview with json string     
            //theme.Text = stateData.Property1[0].data.delegation.Length.ToString();

            //JsonSerializer jsonSerializer = new JsonSerializer();
            //JsonReader jsonReader;
            //using (TextReader ts = new StreamReader(stream))
            //{
            //    jsonReader = new JsonTextReader(ts);
            //    stateData = jsonSerializer.Deserialize<StateData>(jsonReader);
            //}
        }

        
    }
}