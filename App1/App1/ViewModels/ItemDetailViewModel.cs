using App1.Models;
using App1.Services;
using App1.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public List<Models.Image> images;

        public List<Models.Image> Images
        {
            get { return images; }
            set
            {
                images = value;
                OnPropertyChanged(nameof(Image));
            }
        }

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
        private Item item;

        public Item Item
        {
            get { return item; }
            set
            {
                item = value;
            }
        }

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

        public ICommand DeleteItem => new Command<Item>(async (Item item) =>
        {
            var status = await ItemsRepository.DeleteItemAsync(item);
            await App.Current.MainPage.Navigation.PushAsync(new ItemsPage());
        });

        public ItemDetailViewModel(Item item)
        {
            Item = item;
            Images = item.Images;
            Title = item.Name;
            Notes = item.Notes;
            Date = item.Date.ToString("MM/dd/yyyy");
        }

        public ItemDetailViewModel()
        {
        }
    }
}