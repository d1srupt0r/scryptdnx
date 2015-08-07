using System.Linq;
using System.Collections.Generic;

using Scryptdnx.Utils;

namespace Scryptdnx.CommandLine
{
	public class Options
	{
		private static List<Param> _list { get; } = new List<Param>
		{
			new Param(new[] { "/help" }, "Display [h]elp")
			,new Param(new[] { "/v", "/verbose" }, "Display [v]erbose output", Enums.ParamType.Trigger)
			,new Param(1, new[] { "/c", "/cipher" }, (x, k) => x.Cipher(k), "Run [c]ipher on text using a [k]ey (default Z:W)", Enums.ParamType.Crypto)
			,new Param(2, new[] { "/f", "/flip" }, (x, k) => x.Flip(), "Execute character [f]lip on text", Enums.ParamType.Command)
			,new Param(3, new[] { "/e", "/encode" }, (x, k) => x.Encode(), "Base64 [e]ncode text", Enums.ParamType.Command)
			,new Param(4, new[] { "/d", "/decode" }, (x, k) => x.Decode(), "Base64 [d]ecode text", Enums.ParamType.Command)
			,new Param(5, new[] { "/t", "/twist" }, (x, k) => x.Twist(), "Run [t]wist on text (alternating case)", Enums.ParamType.Command)
			,new Param(6, new[] { "/g", "/generator" }, (x, k) => x.HexColor(), "Random [g]enerator", Enums.ParamType.Command)
		};

		public IList<string> Junk { get; set; }

		public IEnumerable<Param> Params { get; set; }

		public bool Verbose { get; set; }

        public static IEnumerable<Param> GetAll(params string[] args) =>
			args.ReplaceAll(@"(--|-)", "/")
                .SelectMany(value => _list.Where(param => param.Cmds.Contains(value)))
                .OrderBy(o => o.Order);
	}
}