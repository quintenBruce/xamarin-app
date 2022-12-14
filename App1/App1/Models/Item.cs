using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class Item
    {
        [PrimaryKey] [AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; } 
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }

        


    }
}
