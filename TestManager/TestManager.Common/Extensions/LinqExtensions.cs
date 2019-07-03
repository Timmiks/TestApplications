using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Common.Extensions
{
    public static class LinqExtension
    {
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }

        public static List<T> Add<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list;
        }
    }
}
