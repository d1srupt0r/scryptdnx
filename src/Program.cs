using System;
using System.Linq;
using System.Reflection;

using Scryptdnx.CommandLine;
using Scryptdnx.Utils;

[assembly: AssemblyVersionAttribute("0.0.1.*"),
           CLSCompliantAttribute(true)]
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
            for (int i = 0; i < po.Junk.Count; i++)
            {
                foreach (var option in po.Params)
                {
                    po.Junk[i] = Execute(option, po.Junk[i]);
                }
            }
        }

        private static void Init(string[] args)
        {
            po = new Options(args);

            if (po.Help || !po.Params.Any())
            {
                po.List.ForEach(Console.WriteLine);
                po.Clear();
            }
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
