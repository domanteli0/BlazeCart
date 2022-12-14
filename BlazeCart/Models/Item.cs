using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    // IF ANY CHANGE TO A CLASS FIELD(S) IS DONE
    // A DATABASE MIGRATION IS NECESSARY
    [Table("Item")]
    public class Item : Entity
    {
        public Item(string name, string category, double price, double amount) { 
            this.NameLT = name;
            this.Category.NameLT = category;
            this.Price = (int)(price * 100);
            this.Ammount = (float?)amount;
        }
        public enum UnitOfMeasure { UNKNOWN, VNT, KG, L }

        public string InternalID { get; set; }
        public string NameLT { get; set; }
        public string? NameEN { get; set; }
        public string? Description { get; set; }
        public UnitOfMeasure? MeasureUnit { get; set; }
        // Ammount of item sold measured by `MeasureUnit`
        public float? Ammount { get; set; }

        // Price in cents for sold unit
        public int Price { get; set; }
        public int? DiscountPrice { get; set; }
        // Price in cents with loyanty card discounts
        public int? LoyaltyPrice { get; set; }

        // Price of one unit of measurement
        // For example: a store might sell half a kilo of tomatoes for is 1.00 eu
        // Then Price would be 1.00, whilst PricePerUnitOfMeasure would be 2.00
        // ditto for `DiscountPricePerUnitOfMeasure` and `LoyaltyPricePerUnitOfMeasure`
        public int? PricePerUnitOfMeasure { get; set; }
        public int? DiscountPricePerUnitOfMeasure { get; set; }
        public int? LoyaltyPricePerUnitOfMeasure { get; set; }

        // URIs pointing to an image of that product
        public Uri? Image { get; set; }
        public Category Category { get; set; }
        public Merchendise.Merch Merch { get; set; }
        // TODO: public virtual ICollection<string>? Barcodes { get; set; }

        public Item(
            string internalID
        )
        {
            InternalID = internalID;
        }

        public Item() { }

        public void FillPerUnitOfMeasureByPrice()
        {
            throw new NotImplementedException();
        }

        public void FillPerUnitOfMeasureByAmmount()
        {
            PricePerUnitOfMeasure = (int)(Price / Ammount!);
            DiscountPricePerUnitOfMeasure = (int?)(DiscountPrice / Ammount!);
            LoyaltyPricePerUnitOfMeasure = (int?)(LoyaltyPrice / Ammount!);
        }

        public static UnitOfMeasure? ParseUnitOfMeasurement(string str)
        {
            UnitOfMeasure? ret = null;
            try
            {
                ret = Enum.Parse<UnitOfMeasure>(str.ToUpper());
            } catch (System.ArgumentException) { ret = null; }

            return ret;
        }

        public override string ToString()
        {
            return NameLT + " [" + InternalID + "]";
        }
    }
}
