
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlazeCart.ViewModels;

[QueryProperty(nameof(coordinates), "coordinates")]
public partial class GoogleMapsViewModel : ObservableObject
{
    [ObservableProperty]
    string coordinates;
}
