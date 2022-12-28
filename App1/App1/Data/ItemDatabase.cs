using App1.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App1.Data
{
    public class ItemDatabase
    {
        private readonly SQLiteAsyncConnection database;

        public ItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Item>().Wait();
        }

        public Task<List<Item>> GetItemsAsync() => database.Table<Item>().ToListAsync();

        public Task<int> SaveItemAsync(Item item) => database.UpdateAsync(item);

        public Task<int> deleteItemAsync(Item item) => database.DeleteAsync(item);
    }
}