using App1.Helpers;
using App1.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace App1.Services
{
    internal class ItemsRepository
    {
        private static SQLiteConnection database;

        private static async Task Init()
        {
            if (database == null)
            {
                database = new SQLiteConnection(System.IO.Path.Combine(FileSystem.AppDataDirectory, "Items.db3"));
                database.CreateTable<Item>();
                database.CreateTable<Image>();
            }
        }

        //returns List of Items in database. Returns null if there are no items
        public static async Task<List<Item>> GetAllItemsAsync()
        {
            await Init();
            var items = database.GetAllWithChildren<Item>();
            if (items is null || items.Count == 0)
                return null;
            return items;
        }

        public static void UpdateItem(Item item)
        {
            Init();
            database.UpdateWithChildren(item);
        }

        //Deletes item and related images
        public static async Task<int> DeleteItemAsync(Item item)
        {
            await Init();
            bool status = true;
            for (int i = 0; i < item.Images.Count; i++)
            {
                if (status)
                {
                    status = FileFunctions.DeleteFile(item.Images.ElementAt(i).Path);
                    if (status)
                        database.Delete<Image>(item.Images.ElementAt(i).Id);
                }
            }

            if (status)
                return database.Delete(item);
            return 0;
        }

        //This could be simplified. Identifies Image by path and deletes that row
        public static async Task<int> DeleteImageByPathAsync(string path)
        {
            await Init();

            var images = database.GetAllWithChildren<Image>();

            var image = images.Where(x => x.Path == path).FirstOrDefault();

            return database.Delete(image);
        }

        public static async Task<int> DeleteImage(Models.Image image)
        {
            await Init();
            return database.Delete(image);
        }

        public static async Task CreateItemAsync(Item item)
        {
            await Init();
            database.InsertWithChildren(item);
        }

        //Inserts image and returns new Image with primary key
        public static async Task<Image> CreateImageAsync(Image image)
        {
            await Init();
            database.Insert(image);
            var newImage = database.Query<Image>("SELECT * FROM Image WHERE Path = ?", image.Path).First();
            return newImage;
        }
    }
}