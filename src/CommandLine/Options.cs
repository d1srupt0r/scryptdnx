using System.Collections.Generic;

namespace scryptdnx.CommandLine
{
	public class Options
	{
		public List<Param> List = new List<Param>
		{
			new Param(new [] { "/help" }, "Display [h]elp")
			,new Param(new[] { "/g", "/generate" }, "Random [g]enerator")
		};
	}
}