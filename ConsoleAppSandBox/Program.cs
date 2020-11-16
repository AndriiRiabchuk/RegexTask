using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleAppSandBox
{
    class Program
    {
        static void Main(string[] args)
        {

            string s = File.ReadAllText("new1.txt");
            Console.WriteLine(s);


            var regex = new Regex("\"\\w*\"");
            foreach (Match match in regex.Matches(s))
                s = s.Replace(match.Value,match.Value.Substring(1,match.Value.Length-2));

            regex = new Regex("\"(.*?)\"");

            foreach (Match match in regex.Matches(s))
            {
                Console.WriteLine(match.Value);
                int m = match.Value.Length;

                for (int i = 0; i < s.Length; i++)
                {
                    if (s.Length - i  < m)
                    {
                        break;
                    }

                    string buf = s.Substring(i, m);

                    if (buf == match.Value)
                    {
                        if (i + m < s.Length && s[i + m] != ' ')
                        {
                            s = s.Substring(0, i + m) + " " + s.Substring(i + m);
                        }
                        if (i-1>=0 &&s[i - 1] != ' ')
                        {
                            s = s.Substring(0, i) + " " + s.Substring(i);
                            i++;
                        }
                    }
                }
            }

            if (s.ToCharArray().Count(c => c == '\"') % 2 == 1)
            {

                int l = s.Length;
                for (int i = l - 1; i >= 0; i--)
                {
                    if (s[i] == '\"')
                    {
                        s = s.Substring(0, i - 1) + "\\" + s.Substring(i, l - i );
                        break;
                    }
                }
            }

            s = s.Replace("'", "''");

            string con = "data source = PC0007;Initial Catalog=lab; User Id = user; Password = 123";
            var sqlcon = new SqlConnection(con);

            sqlcon.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = $"insert into String values('{@s}')";
            command.Connection = sqlcon;

            var cd = command.ExecuteReader();


        }
    }
}
