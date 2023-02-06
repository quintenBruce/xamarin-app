using App1.Models;
using App1.Services;
using App1.Views;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public string Id { get; set; }
        private string itemId;
        private string notes;
        private string date;
        private Item item;
        private Page page;

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

        public Page Page_
        {
            get { return page; }
            set { page = value; }
        }

        public Item Item
        {
            get { return item; }
            set { item = value; }
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

        //Navigate to EditItemPag page
        public ICommand EditItem => new Command<Item>(async (Item item) =>
        {
            await App.Current.MainPage.Navigation.PushAsync(new EditItemPage(item));
        });

        //Prompts user for confirmation -> calls public method DeleteItemAsync by ItemsReposity to delete the item
        public ICommand DeleteItem => new Command<Item>(async (Item item) =>
        {
            var result = await Page_.DisplayAlert("Poe's App", "Are you sure you want to delete this item? (This is permanent)", "Confirm", "Cancel");

            if (result)
            {
                var status = await ItemsRepository.DeleteItemAsync(item);
                await App.Current.MainPage.Navigation.PopToRootAsync();
            }
        });

        public ItemDetailViewModel(Item item, Page page)
        {
            Page_ = page;
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