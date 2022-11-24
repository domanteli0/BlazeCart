namespace Models
{
    // IF ANY CHANGE TO A CLASS FIELD(S) IS DONE
    // A DATABASE MIGRATION IS NECESSARY
    public class Category : Entity
    {
        //[NotMapped]
        public string? InternalID { get; set; }
        public Uri? Uri { get; set; }
        public string? NameEN { get; set; }
        public string? NameLT { get; set; }
        public ICollection<Item> Items { get; set; }
        public List<Category> SubCategories { get; set; }
        public Merchendise.Merch Merch { get; set; }

        public Category() {
            SubCategories = new();
        }
    }
}
