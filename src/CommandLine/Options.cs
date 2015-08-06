using System.Linq;
using System.Collections.Generic;

using scryptdnx.Utils;

namespace scryptdnx.CommandLine
{
	public class Options
	{
		public bool Verbose { get; set; }

		public List<Param> List = new List<Param>
		{
			new Param(new [] { "/help" }, "Display [h]elp")
			,new Param(new[] { "/e", "/encode" }, "Base64 [e]ncode text", (x, k) => x.Encode())
			,new Param(new[] { "/d", "/decode" }, "Base64 [d]ecode text", (x, k) => x.Decode())
			,new Param(new[] { "/g", "/generator" }, "Random [g]enerator", (x, k) => x.HexColor())
		};

		/* ToDo: Possibly store this in a local variable...
	    		 call it 'parse_command_line' or something */
        public IEnumerable<Param> GetAll(params string[] args) =>
			args.ReplaceAll(@"(--|-)", "/")
                .SelectMany(value => List.Where(param => param.Cmds.Contains(value)))
                .OrderBy(o => o.Order);
	}
}