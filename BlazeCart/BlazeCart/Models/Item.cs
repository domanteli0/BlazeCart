#nullable enable
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BlazeCart.Models
{
    [Serializable]
    public partial class Item : ObservableObject
    {
            [PrimaryKey, AutoIncrement]
            public int ItemId { get; set; }
            [ForeignKey(typeof(Cart))]
            public int CartId { get; set; }
            [ManyToOne]   
            public Cart? Cart { get; set; }
            public string? Category { get; set; }
            public string? Name { get; set; }
            public double Price { get; set; }
            public string? Units { get; set; }
            [ObservableProperty]
            public int quantity = 1;
            public double? PackageAmount { get; set; }
            public double? PricePerUnit { get; set; }
            public string? Description { get; set; }
            public string? Origin { get; set; }
            public Uri? Image { get; set; }

            public bool IsFavorite { get; set; }
            public string? Components { get; set; }
            public string? Store { get; set; }
            public bool? Availability { get; set; }

    }
    
}
