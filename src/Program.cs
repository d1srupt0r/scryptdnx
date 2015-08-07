using System;
using System.Linq;
using System.Reflection;

using Scryptdnx.Utils;
using Scryptdnx.CommandLine;

[assembly: AssemblyVersionAttribute("0.0.1.*")]
namespace Scryptdnx
{
    public static class Program
    {
        private static Options po;

        private static void Main(string[] args)
        {
            try { Process(args); }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
#if DEBUG
                    Console.WriteLine(e.StackTrace);
#endif
            }
        }

        private static string[] Combine(params string[] values) =>
            values.String()
                .Split(values.Parse(Const.CommandPrefix + @"\S+"), StringSplitOptions.RemoveEmptyEntries)
                .ReplaceAll(Const.AliasPrefix, Const.GetValue)
                .ToArray();

        private static string Execute(Param option, string value)
        {
            switch (option.Type)
            {
                case ParamType.Command:
                    return option.Method(value, null);
                case ParamType.Crypto:
                    Console.Write($"{value.Limit()} {option.Cmds.Last()} key: ");
                    var key = Console.ReadLine();
                    return option.Method(value, key);
            }

            return value;
        }

        private static void ExecuteAll()
        {
            for (int i = 0; i < po.Junk.Length; i++)
            {
                foreach (var option in po.Params)
                {
                    if (option is Param)
                        po.Junk[i] = Execute(option, po.Junk[i]);
                }
            }
        }

        private static void Init(string[] args)
        {
            // ToDo:
            po = new Options {
                Params = Options.GetAll(args),
                Junk = Combine(args)
            };

            po.Verbose = po.Params.Any(o => o.Cmds.Contains("/v"));
            if (po.Verbose)
                Console.WriteLine(args.String(","));
        }

        private static void Output()
        {
            foreach (var value in po.Junk)
            {
                if (!string.IsNullOrEmpty(value))
                    Console.WriteLine(po.Verbose ? "{0} : {1}" : "{0}", value, value.Length);
            }
        }

        private static void Process(string[] args)
        {
            Init(args);
            ExecuteAll();
            Output();
        }
    }
}
