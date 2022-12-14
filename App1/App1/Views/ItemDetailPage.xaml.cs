using App1.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace App1.Views
{
    public partial class ItemDetailPage : ContentPage
    {

        public string YESir { get; set; }


        public ItemDetailPage(string Title)
        {

            
            InitializeComponent();

            BindingContext = new ItemDetailViewModel(Title);

            

        }
    }
}