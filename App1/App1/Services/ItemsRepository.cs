using App1.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
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

        public static async Task<List<Item>> GetItemsAsync()
        {
            await Init();
            return database.GetAllWithChildren<Item>();
        }

        public static async Task<int> UpdateItemAsync(Item item)
        {
            await Init();
            return database.Update(item);
        }

        public static async Task<int> DeleteItemAsync(Item item)
        {
            await Init();
            return database.Delete(item);
        }

        public static async Task CreateItemAsync(Item item)
        {
            await Init();
            database.InsertWithChildren(item);
        }
    }
}