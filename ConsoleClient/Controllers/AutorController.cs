using Microsoft.VisualBasic.FileIO;
using Oisisi.Core.Models;
using Oisisi.Core.Services;

namespace Oisisi.ConsoleClient.Controllers;

class AutorController
{
    private readonly AutorService _autorService;
    private readonly AdresaService _adresaService;

    public AutorController(
        AutorService autorService,
        AdresaService adresaService)
    {
        _autorService = autorService;
        _adresaService = adresaService;
    }

    public void RunMenu()
    {
        while (true)
        {
            Console.WriteLine("\n------ AUTORI ------");
            Console.WriteLine("1: Prikazi sve autore");
            Console.WriteLine("2: Dodaj autora");
            Console.WriteLine("3: Izmeni autora");
            Console.WriteLine("4: Obrisi autora");
            Console.WriteLine("0: Nazad");

            string unos = Console.ReadLine() ?? "0";

            switch (unos)
            {
                case "0":
                    return;
                case "1":
                    ShowAll();
                    break;
                case "2":
                    Add();
                    break;
                case "3":
                    Update();
                    break;
                case "4":
                    Delete();
                    break;
                default:
                    Console.WriteLine("Nepostojeca opcija.");
                    break;
            }
        }
    }

    private void ShowAll()
    {
        List<Autor> autori = _autorService.GetAll();

        if (autori.Count == 0)
        {
            Console.WriteLine("Nema autora.");
            return;
        }

        foreach (Autor autor in autori)
        {
            Console.WriteLine(autor);
        }
    }

    private void Add()
    {
        Autor? autor = InputAutor();

        if (autor == null)
        {
            return;
        }

        string? greska = _autorService.Add(autor);

        Console.WriteLine(greska == null
            ? "Autor dodat."
            : $"Greska: {greska}");
    }

    private void Update()
    {
        Console.WriteLine("Unesite ID autora:");
        int id = ConsoleViewUtils.SafeInputInt();

        Autor? postojeci = _autorService.GetById(id);

        if (postojeci == null)
        {
            Console.WriteLine("Autor nije pronadjen.");
            return;
        }

        Console.WriteLine($"Trenutni podaci: {postojeci}");
        Console.WriteLine("Unesite podatke za izmenu:");

        Autor? izmenjen = InputAutor();

        if (izmenjen == null)
        {
            return;
        }

        izmenjen.Id = postojeci.Id;
        izmenjen.KnjigeIds = new List<int>(postojeci.KnjigeIds);

        string? greska = _autorService.Update(izmenjen);

        Console.WriteLine(greska == null
            ? "Autor izmenjen."
            : $"Greska: {greska}");
    }


    private void Delete()
    {
        Console.WriteLine("Unesite ID autora:");
        int id = ConsoleViewUtils.SafeInputInt();

        Console.WriteLine("Da li ste sigurni? (da/ne):");
        string odgovor = ConsoleViewUtils.InputString().Trim();

        if (!odgovor.Equals("da", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Brisanje otkazano.");
            return;
        }

        string? greska = _autorService.Delete(id);

        Console.WriteLine(greska == null
            ? "Autor obrisan."
            : $"Greska: {greska}");
    }


    private Autor? InputAutor()
    {
        List<Adresa> adrese = _adresaService.GetAll();

        if (adrese.Count == 0)
        {
            Console.WriteLine("Prvo dodajte adresu kroz meni Adrese.");
            return null;
        }

        Console.WriteLine("Dostupne adrese:");

        foreach (Adresa adresa in adrese)
        {
            Console.WriteLine(adresa);
        }

        Autor autor = new Autor();

        Console.WriteLine("Unesite ime:");
        autor.Ime = ConsoleViewUtils.InputString().Trim();

        Console.WriteLine("Unesite prezime:");
        autor.Prezime = ConsoleViewUtils.InputString().Trim();

        Console.WriteLine("Unesite datum rodjenja (dd.MM.yyyy):");
        autor.DatumRodjenja = ConsoleViewUtils.SafeInputDate();

        Console.WriteLine("Unesite ID adrese:");
        autor.AdresaId = ConsoleViewUtils.SafeInputInt();

        Console.WriteLine("Unesite kontakt telefon:");
        autor.KontaktTelefon = ConsoleViewUtils.InputString().Trim();

        Console.WriteLine("Unesite email:");
        autor.Email = ConsoleViewUtils.InputString().Trim();

        Console.WriteLine("Unesite broj licne karte:");
        autor.BrojLicneKarte = ConsoleViewUtils.InputString().Trim();

        Console.WriteLine("Unesite godine iskustva:");
        autor.GodineIskustva = ConsoleViewUtils.SafeInputInt();

        return autor;
    }
}