using App1.Models;
using App1.ViewModels;
using App1.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    public partial class ItemsPage : ContentPage
    {
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = new ItemsViewModel();


        }

        public void OnTextChanged(object sender, EventArgs e)
        {
            listView.ItemsSource = new List<string>();
        }

        public async void OnItemTapped(object o, ItemTappedEventArgs e)
        {

            var duration = TimeSpan.FromMilliseconds(40);
            Vibration.Vibrate(duration);


            await Navigation.PushAsync(new ItemDetailPage("Hello Stupid O"));



        }
    }
}