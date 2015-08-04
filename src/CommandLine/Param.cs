using System;

using scryptdnx.Utils;

namespace scryptdnx.CommandLine
{
	public class Param
	{
		public string[] Cmds { get; set; }

		public string Help { get; set; }

		public short Order { get; set; }

		public Func<string, string, string> Method { get; set; }

		public Param(string[] cmds, string help)
		{
			Cmds = cmds;
			Help = help;
		}

		public Param(string[] cmds, string help, Func<string, string, string> @method)
		{
			Cmds = cmds;
			Help = help;
			Method = @method;
		}

		public override string ToString() =>
			$"{Cmds.String(" ").PadRight(10)}\t{Help}";
	}
}