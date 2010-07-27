using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action(item);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            return collection.Count() == 0;
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> collection)
        {
            return !collection.IsEmpty();
        }
    }
}
