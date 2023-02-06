using App1.Models;
using App1.ViewModels;
using Xamarin.Forms;

namespace App1.Views
{
    public partial class EditItemPage : ContentPage
    {
        public EditItemPage(Item item)
        {
            InitializeComponent();

            BindingContext = new EditItemViewModel(item, this);
        }
    }
}