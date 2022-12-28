using App1.Models;
using App1.ViewModels;
using Xamarin.Forms;

namespace App1.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item_ Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}