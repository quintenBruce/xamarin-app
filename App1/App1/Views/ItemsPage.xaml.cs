using App1.ViewModels;
using Xamarin.Forms;

namespace App1.Views
{
    public partial class ItemsPage : ContentPage
    {
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = new ItemsViewModel();
        }
    }
}