using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace App1.Models
{
    public class Item
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Notes { get; set; }

        [OneToMany]
        public List<Image> Images { get; set; }

        public DateTime Date { get; set; }
    }
}