using App1.Models;
using App1.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {

        public ImageSource image { get; set; }
        public ImageSource Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged(nameof(Image));

            }
        }



        

        

        public string Id { get; set; }
        private string itemId;
        private string notes;
        private string date;

        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public string Notes
        {
            get => notes;
            set => SetProperty(ref notes, value);
        }

        public ItemDetailViewModel(Item item)
        {
            Title = item.Title;
            Notes = item.Notes;
            Date = item.Date.ToString("MM/dd/yyyy");
        }

        public ItemDetailViewModel()
        {
            //
        } //parameterless constructor
    }
}