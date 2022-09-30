using System.ComponentModel.DataAnnotations;

namespace BlazeCart.Models
{
    internal class Item
    {
        [Key]
        public int ItemId { get; set; }
        public string CartId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
