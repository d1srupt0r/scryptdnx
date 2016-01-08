using System;
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
			,new Param(new[] { "/v", "/verbose" }, "Display [v]erbose output", ParamType.Trigger)
			,new Param(1, new[] { "/c", "/cipher" }, (x, k) => x.Cipher(k), "Run [c]ipher on text using a [k]ey (default Z:W)", ParamType.Crypto)
			,new Param(2, new[] { "/r", "/rot" }, (x, k) => x.Rot(k), "[R]otate by x places", ParamType.Crypto)
			,new Param(2, new[] { "/f", "/flip" }, (x, k) => x.Flip(), "Execute character [f]lip on text", ParamType.Command)
			,new Param(3, new[] { "/e", "/encode" }, (x, k) => x.Encode(), "Base64 [e]ncode text", ParamType.Command)
			,new Param(4, new[] { "/d", "/decode" }, (x, k) => x.Decode(), "Base64 [d]ecode text", ParamType.Command)
			,new Param(5, new[] { "/x", "/hex" }, (x, k) => x.Hex(), "He[x] encode or decode text", ParamType.Command)
			,new Param(6, new[] { "/t", "/twist" }, (x, k) => x.Twist(), "Run [t]wist on text (alternating case)", ParamType.Command)
			,new Param(7, new[] { "/g", "/generator" }, (x, k) => x.HexColor(), "Random [g]enerator", ParamType.Command)
		};

        public bool Help { get; private set; }

		public IList<string> Junk { get; private set; }

        public List<Param> List =>
            _list.OrderBy(o => o.Order).ToList();

		public IEnumerable<Param> Params { get; private set; }

		public bool Verbose { get; private set; }

        public Options(params string[] args)
        {
            Junk = args.String()
                .Split(args.Parse(Const.CommandPrefix + @"\S+"), StringSplitOptions.RemoveEmptyEntries)
                .ReplaceAll(Const.AliasPrefix, Const.GetValue)
                .ToArray();
            Params = args.ReplaceAll(@"(--|-)", "/")
                .SelectMany(value => _list.Where(param => param.Cmds.Contains(value)))
                .OrderBy(o => o.Order);
            Verbose = Params.Any(o => o.Cmds.Contains("/v"));
            Help = Params.Any(o => o.Cmds.Contains("/help"));
        }

        public void Clear()
        {
            Junk = new List<string>();
            Params = new List<Param>();
        }
	}
}