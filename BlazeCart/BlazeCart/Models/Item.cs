using System.ComponentModel.DataAnnotations;

namespace BlazeCart.Models
{
    public class Item
    {
        [Key]

            public int ItemId { get; set; }
            public string Category { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public string Units { get; set; }
            public double PackageAmount { get; set; }
            public double PricePerUnit { get; set; }
            public string Description { get; set; }
            public string Origin { get; set; }
            public string Image { get; set; }
            public string Components { get; set; }
            public string Store { get; set; }
            public bool Availability { get; set; }

    }
}
