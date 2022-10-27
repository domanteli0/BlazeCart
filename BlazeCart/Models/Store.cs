namespace Models
{
    public struct Store
    {
        public enum Merchendise { IKI, MAXIMA }
        public string InternalID { get; private set; }
        private string? _name;
        private string? _address;
        private string? _latitude;
        private string? _longitude;
        private Merchendise _merch;

        public Store(
            string internalID,
            Merchendise merch,
            string? name = null,
            string? address = null,
            string? latitude = null,
            string? longitude = null
        )
        {
            InternalID = internalID;
            _merch = merch;
            _name = name;
            _address = address;
            _latitude = latitude;
            _longitude = longitude;
        }
    }
}
