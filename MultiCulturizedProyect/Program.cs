using MultiCulturizedProyect.Properties;
using System.Globalization;
using static System.Console;

namespace MultiCulturizedProyect
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Cultura default");
            WriteWelcomes();
            WriteLine("----------------------------------------------");

            // Cualquiera de estas funciona
            //CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("en");
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en");

            Resources.Culture = CultureInfo.CreateSpecificCulture("en");
            WriteLine("Cultura en ingles");
            WriteWelcomes();
            WriteLine("----------------------------------------------");

            Resources.Culture = CultureInfo.CreateSpecificCulture("fr");
            WriteLine("Cultura en frances");
            WriteWelcomes();
            WriteLine("----------------------------------------------");
        }

        static void WriteWelcomes()
        {
            WriteLine(string.Format(Resources.Hello, "Hernán"));
            WriteLine(string.Format(Resources.Bye, "Hernán"));
        }
    }
}
