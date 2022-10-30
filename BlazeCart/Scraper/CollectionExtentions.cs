using Models;
using System.Text.RegularExpressions;

namespace Scraper
{
    static public class CollectionExtentions
    {
        /// <summary>
        /// Checks if an object with the equal property (Determined by Equals() method) exists,
        /// if not elem is added, otherthise it is not added
        /// NOTE: This method does not check if specified property is valid and exists
        /// if it does not an exception will be thrown
        /// NOTE: elem should not be null otherwise an exeption will be thrown
        /// </summary>
        public static void AddAsSetByProperty<T>(this ICollection<T> col, T elem, string property)
        {
            if (!col.Any(e => {
                return e!.EqualPropertyValue(elem!, property);
            })
            )
                col.Add(elem);
        }

        /// <summary>
        /// Finds the first element with matching property (Determined by Equals() method)
        /// and replaces it with elem.
        /// If no property mathcing element is found then it is just added to the collection.
        /// </summary>
        public static void UpdateOrAddByProperty<T>(this List<T> col, T elem, string property)
        {
            var temp = col.Find(e =>
            {
                return e!.EqualPropertyValue(elem!, property);
            });

            if (temp is not null)
            {
                col.Remove(temp);
            }

            col.Add(elem);
        }

        public static IEnumerable<Category> GetWithoutChildren(this IEnumerable<Category> categories)
        {
            foreach(var cat in categories)
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
                    str += "\t".Times(level) + cat.ToString() +"\n";
                    str += categoryTree(cat.SubCategories, level + 1);
                }

                return str;
            }
        }

        /// <summary>
        /// Returns first match based on a specified pattern.
        /// </summary>
        public static string FindFirstRegexMatch(this string str, string pattern)
        {
            return (new Regex(pattern)).Matches(str).First().ToString();
        }

        private static bool EqualPropertyValue(this object left, object right, string property)
        {
            return left.GetType().GetProperty(property)!.GetValue(left)!.Equals(
                    right.GetType().GetProperty(property)!.GetValue(right)
                );
        }
    }
}
