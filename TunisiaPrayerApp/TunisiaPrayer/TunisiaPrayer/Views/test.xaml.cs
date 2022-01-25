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
            InitializeComponent();
            try
            {
                DirectoryDisplayLabel.Text = App.statesData[0].data.gouvernorat.intituleAr;
                
                for(short i = 0; i< App.statesData.Count; i++)
                {
                    statePicker.Items.Add(App.statesData[i].data.gouvernorat.intituleAn);
                }
            }
            catch (Exception ex)
            {
                DirectoryDisplayLabel.Text = ex.Message;
            }
            //hi();
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

            DirectoryDisplayLabel.Text = $"it worked :D \n there are {deserializedProduct[2].data.gouvernorat.id}";
            //OnPropertyChanged(nameof(DirectoryDisplayLabel.Text));
        }
    }
}