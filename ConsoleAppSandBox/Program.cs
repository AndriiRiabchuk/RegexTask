using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleAppSandBox
{
    class Program
    {

        static string Validate(string s)
        {
            s = Regex.Replace(Regex.Replace(s, "(\".*?\")", " $1 ").Trim(),@"\s+"," ").Replace("'","''");
        
            return (s.ToCharArray().Count(c => c == '\"') % 2 == 1) ? s.Remove(s.LastIndexOf("\""), 1) : s;
        }

        static void Main(string[] args)
        {
            string s = File.ReadAllText("new1.txt");

            Console.WriteLine(Validate(s));
            return ;
        }
    }
}
