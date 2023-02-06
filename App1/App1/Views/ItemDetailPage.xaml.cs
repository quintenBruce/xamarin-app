using App1.Models;
using App1.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage(Item item)
        {
            InitializeComponent();

            BindingContext = new ItemDetailViewModel(item, this);
        }
    }
}