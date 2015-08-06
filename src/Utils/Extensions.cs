using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace scryptdnx.Utils
{
	public static class Extensions
	{
                private static Encoding _defaultEncoding = Encoding.ASCII;

                public static string Decode<T>(this T value)
                {
                    var bytes = @"[a-zA-Z0-9+/]{4}".ToRegex().IsMatch(value.ToString())
                        .Default(value.ToString(), Convert.FromBase64String, "Invalid Base64 string");
                    return bytes != null ? _defaultEncoding.GetString(bytes) : value.ToString();
                }

                public static TResult Default<TResult>(this bool condition, string value, Func<string, TResult> onTrue, string message)
                {
                    if (condition)
                        return onTrue(value);
                    else
                    {
                        Console.WriteLine(message);
                        return default(TResult);
                    }
                }

                public static string Encode<T>(this T value) =>
                        Convert.ToBase64String(_defaultEncoding.GetBytes(value.ToString()));

                public static string HexColor(this string value)
                {
                    int seed;
                    var r = int.TryParse(value, out seed)
                        ? new Random(seed)
                        : new Random(value.GetHashCode());
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