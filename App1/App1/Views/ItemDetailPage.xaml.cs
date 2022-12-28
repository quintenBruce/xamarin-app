using App1.Models;
using App1.ViewModels;
using Xamarin.Forms;

namespace App1.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public string YESir { get; set; }

        public ItemDetailPage(Item item)
        {
            InitializeComponent();

            BindingContext = new ItemDetailViewModel(item);
        }
    }
}