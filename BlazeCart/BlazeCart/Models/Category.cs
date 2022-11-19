
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlazeCart.Models;

public class Category : ObservableObject
{
    public string InternalID { get;}
    public Uri Uri { get; }
    public string NameLT { get;}
}

