using System.Linq;
//using Common;

namespace Models
{
    // IF ANY CHANGE TO A CLASS FIELD(S) IS DONE
    // A DATABASE MIGRATION IS NECESSARY
    public class Category : Entity, ICloneable
    {
        //[NotMapped]
        public string? InternalID { get; set; }
        public Uri? Uri { get; set; }
        public string? NameEN { get; set; }
        public string? NameLT { get; set; }
        public List<Item> Items { get; set; }
        public List<Category> SubCategories { get; set; }
        public Merchendise.Merch Merch { get; set; }

        public Category() {
            SubCategories = new List<Category>();
            Items = new List<Item>();
        }

        public override string ToString()
        {
            var id = (InternalID is null) ? "ID: null" : "ID: '" + InternalID + "'";
            var str = (NameLT is null) ? "null" : NameLT;
            var count = (Items is null) ? "null" : Items.Count.ToString();
            return id + ", " + str + "' [" + count + "] ";
        }

        public object Clone()
        {
            return new Category()
            {
                InternalID = this.InternalID.CloneOrNull(),
                Uri = this.Uri.CloneOrNull(),
                NameEN = this.NameEN.CloneOrNull(),
                NameLT = this.NameLT.CloneOrNull(),
                Items = this.Items.ToList().ConvertAll(i => (Item) i.Clone()),
                SubCategories = SubCategories.ConvertAll(i => (Category) i.Clone()),
                Merch = this.Merch,
            };
        }
    }
}
