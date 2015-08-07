using System;
using System.Collections.Generic;

using Scryptdnx.Utils;

namespace Scryptdnx.CommandLine
{
	public class Param
	{
		public IEnumerable<string> Cmds { get; set; }

		public string Help { get; set; }

		public Func<string, string, string> Method { get; set; }

		public short Order { get; set; }

		public ParamType Type { get; set; }

		public Param(string[] cmds, string help, ParamType type = ParamType.None)
		{
			Cmds = cmds;
			Help = help;
			Type = type;
		}

		public Param(short order, string[] cmds, Func<string, string, string> @method, string help, ParamType type)
			: this(cmds, help, type)
		{
			Order = order;
			Method = @method;
		}

		public override string ToString() =>
			$"{Cmds.String(" ").PadRight(10)}\t{Help}";
	}
}