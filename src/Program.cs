using System;
using System.Collections.Generic;

using scryptdnx.CommandLine;

namespace scryptdnx
{
    public class Program
    {
        private static Options po = new Options();

        public static void Main(string[] args)
        {
            Init();

            Console.Write("Selection: ");
            var input = Console.ReadLine();
            Console.WriteLine(Parse(input));
        }

        private static void Init()
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine($"The current time is, {DateTime.Now}");
            po.List.ForEach(o => Console.WriteLine(o.ToString()));
        }

        private static string Parse(string input)
        {
            var result = string.Empty;

            // TODO: parse here...

            return result;
        }

        private static string GetHex(int size)
        {
            var result = new List<string>();
            var junk = "0123456789abcdef";
            var r = new Random(8048 + 3066); // same value
            for(int i = 0; i < size; i++)
            {
                result.Add(junk[r.Next(junk.Length)].ToString());
            }
            return string.Join(string.Empty, result);
        }
    }
}
