using System;
using System.Text;

namespace Scraper
{
    public class Product
    {
        public enum unitOfMeasure { VNT, KG, L }
        public string InternalID { get; set; }
        public string? NameEN { get; set; }
        public string? NameLT { get; set; }
        public string? Description { get; set; }
        public unitOfMeasure MeasureUnit { get; set; }
        public float Ammount { get; set; }

        // Price in cents
        public int Price { get; set; }
        public int? DiscountPrice { get; set; }

        // Price in cents with loyanty card discounts
        public decimal? LoyaltyPrice { get; set; }

        // URIs pointing to an image of that product
        public List<Uri> Images { get; set; }
        public List<Category> Categories { get; set; }
        public List<String> Barcodes { get; set; }
        public List<Store> AvailableAt { get; set; }

        public Product()
        {
            Images = new List<Uri>();
            Categories = new List<Category>();
            Barcodes = new List<String>();
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
