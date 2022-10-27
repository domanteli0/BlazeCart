namespace Models
{
    public class Category
    {
        public string? InternalID { private set; get; }
        private string? _nameEN;
        private string? _nameLT;
        private Category? _parent;
        public List<Category> SubCategories { set; get; }

        public Category(string? internalID = null, string? nameLT = null, string? nameEN = null, Category? parent = null)
        {
            SubCategories = new List<Category>();
            InternalID = internalID;
            _nameLT = nameLT;
            _nameEN = nameEN;
            _parent = parent;
        }

        public string CategoryTree()
        {
            return categoryTree();
        }

        private string categoryTree(int level = 0)
        {
            var str = "\t".Times(level) + "Name: " + _nameLT + " [" + InternalID + "]\n";
            foreach (var sub in SubCategories!)
                str += sub.categoryTree(level + 1);

            return str;
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
