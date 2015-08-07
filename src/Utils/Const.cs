using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Scryptdnx.Utils
{
	public static class Const
	{
		public const string AliasPrefix = @"[@#]";

		public const string Alphabet = @"abcdefghijklmnopqrstuvwxyz";

		public const string Hex = @"0123456789abcdef";

		public const string CommandPrefix = @"[-/]";

		public static List<System.Reflection.FieldInfo> GetAll =>
			typeof(Const).GetFields().ToList();

		public static string GetValue(string value)
		{
			var name = AliasPrefix.ToRegex().Replace(value, string.Empty);
			var @field = GetAll.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
			return @field != null ? @field.GetValue(null).ToString() : value;
		}
	}
}