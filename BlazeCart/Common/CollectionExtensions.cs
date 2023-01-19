using System;
using System.Reflection.Emit;
using static Common.ObjectExtensions;

namespace Common
{
	public static class CollectionExtensions
	{
        // / <summary>
        // / Selects first element from grouping genereted by `commonKey`
        // / Inspired by: https://stackoverflow.com/a/2537897
        // / </summary>
        // / <param name="commonKey"></param>
        // public static IEnumerable<T> DistictBy<T, U>(
        //     this IEnumerable<T> iter,
        //     Func<T, U> commonKey
        // ) => iter
        //         .GroupBy(commonKey)
        //         .Select((i) => i.First());

        public static T FindFirstOr<T> (
            this IEnumerable<T> iter,
            Func<T, bool> pred,
            T source
        )
        {
            var filtered = iter.Where(pred);
            return filtered.Count() == 0 ? source : filtered.First(); 
        }

        /// <summary>
        /// Converts a dictionary to a list without the keys
        /// </summary>
        public static List<V> ToListOfValues<K, V>(this IDictionary<K, V> dic)
            => dic.ToList().ConvertAll((kvp) => kvp.Value);

        public static Dictionary<K, V> Clone<K, V>(this IDictionary<K,V> dic)
            where K : ICloneable
            where V : ICloneable =>
            dic.ToDictionary(
                c => (K) c.Key.Clone(), c => (V) c.Value.Clone()
            );

        public static IEnumerable<T> ConcatOrEmpty<T>(this IEnumerable<T> fst, IEnumerable<T>? snd)
            => (snd is null) ? fst : fst.Concat(snd);

        public static IEnumerable<U> TrySelect<E, T, U>(this IEnumerable<T> iter, Func<T, U> map)
            where E : Exception
        {
            var list = new List<U>();
            foreach (T el in iter) {
                try {
                    if (el is not null)
                    {
                        var mapped = map(el);
                        if (mapped is not null)
                            list.Add(mapped);
                    }
                } catch (E) { }
            }
            return list;
        } 

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? iter)
            => (iter is null) ? new List<T>() : iter!;
    }
}

