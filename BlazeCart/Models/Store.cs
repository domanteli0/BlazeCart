namespace Models
{
    public class Store : Entity
    {

        public string InternalID { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        //public Merchendise.Merch Merch { get; set; }

        public Store(
            string internalID,
            //Merchendise.Merch merch,
            string? name = null,
            string? address = null,
            string? latitude = null,
            string? longitude = null
        )
        {
            InternalID = internalID;
            //Merch = merch;
            Name = name;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
