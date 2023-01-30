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

        public static async Task<List<Item>> GetAllItemsAsync()
        {
            await Init();
            var items = database.GetAllWithChildren<Item>();
            if (items is null || items.Count == 0)
                return null;
            return items;
        }

        public static async Task<int> UpdateItemAsync(Item item)
        {
            await Init();
            return database.Update(item);
        }

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

        public static async Task CreateItemAsync(Item item)
        {
            await Init();
            database.InsertWithChildren(item);
        }

        public static void DeleteItemsandImages()
        {
            database.DeleteAll<Item>();
            database.DeleteAll<Image>();
        }
    }
}