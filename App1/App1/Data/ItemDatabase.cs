using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App1.Models;
using SQLite;

namespace App1.Data
{
    public class ItemDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ItemDatabase(string dbPath)
        {
            database= new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Item>().Wait();
        }

        public Task<List<Item>> GetItemsAsync() => database.Table<Item>().ToListAsync();
        public Task<int> SaveItemAsync(Item item) => database.UpdateAsync(item);
        public Task<int> deleteItemAsync(Item item) => database.DeleteAsync(item);
        
    }
}
