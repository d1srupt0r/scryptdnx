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
                .Default(k, "Z:W")
                .Split(':').Select(x => char.Parse(x)).Min());
            var map = Const.Alphabet.Split(swap).SelectMany(g => g.Reverse()).String() + swap.ToString();
            return value.Swap(map).String();
        }

        public static string Decode<T>(this T value)
        {
            var bytes = @"[a-zA-Z0-9+/]{4}".ToRegex().IsMatch(value.ToString())
                .Default(value.ToString(), Convert.FromBase64String);
            return bytes != null ? _defaultEncoding.GetString(bytes) : value.ToString();
        }

        public static TResult Default<TResult>(this bool condition, string value, Func<string, TResult> onTrue) =>
                condition.Default(onTrue(value), default(TResult));

        public static T Default<T>(this bool condition, T trueValue, T falseValue) =>
                condition ? trueValue : falseValue;

        public static string Encode<T>(this T value) =>
            Convert.ToBase64String(_defaultEncoding.GetBytes(value.ToString()));

        public static TResult f<T, TResult>(this T value, Func<T, TResult> selector) =>
            selector(value);

        public static string Flip(this IEnumerable<char> value) =>
            value.Reverse().String();

        public static string Hex<T>(this T value)
        {
            // ToDo: optimize this, does it really need two methods?
            var val = value.ToString();
            return val.All(x => (x >= '0' && x <= '9')
                || (x >= 'a' && x <= 'f')
                || (x >= 'A' && x <= 'F'))
                ? val.HexString().String()
                : BitConverter.ToString(_defaultEncoding.GetBytes(val)).Replace("-", string.Empty);
        }

        public static string HexColor(this string value)
        {
            var r = new Random(value.ToInt() ?? value.GetHashCode());
            return Enumerable.Repeat(0, 6)
                .Select(i => Const.Hex[r.Next(Const.Hex.Length)]).String();
        }

        public static IEnumerable<char> HexString<T>(this T value)
        {
            var val = value.ToString();
            for (int i = 0; i < val.Length; i += 2)
            {
                var hs = val.Substring(i, 2);
                yield return Convert.ToChar(Convert.ToUInt32(hs, 16));
            }
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

        public static string Rot(this string value, string size = null)
        {
            var s = size.ToInt().f(x => !x.HasValue || x > Const.Alphabet.Length ? 13 : x.Value);
            var map = string.Join("",
                Const.Alphabet.Substring(s, Const.Alphabet.Length - s),
                Const.Alphabet.Substring(0, s));
            return value.Swap(map).String();
        }

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

        public static int? ToInt(this string value) =>
            @"\d".ToRegex().IsMatch(value) ? int.Parse(value) : (int?)null;

        public static Regex ToRegex(this string value) =>
            new Regex(@value, RegexOptions.Compiled);

        public static string Twist(this IEnumerable<char> value) =>
            value.Select((c, i) => new { Value = c, Index = i })
                .Select(x => x.Index % 4 == 0
                    ? x.Value.ToggleCase()
                    : x.Value).String();
    }
}