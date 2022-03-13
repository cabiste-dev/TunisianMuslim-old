using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TunisiaPrayer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TunisiaPrayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChapterPage : ContentPage
    {
        public ChapterPage()
        {
            InitializeComponent();
            BindingContext = new ChapterViewModel();
        }
    }
}