using System.Text;
using Oisisi.ConsoleClient.Controllers;
using Oisisi.Core.DAO;
using Oisisi.Core.Services;

namespace Oisisi.ConsoleClient;

class Program
{
    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        PosetilacDao posetilacDao = new PosetilacDao();
        KupovinaDao kupovinaDao = new KupovinaDao();
        IzdavacDao izdavacDao = new IzdavacDao();

        PosetilacService posetilacService = new PosetilacService(posetilacDao, kupovinaDao);
        KupovinaService kupovinaService = new KupovinaService(kupovinaDao, posetilacDao);
        IzdavacService izdavacService = new IzdavacService(izdavacDao);

        PosetilacController posetilacController = new PosetilacController(posetilacService);
        KupovinaController kupovinaController = new KupovinaController(kupovinaService);
        IzdavacController izdavacController = new IzdavacController(izdavacService);

        AdresaDao adresaDao = new AdresaDao();
        AdresaService adresaService = new AdresaService(adresaDao);
        AdresaController adresaController = new AdresaController(adresaService);

        AutorDao autorDao = new AutorDao();
        AutorService autorService = new AutorService(autorDao, adresaDao, izdavacDao);
        AutorController autorController = new AutorController(
            autorService, adresaService);

        RunMainMenu(posetilacController, kupovinaController, izdavacController, adresaController, autorController);
    }

    static void RunMainMenu(PosetilacController posetilacController, KupovinaController kupovinaController, IzdavacController izdavacController, AdresaController adresaController,AutorController autorController)
    {
        while(true)
        {
            Console.WriteLine("\n----- SAJAM KNJIGA -----");
            Console.WriteLine("1: Posetioci");
            Console.WriteLine("2: Kupovine");
            Console.WriteLine("3: Izdavaci");
            Console.WriteLine("4: Adrese");
            Console.WriteLine("5: Autori");
            Console.WriteLine("0: Izlaz");

            string userInput = Console.ReadLine() ?? "0";
            if(userInput == "0")
            {
                break;
            }

            switch (userInput)
            {
                case "1":
                    posetilacController.RunMenu();
                    break;
                case "2":
                    kupovinaController.RunMenu();
                    break;
                case "3":
                    izdavacController.RunMenu();
                    break;
                case "4":
                    adresaController.RunMenu();
                    break;
                case "5":
                    autorController.RunMenu();
                    break;
            }
        }
    }
}
