namespace BlazeCart.Models
{
    internal class Cart
    {
        public int CartId { get; set; }
        public string Name { get; set; }
        public List<Item> CartItems { get; set; }


    }
}
