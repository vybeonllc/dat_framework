using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Extensions
{
    public static class IEnumerableExtension
    {
        public static string ToDelimitedString<T>(this IEnumerable<T> source)
        {
            return source.ToDelimitedString(x => x.ToString(),
                CultureInfo.CurrentCulture.TextInfo.ListSeparator);
        }

        public static string ToDelimitedString<T>(
            this IEnumerable<T> source, Func<T, string> converter)
        {
            return source.ToDelimitedString(converter,
                CultureInfo.CurrentCulture.TextInfo.ListSeparator);
        }

        public static string ToDelimitedString<T>(
            this IEnumerable<T> source, string separator)
        {

            return source.ToDelimitedString(x => x.ToString(), separator);
        }

        public static string ToDelimitedString<T>(this IEnumerable<T> source,
            Func<T, string> converter, string separator)
        {
            return string.Join(separator, source.Select(converter).ToArray());
        }
        public static IEnumerable<T> Select<S, T>(this IEnumerable<S> source,
            Func<S, string, object, object> CustomValueSelector = null, params object[] constructor_parameters) where T : new()
        {
            foreach (S item in source)
            {
                T target = default(T);
                if (Reflection.MemberInfoClass.TryParse<S, T>(item, ref target, CustomValueSelector, constructor_parameters))
                    yield return target;
            }
        }
    }
}
