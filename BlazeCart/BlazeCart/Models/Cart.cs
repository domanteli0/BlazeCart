using System.Collections.ObjectModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BlazeCart.Models
{
    public class Cart
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string  Name { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Item>  CartItems { get; set; }
        public string Image { get; set; }
        public int? ItemsCount { get; set; }

        public double? TotalPrice { get; set; }
    }
}
