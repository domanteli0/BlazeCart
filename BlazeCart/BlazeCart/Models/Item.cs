#nullable enable
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BlazeCart.Models
{
    [Serializable]
    public partial class Item : ObservableObject
    {
            public enum UnitOfMeasure { UNKNOWN, VNT, KG, L }
            [JsonIgnore]
            [PrimaryKey, AutoIncrement]
            public int ItemId { get; set; }
            public Guid Id { get; set; }
            [JsonIgnore]
            [ForeignKey(typeof(Cart))]
            public int CartId { get; set; }

            [JsonIgnore]
            [ManyToOne]   
            public Cart? Cart { get; set; }
            public string? Category { get; set; }
            public string? NameLT { get; set; }
            public double Price { get; set; }
            public double? DiscountPrice { get; set; }
            public double? LoyaltyPrice { get; set; }
            
            public string? LowerPrice { get; set; }
            public UnitOfMeasure? MeasureUnit { get; set; }

            [JsonIgnore]
            [ObservableProperty]
            public int quantity = 1;
            public double? Ammount { get; set; }
            public double? PricePerUnitOfMeasure { get; set; }
            public string? Description { get; set; }
            public string? Origin { get; set; }
            public Uri? Image { get; set; }

            [JsonIgnore]
            public bool IsFavorite { get; set; }
            public string? Components { get; set; }
            
            public string? Store { get; set; }
            public bool? Availability { get; set; }

    }
    
}
