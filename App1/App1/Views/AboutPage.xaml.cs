using App1.ViewModels;
using Xamarin.Forms;

namespace App1.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            BindingContext = new AboutViewModel();
        }
    }
}