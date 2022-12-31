using App1.Models;
using App1.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand OpenWebCommand { get; }
        public ICommand Navigate_ { get; }

        public ObservableCollection<Item> Items { get; set; }
        public bool IsRefreshing_ { get; set; }

        public ICommand RefreshList => new Command(() =>
        {
            var newItems = new ObservableCollection<Item>
            {
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Name = "Bookshelves"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Name = "Alan Desk"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Name = "Console"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Name = "Barndoor"},
            };

            foreach (var item in Items.ToArray())
                Items.Remove(item);

            foreach (var item in newItems)
            {
                if (!Items.ToArray().Contains(item))
                    Items.Add(item);
            }

            IsRefreshing_ = false;

            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsRefreshing_));
        });

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            var filteredItems = Items.Where(x => x.Name.Contains(query));
            foreach (var item in Items.ToArray())
            {
                if (!filteredItems.Contains(item))
                    Items.Remove(item);
            }

            OnPropertyChanged(nameof(Items));
        });

        public void GoToPage()
        {
            HapticFeedback.Perform(HapticFeedbackType.LongPress);
            App.Current.MainPage.Navigation.PushAsync(new Page1());
        }

        public AboutViewModel()
        {
            Title = "About--";
            OpenWebCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(NewItemPage)}"));

            Navigate_ = new Command(() => GoToPage());

            Items = new ObservableCollection<Item>
            {
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Name = "Bookshelves"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Name = "Alan Desk"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Name = "Console"},
                new Item {Id = 3, Date = DateTime.Now.Date, Notes = "This", Name = "Barndoor"},
            };
        }
    }
}