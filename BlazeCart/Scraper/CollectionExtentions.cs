using System;
namespace Scraper
{
    static public class CollectionExtentions
    {
        /// <summary>
        /// Checks if an object with the equal property (Determined by Equals() method) exists,
        /// if not elem is added, otherthise it is not
        /// NOTE: This method does not check if specified property is valid and exists
        /// if it does not an exception will be thrown
        /// NOTE: elem should not be null otherwise an exeption will be thrown
        /// </summary>
        public static void AddAsSetByProperty<T>(this ICollection<T> col, T elem, string property)
        {
            if (!col.Any(e => {
                return e.GetType().GetProperty(property)!.GetValue(e)!.Equals(
                    elem.GetType().GetProperty(property)!.GetValue(elem)
                    );
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
                return e.GetType().GetProperty(property)!.GetValue(e)!.Equals(
                    elem.GetType().GetProperty(property)!.GetValue(elem));
            });

            if (temp is not null)
            {
                col.Remove(temp);
            }

            col.Add(elem);
        }
    }
}
