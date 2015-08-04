using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace scryptdnx.Utils
{
	public static class Extensions
	{
                public static string HexColor(this string value)
                {
                    int seed;
                    var r = int.TryParse(value, out seed)
                        ? new Random(seed)
                        : new Random();
                    return string.Join(string.Empty, Enumerable.Repeat(0, 6)
                            .Select(i => Const.Hex[r.Next(Const.Hex.Length)]));
                }

                public static T[] Parse<T>(this T[] array, string pattern) =>
                        array.Where(x => pattern.ToRegex().IsMatch(x.ToString())).ToArray();

                public static T[] ReplaceAll<T>(this T[] array, string pattern, string replacement = "") =>
                        array.Select(x => pattern.ToRegex().Replace(x.ToString(), replacement)).Cast<T>().ToArray();

                public static T[] ReplaceAll<T>(this T[] array, string pattern, Func<T, T> action) =>
                        array.Select(x => pattern.ToRegex().IsMatch(x.ToString()) ? action(x) : x).ToArray();

                public static string String<T>(this IEnumerable<T> values, string separator = null) =>
                        string.Join(separator ?? string.Empty, values);

                public static Regex ToRegex(this string value) =>
                        new Regex(@value, RegexOptions.Compiled);
	}
}