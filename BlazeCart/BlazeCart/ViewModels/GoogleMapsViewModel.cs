
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlazeCart.ViewModels;

[QueryProperty("Coordinates", "Coordinates")]
public partial class GoogleMapsViewModel : ObservableObject
{
    [ObservableProperty]
    string coordinates;
}
