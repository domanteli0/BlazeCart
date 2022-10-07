using System.Collections.ObjectModel;

namespace BlazeCart.Models
{
    internal class Cart
    {
        public int CartId { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Item> CartItems { get; set; }


    }
}
