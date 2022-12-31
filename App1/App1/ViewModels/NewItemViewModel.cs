using App1.Helpers;
using App1.Models;
using App1.Services;
using System;
using System.Collections.Generic;
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
        private string name;
        private string notes;

        private ObservableCollection<App1.Models.Image> images;

        public ObservableCollection<App1.Models.Image> Images
        {
            get { return images; }
            set
            {
                images = value;
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }

        public string Notes
        {
            get => notes;
            set
            {
                notes = value;
            }
        }

        // PickImage Command writes stream given by Xamarin.Essentials.MediaPicker.PickPhotoAsync()
        // to new file in applications cache.
        // A new Image object with path property set to new file will be added to Images observable collection

        public ICommand PickImage => new Command(async () =>
        {
            var photo = await MediaPicker.PickPhotoAsync();
            var stream = await photo.OpenReadAsync();
            var photoPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

            var status = await FileFunctions.WriteFile(stream, photoPath);

            if (status)
                Images.Add(new Models.Image() { Path = photoPath });
        });

        public ICommand AddItem => new Command(async () =>
        {
            if (!ValidateSave())
                return;

            var item = new Item();
            item.Notes = notes;
            item.Name = name;
            item.Date = DateTime.Now;
            item.Images = (List<Models.Image>)images.ToList<App1.Models.Image>();

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

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Name);
        }
    }
}