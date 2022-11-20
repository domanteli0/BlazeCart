using CommunityToolkit.Mvvm.ComponentModel;

namespace BlazeCart.Models;

public class Category : ObservableObject
{
    public string InternalID { get; }
    public Uri Uri { get; set; }
    public string NameLT { get; set; }
    public List<Category>? SubCategories { get; set; }
}
