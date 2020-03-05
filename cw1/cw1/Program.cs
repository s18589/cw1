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

            string url = args.Length > 0 ? args[0] : "https://www.pja.edu.pl";
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);

                var list = new List<string>();
                var zbior = new HashSet<string>();
                var slownik = new Dictionary<string, int>();

                if (!response.IsSuccessStatusCode) return;
                
                string html = await response.Content.ReadAsStringAsync();
                var regex = new Regex("[a-z]+[a-z0-9]*@[a-z.]+");
                var matches = regex.Matches(html);

                    foreach (var m in matches)
                    {
                        Console.WriteLine(m);
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
