using Oisisi.Core.Models;
using Oisisi.Core.Services;

namespace Oisisi.ConsoleClient.Controllers;


class AdresaController
{
    private readonly AdresaService _adresaService;

    public AdresaController(AdresaService adresaService)
    {
        _adresaService = adresaService;
    }

    public void RunMenu()
    {
        while (true)
        {
            Console.WriteLine("\n----- ADRESE -----");
            Console.WriteLine("1: Prikazi sve adrese");
            Console.WriteLine("2: Dodaj adresu");
            Console.WriteLine("3: Izmeni adresu");
            Console.WriteLine("4: Obrisi adresu");
            Console.WriteLine("0: Nazad");

            string unos = Console.ReadLine() ?? "0";

            switch (unos)
            {
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
                case "0":
                    return;
                default:
                    Console.WriteLine("Nepostojeca opcija.");
                    break;
            }
        }
    }

    private void ShowAll()
    {
        List<Adresa> adrese = _adresaService.GetAll();

        if (adrese.Count == 0)
        {
            Console.WriteLine("Nema adresa.");
            return;
        }

        foreach (Adresa adresa in adrese)
        {
            Console.WriteLine(adresa);
        }
    }

    private void Add()
    {
        Adresa adresa = InputAdresa();
        string? greska = _adresaService.Add(adresa);

        if (greska != null)
        {
            Console.WriteLine($"Greska: {greska}");
            return;
        }

        Console.WriteLine($"Adresa dodata. ID: {adresa.Id}");
    }

    private void Update()
    {
        int id = InputId();

        if (_adresaService.GetById(id) == null)
        {
            Console.WriteLine("Adresa nije pronadjena.");
            return;
        }

        Adresa izmenjena = InputAdresa();
        izmenjena.Id = id;

        string? greska = _adresaService.Update(izmenjena);

        if (greska != null)
        {
            Console.WriteLine($"Greska: {greska}");
            return;
        }

        Console.WriteLine("Adresa izmenjena.");
    }

    private void Delete()
    {
        int id = InputId();

        if (_adresaService.GetById(id) == null)
        {
            Console.WriteLine("Adresa nije pronadjena.");
            return;
        }

        Console.WriteLine("Da li ste sigurni? (da/ne):");
        string odgovor = ConsoleViewUtils.InputString().Trim().ToLower();

        if (odgovor != "da")
        {
            Console.WriteLine("Brisanje otkazano.");
            return;
        }

        if (_adresaService.Delete(id))
        {
            Console.WriteLine("Adresa obrisana.");
        }
        else
        {
            Console.WriteLine("Adresa nije pronadjena.");
        }
    }

    private int InputId()
    {
        Console.WriteLine("Unesite ID adrese:");
        return ConsoleViewUtils.SafeInputInt();
    }

    private Adresa InputAdresa()
    {
        Console.WriteLine("Unesite ulicu:");
        string ulica = ConsoleViewUtils.InputString();

        Console.WriteLine("Unesite broj:");
        string broj = ConsoleViewUtils.InputString();

        Console.WriteLine("Unesite grad:");
        string grad = ConsoleViewUtils.InputString();

        Console.WriteLine("Unesite drzavu:");
        string drzava = ConsoleViewUtils.InputString();

        return new Adresa(ulica, broj, grad, drzava);
    }
}