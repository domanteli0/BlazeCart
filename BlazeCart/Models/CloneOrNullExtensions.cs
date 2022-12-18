using System;
using System.Net.Http;

namespace Models
{
	public static class CloneOrNullExtensions
	{
        public static Uri CloneOrNull(this Uri obj) =>
            (obj is null)
            ? obj
            : (Uri) new Uri(obj.ToString());

        public static T CloneOrNull<T>(this T obj)
            where T : ICloneable
                =>
                    (obj is null)
                    ? obj
                    : (T)obj.Clone();
    }
}

