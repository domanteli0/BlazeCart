namespace Models
{
    // IF ANY CHANGE TO A CLASS FIELD(S) IS DONE
    // A DATABASE MIGRATION IS NECESSARY
    public class Store : Entity
    {

        public string InternalID { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public Merchendise.Merch Merch { get; set; }

        public Store() { }
    }
}
