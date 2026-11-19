using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace Oisisi.ConsoleClient.Controllers;

static class ConsoleViewUtils
{
    public static int SafeInputInt()
    {
        int input;

        string rawInput = Console.ReadLine() ?? string.Empty;

        while (!int.TryParse(rawInput, out input))
        {
            Console.WriteLine("Nije ispravan broj, pokusajte ponovo: ");
            rawInput = Console.ReadLine() ?? string.Empty;
        }

        return input;
    }

    public static DateTime SafeInputDate()
    {
        DateTime input;

        string rawInput = Console.ReadLine() ?? string.Empty;

        while(!DateTime.TryParseExact(rawInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out input))
        {
            Console.WriteLine("Nije ispravan datum (format dd.MM.yyyy), pokusajte ponovo: ");

            rawInput = Console.ReadLine() ?? string.Empty;
        }

        return input;
    }

    public static string InputString()
    {
        return Console.ReadLine() ?? string.Empty;
    }

    public static decimal SafeInputDecimal()
    {
        while (true)
        {
            string unos = (Console.ReadLine() ?? string.Empty)
                .Trim()
                .Replace(',', '.');

            if (decimal.TryParse(
                unos,
                NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture,
                out decimal broj))
            {
                return broj;
            }

            Console.WriteLine(
                "Unesite ispravan iznos, npr. 1250,50 ili 1250.50, bez razdvajanja hiljada:");
        }
    }

}