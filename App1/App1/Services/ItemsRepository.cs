using App1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using App1.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms.Shapes;

namespace App1.Services
{
    class ItemsRepository
    {
        static SQLiteConnection database;

        static async Task Init()
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
