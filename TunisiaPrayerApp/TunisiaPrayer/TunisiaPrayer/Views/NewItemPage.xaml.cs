using System;
using System.Collections.Generic;
using System.ComponentModel;
using TunisiaPrayer.Models;
using TunisiaPrayer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TunisiaPrayer.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}