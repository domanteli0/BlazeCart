using CommunityToolkit.Mvvm.ComponentModel;

namespace BlazeCart.Models;

public class Category : ObservableObject
{
    public string InternalID { get; }
    public string NameLT { get; set; }
    public Uri? Image { get; set; }
    public Guid Id { get; set; }
    public int Count { get; set; }
}
