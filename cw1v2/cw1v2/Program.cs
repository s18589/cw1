using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cw1
{
    public class Student
    {
        public string Imie { get; set; }

        private string _nazwisko;

        public string Nazwisko
        {
            get { return _nazwisko; }
            set {_nazwisko = value; }
        }

    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length == 0) throw new ArgumentException("Parametr URL nie został podany");

            var urlRegex = new Regex("^(https?://)?[0 9a-zA-Z].[-_0-9a-zA-Z].[0-9a-zA-Z]+$");

            string url = args.Length > 0 ? args[0] : "https://www.pja.edu.pl";
            if (urlRegex.Matches(url).Count == 0) throw new ArgumentException();
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);

                var list = new List<string>();
                var zbior = new HashSet<string>();
                var slownik = new Dictionary<string, int>();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Nie udało się pobrać strony");
                    return;
                }
                else
                {
                    string html = await response.Content.ReadAsStringAsync();
                    var regex = new Regex("[a-z]+[a-z0-9]*@[a-z.]+");
                    var matches = regex.Matches(html);

                    if(matches.Count == 0)
                    {
                        Console.WriteLine("Nie znaleziono adresów email");
                    }
                    else
                    {
                        foreach(string m in matches)
                        {
                            zbior.Add(m);
                        }
                        foreach(var m in zbior)
                        {
                            Console.WriteLine(m);
                        }
                    }
                    httpClient.Dispose();
                }
            }catch(Exception exc)
            {
                //string.Format("Wystąpił błąd {0}", exc.ToString());
                Console.WriteLine($"Wystąpił błąd {exc.ToString()}");
            }

            Console.WriteLine("Koniec!");
        }
    }
}