using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.ViewModels;

namespace App1.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            BindingContext = new AboutViewModel();

            
        }

        public void OnTextChanged(object sender, EventArgs e)
        {
            
        }

        public async void OnItemTapped(object o, ItemTappedEventArgs e)
        {

            var duration = TimeSpan.FromMilliseconds(40);
            Vibration.Vibrate(duration);


            await Navigation.PushAsync(new ItemDetailPage("Hello Stupid O"));
                
            
            
        }
    }
}