using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class Image
    {
        [PrimaryKey] [AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(Item))]
        public int ItemId { get; set; }
        public string Path { get; set; }

        [ManyToOne]
        public Item Item { get; set; }
        
    }
}
