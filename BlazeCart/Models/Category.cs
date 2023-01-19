using System.Linq;
// using static Common.StringExtensions;
using Common;

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

    public static class IEnumerableCategoryExtensions {
        public static void ForEachR(this IEnumerable<Category> list, Action<Category> act)
        {
            foreach (var sub in list)
            {
                sub.SubCategories.ForEach(act);
                act(sub);
            }
        }

        public static IEnumerable<Category> GetWithoutChildren(this IEnumerable<Category> categories)
        {
            foreach (var cat in categories)
            {
                if (cat.SubCategories.Count() > 0)
                    foreach (var child in cat.SubCategories.GetWithoutChildren())
                    {
                        yield return child;
                    }
                else
                {
                    yield return cat;
                }
            }

        }

        public static string Tree(this IEnumerable<Category> categories)
        {
            return (categoryTree(categories, 0));

            string categoryTree(IEnumerable<Category> categories, int level)
            {
                var str = "";
                foreach (var cat in categories!)
                {
                    str += "\t".Times(level) + cat.ToString() + "\n";
                    str += categoryTree(cat.SubCategories, level + 1);
                }

                return str;
            }
        }

        public static string Tree(this Category category) 
            => tree(category, 0);

        private static string tree(Category category,int level)
        {
            var str = "\t".Times(level) + category.ToString() + "\n";
            foreach (var sub in category.SubCategories)
                str += tree(sub, level + 1);

            return str;
        }
    }
}
