using System;
namespace Common
{
    public static class ObjectExtensions
    {
        public static bool EqualPropertyValue(this object left, object right, string property)
        {
            return left.GetType().GetProperty(property)!.GetValue(left)!.Equals(
                    right.GetType().GetProperty(property)!.GetValue(right)
                );
        }

        public static void DoOrNothing<T>(this T obj, Action<T> doIfNotNull)
        {
            if (obj is not null) { doIfNotNull(obj); };
        }
    }
}

