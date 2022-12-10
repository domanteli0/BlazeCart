
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlazeCart.ViewModels;

[QueryProperty(nameof(Coordinates), "Coordinates")]
public partial class GoogleMapsViewModel : ObservableObject
{
    [ObservableProperty]
    string coordinates;

    [ObservableProperty]
    string url;

    public GoogleMapsViewModel() {
        url = ReturnFullUrl("Iki", coordinates);
    }

    private string ReturnFullUrl(String storeName, String coordinates) {
        return $"https://www.google.com/maps/dir/?api=1&origin={coordinates}&destination={storeName}+Parduotuve";
    }

}
