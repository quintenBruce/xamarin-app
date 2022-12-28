using SQLite;
using SQLiteNetExtensions.Attributes;

namespace App1.Models
{
    public class Image
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Item))]
        public int ItemId { get; set; }

        public string Path { get; set; }

        [ManyToOne]
        public Item Item { get; set; }
    }
}