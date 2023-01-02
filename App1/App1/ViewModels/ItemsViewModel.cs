using App1.Models;
using App1.Services;
using App1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
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

        private ObservableCollection<Item> items;

        public ObservableCollection<Item> Items
        {
            get => items;
            set => items = value;
        }

        public bool IsRefreshing_ { get; set; }

        public ICommand RefreshList => new Command(() =>
        {
            Items = ListToObservableCollection(ItemsRepository.GetAllItemsAsync().Result);

            ItemsVisibile = !(Items is null);
            TextVisible = !ItemsVisibile;

            Height = (Items is null) ? 0 : (Items.Count * 60) + (Items.Count * 5);

            IsRefreshing_ = false;
            Height = (Items.Count * 60) + (Items.Count * 5);

            OnPropertyChanged(nameof(Height));
            OnPropertyChanged(nameof(Items));
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

        private bool itemsVisibile;

        public bool ItemsVisibile

        {
            get { return itemsVisibile; }
            set
            {
                itemsVisibile = value;
            }
        }

        private bool textVisible;

        public bool TextVisible
        {
            get { return textVisible; }
            set { textVisible = value; }
        }

        public ItemsViewModel()
        {
            Title = "Items";
            OpenWebCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(NewItemPage)}"));

            Items = ListToObservableCollection(ItemsRepository.GetAllItemsAsync().Result);

            ItemsVisibile = !(Items is null);
            TextVisible = !ItemsVisibile;

            Height = (Items is null) ? 0 : (Items.Count * 60) + (Items.Count * 5);
        }

        public ObservableCollection<Item> ListToObservableCollection(List<Item> items)
        {
            if (items == null)
                return null;
            var collection = new ObservableCollection<Item>();
            foreach (var item in items)
                collection.Add(item);

            return collection;
        }
    }
}