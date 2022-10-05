

using CommunityToolkit.Mvvm.ComponentModel;

namespace BlazeCart.ViewModels;



public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    public string name;

    [ObservableProperty]
    public bool isBusy;

    
}
