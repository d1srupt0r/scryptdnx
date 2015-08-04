using System.Collections.Generic;

namespace scryptdnx.Utils
{
	public static class Extensions
	{
        public static string String<T>(this IEnumerable<T> values, string separator = null) =>
			string.Join(separator ?? string.Empty, values);
	}
}