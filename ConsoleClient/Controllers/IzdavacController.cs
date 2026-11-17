using System.ComponentModel;
using System.Data;
using Oisisi.Core.Models;
using Oisisi.Core.Services;

namespace Oisisi.ConsoleClient.Controllers;

class IzdavacController
{
    private readonly IzdavacService _izdavacService;
    public IzdavacController(IzdavacService izdavacService)
    {
        _izdavacService = izdavacService;
    }

    public void RunMenu()
    {
        while (true)
        {
            ShowMenu();
            string userInput = Console.ReadLine() ?? "0";
            if (userInput == "0")
            {
                break;
            }
            HandleMenuInput(userInput); 
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("\n---- IZDAVACI ----");
        Console.WriteLine("1: Prikazi sve izdavace");
        Console.WriteLine("2: Dodaj izdavaca");
        Console.WriteLine("3: Izmeni izdavaca");
        Console.WriteLine("4: Obrisi izdavaca");
        Console.WriteLine("0: Nazad");
    }

    private void HandleMenuInput(string input)
    {
        switch(input)
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
        }
    }

    private void ShowAll()
    {
        List<Izdavac> izdavaci = _izdavacService.GetAll();
        Console.WriteLine("\nIzdavaci:");
        if(izdavaci.Count == 0)
        {
            Console.WriteLine("Nema izdavaca.");
            return;
        }

        foreach(Izdavac izd in izdavaci)
        {
            Console.WriteLine(izd);
        }
    }

    private void Add()
    {
        Izdavac izdavac = InputIzdavac();
        string? greska = _izdavacService.Add(izdavac);
        if(greska != null)
        {
            Console.WriteLine($"Greska: {greska}");
            return;
        }

        Console.WriteLine("Izdavac dodat.");
    }

    private void Update()
    {
        int id = InputId();
        Izdavac? postojeci = _izdavacService.GetById(id);
        if(postojeci == null)
        {
            Console.WriteLine("Izdavac nije pronadjen.");
            return;
        }

        Izdavac izmenjen = InputIzdavac();
        izmenjen.Id = id;

        izmenjen.SefAutorId = postojeci.SefAutorId;
        izmenjen.AutoriIds = postojeci.AutoriIds;
        izmenjen.KnjigeIds = postojeci.KnjigeIds;

        string? greska = _izdavacService.Update(izmenjen);
        if(greska != null)
        {
            Console.WriteLine($"Greska: {greska}");
            return;
        }

        Console.WriteLine("Izdavac izmenjen.");
    }

    private void Delete()
    {
        int id = InputId();
        Console.WriteLine("Da li ste sigurni? (da/ne): ");
        string odgovor = ConsoleViewUtils.InputString().Trim().ToLower();
        if(odgovor != "da")
        {
            Console.WriteLine("Brisanje otkazano.");
            return;
        }

        if(_izdavacService.Delete(id))
        {
            Console.WriteLine("Izdavac obrisan.");
        }
        else
        {
            Console.WriteLine("Izdavac nije pronadjen.");
        }
    }

    private int InputId()
    {
        Console.WriteLine("Unesite ID izdavaca: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    private Izdavac InputIzdavac()
    {
        Console.WriteLine("Unesite sifru: ");
        string sifra = ConsoleViewUtils.InputString();

        Console.WriteLine("Unesite naziv: ");
        string naziv = ConsoleViewUtils.InputString();

        return new Izdavac
        {
            Sifra = sifra,
            Naziv = naziv
        };
    }
}