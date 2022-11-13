using System.Collections.ObjectModel;
using BlazeCart.Models;

namespace BlazeCart.ViewModels
{
    public class CartUsedEventArgs : EventArgs
    {
        public ObservableCollection<Item> _items { get; }
        public CartUsedEventArgs(ObservableCollection<Item> items)
        {
            _items = items;
        }
    }
}
