using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Scryptdnx.Utils
{
	public static class Extensions
	{
                private static Encoding _defaultEncoding = Encoding.ASCII;

                public static string Cipher(this string value, string key = null)
                {
                    var k = key == null ? string.Empty : key.ToLower();
                    var swap = char.ToLower(@"[a-z]:[a-z]".ToRegex().IsMatch(k)
                        .Default(k, "Z:W", "Cipher format 'A:Z', defaulting to Z:W")
                        .Split(':').Select(x => char.Parse(x)).Min());
                    var map = Const.Alphabet.Split(swap).SelectMany(g => g.Reverse()).String() + swap;
                    return value.Swap(map).String();
                }

                public static string Decode<T>(this T value)
                {
                    var bytes = @"[a-zA-Z0-9+/]{4}".ToRegex().IsMatch(value.ToString())
                        .Default(value.ToString(), Convert.FromBase64String);
                    return bytes != null ? _defaultEncoding.GetString(bytes) : value.ToString();
                }

                public static TResult Default<TResult>(this bool condition, string value, Func<string, TResult> onTrue) =>
                        condition ? onTrue(value) : default(TResult);

                public static T Default<T>(this bool condition, T trueValue, T falseValue, string message) =>
                        condition ? trueValue : falseValue;

                public static string Encode<T>(this T value) =>
                    Convert.ToBase64String(_defaultEncoding.GetBytes(value.ToString()));

                public static string Flip(this string value) =>
                    value.Reverse().String();

                public static string HexColor(this string value)
                {
                    int seed;
                    var r = int.TryParse(value, out seed)
                        ? new Random(seed)
                        : new Random(value.GetHashCode());
                    return string.Join(string.Empty, Enumerable.Repeat(0, 6)
                            .Select(i => Const.Hex[r.Next(Const.Hex.Length)]));
                }

                public static string Limit<T>(this T value, int size = 30)
                {
                    var x = value is string ? value as string : value.ToString();
                    return x.Length > size ? x.Substring(0, size) + "..." : x;
                }

                public static T[] Parse<T>(this T[] array, string pattern) =>
                    array.Where(x => pattern.ToRegex().IsMatch(x.ToString())).ToArray();

                public static T[] ReplaceAll<T>(this T[] array, string pattern, string replacement = "") =>
                    array.Select(x => pattern.ToRegex().Replace(x.ToString(), replacement)).Cast<T>().ToArray();

                public static T[] ReplaceAll<T>(this T[] array, string pattern, Func<T, T> action) =>
                    array.Select(x => pattern.ToRegex().IsMatch(x.ToString()) ? action(x) : x).ToArray();

                public static string String<T>(this IEnumerable<T> values, string separator = null) =>
                    string.Join(separator ?? string.Empty, values);

                public static IEnumerable<char> Swap(this string value, string key)
                {
                    foreach (var c in value)
                    {
                        var i = Const.Alphabet.IndexOf(char.ToLower(c));

                        if (char.IsLetter(c))
                            yield return char.IsUpper(c) ? char.ToUpper(key[i]) : key[i];
                        else
                            yield return c;
                    }
                }

                public static char ToggleCase(this char value) =>
                    char.IsUpper(value) ? char.ToLower(value) : char.ToUpper(value);

                public static Regex ToRegex(this string value) =>
                    new Regex(@value, RegexOptions.Compiled);

                public static string Twist(this string value) =>
                    value.Select((c, i) => new { Value = c.ToString(), Index = i })
                        .Select(x => x.Index % 4 == 0
                            ? x.Value.First().ToggleCase()
                            : x.Value.First()).String();
	}
}