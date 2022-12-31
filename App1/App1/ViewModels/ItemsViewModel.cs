using App1.Models;
using App1.Services;
using App1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand OpenWebCommand { get; }

        public ICommand Navigate => new Command<Item>((Item item) =>
        {
            var duration = TimeSpan.FromMilliseconds(40);
            Vibration.Vibrate(duration);

            App.Current.MainPage.Navigation.PushAsync(new ItemDetailPage(item));
        });

        public ICommand AddItem => new Command(() =>
        {
            var duration = TimeSpan.FromMilliseconds(40);
            Vibration.Vibrate(duration);

            App.Current.MainPage.Navigation.PushAsync(new NewItemPage());
        }
        );

        public ObservableCollection<Item> Items { get; set; }
        public bool IsRefreshing_ { get; set; }

        public ICommand RefreshList => new Command(() =>
        {
            var newItems = new ObservableCollection<Item>
            {
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Bookshelves"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Alen Desk"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Console"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Entry Console"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Barndoor"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Outdoor Sofa Table"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Table"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Bed"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Dresser"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Desk"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Bench"}
            };

            foreach (var item in Items.ToArray())
                Items.Remove(item);

            foreach (var item in newItems)
            {
                if (!Items.ToArray().Contains(item))
                    Items.Add(item);
            }

            IsRefreshing_ = false;
            Height = (Items.Count * 60) + (Items.Count * 5);

            OnPropertyChanged(nameof(Height));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsRefreshing_));
        });

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            var filteredItems = Items.Where(x => x.Title.ToLower().Contains(query.ToLower()));
            foreach (var item in Items.ToArray())
            {
                if (!filteredItems.Contains(item))
                    Items.Remove(item);
            }
            Height = (Items.Count * 60) + (Items.Count * 5);
            OnPropertyChanged(nameof(Height));
            OnPropertyChanged(nameof(Items));
        });

        public void GoToPage()
        {
            HapticFeedback.Perform(HapticFeedbackType.LongPress);
            App.Current.MainPage.Navigation.PushAsync(new Page1());
        }

        private int _height;

        public int Height

        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        public ItemsViewModel()
        {
            Title = "Items";
            OpenWebCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(NewItemPage)}"));

            Items = new ObservableCollection<Item>
            {
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Bookshelves", Images =  new List<App1.Models.Image> { new App1.Models.Image { Path = "/data/user/0/com.companyname.app1/cache/20221228_131837.jpg" }, new App1.Models.Image { Path = "/data/user/0/com.companyname.app1/cache/20221228_131837.jpg" } }},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "ThThisis", Title = "Alen Desk", Images =  new List<App1.Models.Image> {  new App1.Models.Image { Path = "/data/user/0/com.companyname.app1/cache/20221228_131837.jpg" } }},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Console", Images =  new List<App1.Models.Image> {  new App1.Models.Image { Path = "/data/user/0/com.companyname.app1/cache/20221228_131837.jpg" }, new App1.Models.Image { Path = "/data/user/0/com.companyname.app1/cache/20221228_131837.jpg" }, new App1.Models.Image { Path = "/data/user/0/com.companyname.app1/cache/20221228_131837.jpg" } }},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "ThThisThisThisThisThisis", Title = "Entry Console", },
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "ThiThiss", Title = "Barndoor"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Outdoor Sofa Table"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Console"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "ThThisThisThisThisThisis", Title = "Entry Console"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Barndoor"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Outdoor Sofa Table"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "ThThisThisThisThisThisis", Title = "Table"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "ThThisThisis", Title = "Bed"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Table"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "ThThisis", Title = "Bed"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "ThiThiss", Title = "Dresser"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Desk"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Console"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "ThThisThisis", Title = "Entry Console"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Barndoor"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "ThThisis", Title = "Outdoor Sofa Table"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "ThThisThisThisis", Title = "Table"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Title = "Bed"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "ThiThisThisThisThiss", Title = "Bench"}
            };

            Height = (Items.Count * 60) + (Items.Count * 5);
        }
    }
}