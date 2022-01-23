using System.ComponentModel;
using TunisiaPrayer.ViewModels;
using Xamarin.Forms;

namespace TunisiaPrayer.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}