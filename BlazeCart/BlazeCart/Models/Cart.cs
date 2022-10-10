using System.Collections.ObjectModel;

namespace BlazeCart.Models
{
    [Serializable]
    public class Cart
    {
        public int CartId { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Item> CartItems { get; set; }


    }
}
