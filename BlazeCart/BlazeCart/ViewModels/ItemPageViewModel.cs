using CommunityToolkit.Mvvm.ComponentModel;
using BlazeCart.Models;

namespace BlazeCart.ViewModels
{
    [QueryProperty(nameof(Item), "Item")]
    [QueryProperty(nameof(Name), "Name")]
    [QueryProperty(nameof(Price), "Price")]
    [QueryProperty(nameof(Image), "Image")]
    [QueryProperty(nameof(Description), "Description")]
    
    
    public partial class ItemPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Item item;
        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public double price;

        [ObservableProperty]
        public Uri image;

        [ObservableProperty]
        public string description;


       
    }
}
