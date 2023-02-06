using App1.Helpers;
using App1.Models;
using App1.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private Item item = new Item();
        private string name;
        private string notes;
        private bool coverImageButtonVisible = true; //if image is visible, then button is not. visa versa
        private bool coverImageVisible = false;
        private ObservableCollection<App1.Models.Image> images;
        private string coverImagePath;

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Name);
        }

        public string CoverImagePath
        {
            get
            {
                return coverImagePath;
            }
            set
            {
                coverImagePath = value;

                CoverImageVisible = !(CoverImagePath is null);
                CoverImageButtonVisible = !CoverImageVisible;
                OnPropertyChanged(nameof(CoverImageButtonVisible));
                OnPropertyChanged(nameof(coverImageVisible));
                OnPropertyChanged(nameof(CoverImagePath));
            }
        }

        public bool CoverImageButtonVisible
        {
            get { return coverImageButtonVisible; }
            set { coverImageButtonVisible = value; }
        }

        public bool CoverImageVisible
        {
            get { return coverImageVisible; }
            set { coverImageVisible = value; }
        }

        public ObservableCollection<App1.Models.Image> Images
        {
            get { return images; }
            set { images = value; }
        }

        public string Name
        {
            get => name;
            set { name = value; }
        }

        public string Notes
        {
            get => notes;
            set { notes = value; }
        }

        //I'm not sure why I included the random number stuff..but this command writes the chosen file to cache and sets path to CoverImagePath Property and item field
        public ICommand PickCoverImage => new Command(async () =>
        {
            var photo = await MediaPicker.PickPhotoAsync();
            var stream = await photo.OpenReadAsync();

            Random rnd = new Random();
            int num = rnd.Next(100000);

            var photoPath = Path.Combine(FileSystem.CacheDirectory, (photo.FileName + num.ToString()));
            photoPath = FileFunctions.CheckFilePath(photoPath);
            var status = await FileFunctions.WriteFile(stream, photoPath);

            if (status)
                item.CoverImagePath = CoverImagePath = photoPath;
        });

        // PickImage Command writes stream given by Xamarin.Essentials.MediaPicker.PickPhotoAsync()
        // to new file in applications cache.
        // A new Image object with path property set to new file will be added to Images observable collection

        public ICommand PickImage => new Command(async () =>
        {
            var photo = await MediaPicker.PickPhotoAsync();
            var stream = await photo.OpenReadAsync();
            var photoPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            photoPath = FileFunctions.CheckFilePath(photoPath);
            var status = await FileFunctions.WriteFile(stream, photoPath);

            if (status)
                Images.Add(new Models.Image() { Path = photoPath });
        });

        //inserts new item into database if the form is valid -> navigates to root page
        public ICommand AddItem => new Command(async () =>
        {
            if (!ValidateSave())
                return;

            item.Notes = notes;
            item.Name = name;
            item.Date = DateTime.Now;
            item.Images = images.ToList<App1.Models.Image>();

            await ItemsRepository.CreateItemAsync(item);

            await Shell.Current.GoToAsync("..");
        });

        // CancelCommand Command will remove images from cache
        // and pop current page off the navigation stack

        public ICommand CancelCommand => new Command(async () =>
        {
            foreach (var image in images)
            {
                var stutus = FileFunctions.DeleteFile(image.Path);
            }

            await Shell.Current.GoToAsync("..");
        });

        public NewItemViewModel()
        {
            Name = name;
            Notes = notes;
            images = new ObservableCollection<App1.Models.Image>();
            Images = images;
        }
    }
}