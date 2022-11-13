using System.Collections.ObjectModel;
using BlazeCart.Models;

namespace BlazeCart.ViewModels
{
    public class CartUsedEventArgs : EventArgs
    {
        public ObservableCollection<Item> Items { get; }
        public CartUsedEventArgs(ObservableCollection<Item> items)
        {
            Items = items;
        }
    }
}
