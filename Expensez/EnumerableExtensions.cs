using System.Collections.Generic;

namespace Expensez {
    public static class EnumerableExtensions {
        public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> items ) {
            foreach (var item in items)
                self.Add(item);
        }
    }
}
