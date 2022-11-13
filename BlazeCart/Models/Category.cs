namespace Models
{
    public class Category : Entity
    {
        public string? InternalID { get; set; }
        public Uri? Uri { get; set; }
        private string? _nameEN;
        private string? _nameLT;
        private Category? _parent;
        public List<Category> SubCategories { set; get; }

        public Category(string? internalID = null, string? nameLT = null, string? nameEN = null, Category? parent = null, Uri? uri = null)
        {
            SubCategories = new List<Category>();
            InternalID = internalID;
            _nameLT = nameLT;
            _nameEN = nameEN;
            _parent = parent;
            Uri = uri;
        }

        public string Tree()
        {
            return tree(0);
        }

        private string tree(int level)
        {
            var str = "\t".Times(level) + ToString() + "\n";
            foreach (var sub in SubCategories!)
                str += sub.tree(level + 1);

            return str;
        }

        public override string ToString()
        {
            return "Name: '" + _nameLT + "' [" + InternalID + "] (" + Uri + ")";
        }
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
