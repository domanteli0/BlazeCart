using System;
namespace Scraper
{
    static public class AddAsSetToICollection
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
            if (!col.Any(l => {
                return l.GetType().GetProperty(property)!.GetValue(l)!.Equals(
                    elem.GetType().GetProperty(property)!.GetValue(elem)
                    );
            })
            )
                col.Add(elem);
        }
    }
}

