using App1.Models;
using App1.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string text;
        private string description;












        public ICommand PickImage => new Command(async () =>
        {

            var photo = await MediaPicker.PickPhotoAsync();

            var stream = await photo.OpenReadAsync();



            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

            using (var newStream = File.OpenWrite(newFile))
            {
                await stream.CopyToAsync(newStream);

            }

            var photoPath = newFile;

            var images = new List<App1.Models.Image>
                {
                    new App1.Models.Image { Path= photoPath}
                };

            ItemsRepository.CreateItemAsync(new Item { Images = images });




            var allItems = await ItemsRepository.GetItemsAsync();


            Image = ImageSource.FromFile(allItems.Last().Images.Last().Path);


            var sdffff = "gg";


        }
        );
































        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}