using System;
using System.Linq;
using System.Collections.Generic;

using scryptdnx.Utils;
using scryptdnx.CommandLine;

namespace scryptdnx
{
    public class Program
    {
        private static Options po;

        public static void Main(string[] args)
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

        private static IList<string> Combine(params string[] values) =>
            values.String()
                .Split(values.Parse(Const.CommandPrefix + @"\S+"), StringSplitOptions.RemoveEmptyEntries)
                .ReplaceAll(Const.AliasPrefix, Const.GetValue)
                .ToList();

        private static string Execute(Param option, string value) =>
            option.Method(value, null);

        private static void ExecuteAll(IEnumerable<Param> options, IList<string> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                foreach (var option in options)
                {
                    if (option is Param)
                        values[i] = Execute(option, values[i]);
                }
            }
        }

        private static void Init()
        {
            // ToDo: do cool things like work with triggers
            po = new Options
            {
                Verbose = false
            };
        }

        private static void Output(IEnumerable<string> values)
        {
            foreach (var value in values)
            {
                if (!string.IsNullOrEmpty(value))
                    Console.WriteLine(po.Verbose ? "{0} : {1}" : "{0}", value, value.Length);
            }
        }


        private static void Process(string[] args)
        {
            Console.WriteLine(args.String(","));

            Init();
            var options = po.GetAll(args);
            var junk = Combine(args);
            ExecuteAll(options, junk);
            Output(junk);
        }
    }
}
