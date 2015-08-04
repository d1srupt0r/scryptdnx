using scryptdnx.Utils;

namespace scryptdnx.CommandLine
{
	public class Param
	{
		public string[] Cmds { get; set; }

		public string Help { get; set; }

		public short Order { get; set; }

		public Param(string[] cmds, string help)
		{
			Cmds = cmds;
			Help = help;
		}

		public override string ToString() =>
			$"{Cmds.String(" ").PadRight(10)}\t{Help}";
	}
}