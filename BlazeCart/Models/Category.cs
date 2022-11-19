namespace Models
{
    public class Category : Entity
    {
        public string? InternalID { get; set; }
        public Uri? Uri { get; set; }
        public string? NameEN { get; set; }
        public string? NameLT { get; set; }
        public Category? Parent { get; set; }
        public ICollection<Item> Items { get; set; }
        public List<Category>? SubCategories { get; set; }

        public Category() {
            SubCategories = new();
        }

        //public string Tree()
        //{
        //    return tree(0);
        //}

        //private string tree(int level)
        //{
        //    var str = "\t".Times(level) + ToString() + "\n";
        //    foreach (var sub in SubCategories!)
        //        str += sub.tree(level + 1);

        //    return str;
        //}

        //public override string ToString()
        //{
        //    return "Name: '" + _nameLT + "' [" + InternalID + "] (" + Uri + ")";
        //}
    }

    public static class StringExtentions
    {
        /// <summary>
        /// Returns a string repeated itself `count` ammount of times
        /// </summary>
        /// <returns></returns>
        public static string Times(this string str, int count)
        {
            var ret = "";
            for (int i = 0; i < count; i++)
                ret += str;

            return ret;
        }
    }
}
