using System;
using System.Text;

namespace Scraper
{
    public class Item
    {
        public enum unitOfMeasure { VNT, KG, L }
        public string InternalID { get; set; }
        public string? NameEN { get; set; }
        public string? NameLT { get; set; }
        public string? Description { get; set; }
        public unitOfMeasure MeasureUnit { get; set; }
        public float Ammount { get; set; }

        // Price in cents for sold unit
        public int Price { get; set; }
        public int? DiscountPrice { get; set; }

        // Price in cents with loyanty card discounts
        public decimal? LoyaltyPrice { get; set; }

        // Price of one unit of measurement
        // For example: a store might sell half a kilo of tomatoes for is 1.00 eu
        // Then Price would be 1.00, whilst PricePerUnitOfMeasure would be 2.00
        // ditto for `DiscountPricePerUnitOfMeasure` and `LoyaltyPricePerUnitOfMeasure`
        public int PricePerUnitOfMeasure { get; set; }
        public int DiscountPricePerUnitOfMeasure { get; set; }
        public int LoyaltyPricePerUnitOfMeasure { get; set; }

        // URIs pointing to an image of that product
        public List<Uri> Images { get; set; }
        public List<Category> Categories { get; set; }
        public List<String>? Barcodes { get; set; }
        public List<Store> AvailableAt { get; set; }

        public Item()
        {
            Images = new List<Uri>();
            Categories = new List<Category>();
            AvailableAt = new List<Store>();
        }

        // <https://stackoverflow.com/questions/4023462/how-do-i-automatically-display-all-properties-of-a-class-and-their-values-in-a-s>
        public override string ToString()
        {
            return GetType().GetProperties()
                .Select(info => (info.Name, Value: info.GetValue(this, null) ?? "(null)"))
                .Aggregate(
                    new StringBuilder(),
                    (sb, pair) => sb.AppendLine($"{pair.Name}: {pair.Value}"),
                    sb => sb.ToString());
        }
    }
}
