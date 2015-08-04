using System;
using System.Collections.Generic;
//using System.Linq;

using ConsoleApplication.Enums;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Init();

            Console.Write("Selection: ");
            var input = Console.ReadLine();
            Console.WriteLine(ParseInput(input));
        }

        private static void Init()
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine($"The current time is, {DateTime.Now}");
            Console.WriteLine(string.Join("\t", GetNames<MenuItem>()));
        }

        private static string ParseInput(string input)
        {
            var result = string.Empty;

            MenuItem item;
            if (Enum.TryParse(input, true, out item))
            {
                switch(item)
                {
                    case MenuItem.Generate:
                        Console.WriteLine(string.Join("\t", GetNames<GenerateItem>()));
                        result = $"{GetHex(6)} - {char.GetNumericValue('a')}";
                        break;
                }
            }

            return result;
        }

        private static IEnumerable<string> GetNames<T>()
        {
            var names = Enum.GetNames(typeof(T));
            for (int i = 0; i < names.Length; i++)
            {
                yield return $"{i} {names[i]}";
            }
        }

        private static string GetHex(int size)
        {
            var result = new List<string>();
            var junk = "0123456789abcdef";
            var r = new Random(8048 + 3066); // same color
            for(int i = 0; i < size; i++)
            {
                result.Add(junk[r.Next(junk.Length)].ToString());
            }
            return string.Join(string.Empty, result);
        }
    }
}
