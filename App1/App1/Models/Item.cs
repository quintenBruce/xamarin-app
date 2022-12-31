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

        public string Name { get; set; }
        public string Notes { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Image> Images { get; set; }

        public DateTime Date { get; set; }
    }
}