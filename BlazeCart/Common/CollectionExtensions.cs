using System;
using Models;
using static Common.ObjectExtensions;
namespace Common
{
	public static class CollectionExtensions
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

        // Iterate on collections with collections

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
        {
            return tree(category, 0);
        }

        private static string tree(Category category,int level)
        {
            var str = "\t".Times(level) + category.ToString() + "\n";
            foreach (var sub in category.SubCategories)
                str += tree(sub, level + 1);

            return str;
        }

        /// <summary>
        /// Converts a dictionary to a list without the keys
        /// </summary>
        public static List<V> ToListOfValues<K, V>(this IDictionary<K, V> dic)
        {
            return dic.ToList().ConvertAll((kvp) => kvp.Value);

        }

        public static Dictionary<K, V> Clone<K, V>(this IDictionary<K,V> dic)
            where K : ICloneable
            where V : ICloneable =>
            dic.ToDictionary(
                c => (K) c.Key.Clone(), c => (V) c.Value.Clone()
            );
    }
}

