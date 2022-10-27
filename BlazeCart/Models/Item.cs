using System;
using System.Text;

namespace Models
{
    public class Item
    {
        public enum UnitOfMeasure { UNKNOWN, VNT, KG, L }

        public string InternalID { get; private set; }
        private string _nameLT;
        private string? _nameEN;
        private string? _description;
        private UnitOfMeasure? _measureUnit;
        // Ammount of item sold measured by `MeasureUnit`
        private float? _ammount;

        // Price in cents for sold unit
        private int _price;
        private int? _discountPrice;
        // Price in cents with loyanty card discounts
        private int? _loyaltyPrice;

        // Price of one unit of measurement
        // For example: a store might sell half a kilo of tomatoes for is 1.00 eu
        // Then Price would be 1.00, whilst PricePerUnitOfMeasure would be 2.00
        // ditto for `DiscountPricePerUnitOfMeasure` and `LoyaltyPricePerUnitOfMeasure`
        private int? _pricePerUnitOfMeasure;
        private int? _discountPricePerUnitOfMeasure;
        private int? _loyaltyPricePerUnitOfMeasure;

        // URIs pointing to an image of that product
        private Uri? _image;
        //private List<Category> _categories;
        private List<String>? _barcodes;

        public Item(
            string internalID,
            string nameLT,
            int price,
            Uri? image,
            string measureUnit,

            int? pricePerUnitOfMeasure = null,

            string? nameEN = null,
            string? description = null,
            int? discountPrice = null,
            int? loyaltyPrice = null,
            float? ammount = null,
            List<String>? barcodes = null
        )
        {
            InternalID = internalID;
            _nameLT = nameLT;
            _price = price;
            _image = image;
            _pricePerUnitOfMeasure = pricePerUnitOfMeasure;
            _nameEN = nameEN;
            _description = description;
            _discountPrice = discountPrice;
            _loyaltyPrice = loyaltyPrice;
            _ammount = ammount;
            _barcodes = barcodes;


            if (measureUnit is not null)
                _measureUnit = ParseUnitOfMeasurement(measureUnit);
        }



        public void FillPerUnitOfMeasureByPrice()
        {

        }

        public void FillPerUnitOfMeasureByAmmount()
        {
            _pricePerUnitOfMeasure = (int) (_price /  _ammount!);
            _discountPricePerUnitOfMeasure = (int?) (_discountPrice / _ammount!);
            _loyaltyPricePerUnitOfMeasure = (int?) (_loyaltyPrice / _ammount!);
        }

        private UnitOfMeasure? ParseUnitOfMeasurement(string str)
        {
            UnitOfMeasure? ret = null;
            try
            {
                ret = Enum.Parse<UnitOfMeasure>(str.ToUpper());
            } catch (System.ArgumentException) { ret = null; }

            return ret;
        }

        // <https://stackoverflow.com/questions/4023462/how-do-i-automatically-display-all-properties-of-a-class-and-their-values-in-a-s>
        public override string ToString()
        {
            return _nameLT + " [" + InternalID + "]";
        }
    }
}
