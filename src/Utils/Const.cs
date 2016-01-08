using System.Linq;
using System.Reflection;

namespace Scryptdnx.Utils
{
	public static class Const
	{
		public static readonly string AliasPrefix = @"[@#]";

		public static readonly string Alphabet = @"abcdefghijklmnopqrstuvwxyz";

		public static readonly string Hex = @"0123456789abcdef";

		public static readonly string CommandPrefix = @"[-/]";

		public static string GetValue(string value)
		{
			var name = AliasPrefix.ToRegex().Replace(value, string.Empty);
			var @field = typeof(Const).GetFields()
                .FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
			return @field != null ? @field.GetValue(null).ToString() : value;
		}
	}
}