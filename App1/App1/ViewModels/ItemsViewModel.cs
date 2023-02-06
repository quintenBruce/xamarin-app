using App1.Models;
using App1.Services;
using App1.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private bool textVisible;
        private ObservableCollection<Item> items;
        private int _height;
        private bool itemsVisibile;

        public int Height

        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        public bool ItemsVisibile

        {
            get { return itemsVisibile; }
            set
            {
                itemsVisibile = value;
            }
        }

        public bool TextVisible
        {
            get { return textVisible; }
            set { textVisible = value; }
        }

        public ObservableCollection<Item> Items
        {
            get => items;
            set => items = value;
        }

        public bool IsRefreshing_ { get; set; }

        public ICommand RefreshList => new Command(() =>
        {
            Items = new ObservableCollection<Item>(ItemsRepository.GetAllItemsAsync().Result);

            if (Items != null)
            {
                ItemsVisibile = !(Items is null);
                TextVisible = !ItemsVisibile;

                Height = (Items is null) ? 0 : (Items.Count * 60) + (Items.Count * 5);

                IsRefreshing_ = false;
                Height = (Items.Count * 60) + (Items.Count * 5);

                OnPropertyChanged(nameof(Height));
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsRefreshing_));
            }
            IsRefreshing_ = false;
            OnPropertyChanged(nameof(IsRefreshing_));
        });

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            var filteredItems = Items.Where(x => x.Name.ToLower().Contains(query.ToLower()));
            foreach (var item in Items.ToArray())
            {
                if (!filteredItems.Contains(item))
                    Items.Remove(item);
            }
            Height = (Items.Count * 60) + (Items.Count * 5);
            OnPropertyChanged(nameof(Height));
            OnPropertyChanged(nameof(Items));
        });

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

        //Open web command does not, in fact, open anything from the web. I should change that sometime
        public ItemsViewModel()
        {
            Title = "Items";
            OpenWebCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(NewItemPage)}"));

            Items = new ObservableCollection<Item>(ItemsRepository.GetAllItemsAsync().Result);

            ItemsVisibile = !(Items is null);
            TextVisible = !ItemsVisibile;

            Height = (Items is null) ? 0 : (Items.Count * 60) + (Items.Count * 5);
        }
    }
}