using App1.Models;
using System.IO;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private FileResult photoPath { get; set; }
        public ImageSource image { get; set; }

        public FileResult PhotoPath
        {
            get
            {
                return photoPath;
            }
            set
            {
                photoPath = value;
            }
        }

        private string itemId;
        private string notes;
        private string date;

        public ICommand PickImage => new Command(async () =>
        {

            var photo = await MediaPicker.PickPhotoAsync();

            var stream = await photo.OpenReadAsync();

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
            }

            var sldf = photo.FullPath;

            image = ImageSource.FromStream(() => stream);

            OnPropertyChanged(nameof(image));

        }
        );

        public string Id { get; set; }

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

        public ItemDetailViewModel(Item item)
        {
            Title = item.Title;
            Notes = item.Notes;
            Date = item.Date.ToString("MM/dd/yyyy");
        }

        public ItemDetailViewModel()
        {
            //
        } //parameterless constructor
    }
}