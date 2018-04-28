using System.Collections.Generic;

namespace Factorization.WPF.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddIfNotExists<T>(this ICollection<T> collection, T item)
        {
            if (!collection.Contains(item))
                collection.Add(item);
        }
    }
}