using App1.Data;
using System;
using System.IO;
using Xamarin.Forms;

namespace App1
{
    public partial class App : Application
    {
        private static ItemDatabase database;

        public static ItemDatabase Databasee
        {
            get
            {
                if (database == null)
                {
                    database = new ItemDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Items.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}