using App1.Helpers;
using App1.Models;
using App1.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class EditItemViewModel : BaseViewModel
    {
        public ICommand OpenWebCommand { get; }
        private string notes;
        private string name;
        private string newCoverImagePath;
        private List<Models.Image> tempImages = new List<Models.Image>();
        private ObservableCollection<App1.Models.Image> images;
        private readonly Page page_;

        private ObservableCollection<Models.Image> RemoveImageFromCollections(ObservableCollection<Models.Image> collection, string path)
        {
            var newCollection = new ObservableCollection<Models.Image>();
            foreach (var image in collection)
            {
                if (image.Path != path)
                {
                    newCollection.Add(image);
                }
            }

            return newCollection;
        }

        public ObservableCollection<App1.Models.Image> Images
        {
            get
            {
                return images;
            }
            set
            {
                images = value;
                OnPropertyChanged(nameof(Images));
            }
        }

        public Item Item { get; set; }

        public string CoverImagePath
        {
            get
            {
                return newCoverImagePath;
            }
            set
            {
                newCoverImagePath = value;
                OnPropertyChanged(nameof(CoverImagePath));
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        // removes temporary images from cache and navigates to root page
        public ICommand CancelCommand => new Command(async () =>
        {
            foreach (var image in tempImages)
            {
                var status = await ItemsRepository.DeleteImage(image);
                if (status > 0)
                {
                    FileFunctions.DeleteFile(image.Path);
                }
            }

            if (Item.CoverImagePath != newCoverImagePath)
                FileFunctions.DeleteFile(newCoverImagePath);

            await App.Current.MainPage.Navigation.PopToRootAsync();
        });

        //Prompts User to confirm decision -> removes image from both cache and database
        public ICommand DeleteImage => new Command<string>(async (string path) =>
        {
            var result = await page_.DisplayAlert("Poe's App", "Are you sure you want to delete this image? (This is permanent)", "Confirm", "Cancel");

            if (result)
            {
                var status = FileFunctions.DeleteFile(path);
                if (status)
                {
                    Images = RemoveImageFromCollections(Images, path);
                    await ItemsRepository.DeleteImageByPathAsync(path);
                }
            }
        });

        //Writes user chosen image to file and inserts image into database
        public ICommand AddImage => new Command(async () =>
        {
            var photo = await MediaPicker.PickPhotoAsync();
            var stream = await photo.OpenReadAsync();
            var photoPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            photoPath = FileFunctions.CheckFilePath(photoPath);
            var status = await FileFunctions.WriteFile(stream, photoPath);

            if (status)
            {
                //insert image into database (UpdateWithChildren requires all references to have primary keys)
                var image = await ItemsRepository.CreateImageAsync(new Models.Image() { Path = photoPath });
                tempImages.Add(image);
                Images.Add(image);
            }
        });

        //Apply's all changes to Item object and calls ItemsRepository to update items and children -> navigates to root page
        public ICommand Save => new Command(async () =>
        {
            Item.Name = Name;
            Item.Notes = Notes;
            Item.CoverImagePath = CoverImagePath;
            Item.Images = Images.ToList<App1.Models.Image>();
            ItemsRepository.UpdateItem(Item);

            await App.Current.MainPage.Navigation.PopToRootAsync();
        });

        //Writes user chosen image to file and sets property to path
        public ICommand ChangeCoverImage => new Command(async () =>
        {
            var photo = await MediaPicker.PickPhotoAsync();
            var stream = await photo.OpenReadAsync();
            var photoPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            photoPath = FileFunctions.CheckFilePath(photoPath);
            var status = await FileFunctions.WriteFile(stream, photoPath);

            if (status)
                CoverImagePath = photoPath;
        });

        public EditItemViewModel(Item item, Page page)
        {
            page_ = page;
            Name = item.Name;
            Item = item;
            Notes = item.Notes;
            Images = new ObservableCollection<Models.Image>(item.Images);

            CoverImagePath = item.CoverImagePath;
        }
    }
}