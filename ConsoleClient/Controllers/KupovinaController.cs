using System.ComponentModel;
using System.Data;
using Oisisi.Core.Models;
using Oisisi.Core.Services;

namespace Oisisi.ConsoleClient.Controllers;

class KupovinaController
{
    private readonly KupovinaService _kupovinaService;

    public KupovinaController(KupovinaService kupovinaService)
    {
        _kupovinaService = kupovinaService;
    }

    public void RunMenu()
    {
        while(true)
        {
            ShowMenu();
            string userInput = Console.ReadLine() ?? "0";
            if (userInput == "0")
                break;
            HandleMenuInput(userInput);
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("\n----- Kupovine -----");
        Console.WriteLine("1: Prikazi sve kupovine");
        Console.WriteLine("2: Dodaj kupovinu");
        Console.WriteLine("3: Izmeni kupovinu");
        Console.WriteLine("4: Obrisi kupovinu");
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
        List<Kupovina> kupovine = _kupovinaService.GetAll();
        Console.WriteLine("\nKupovine: ");
        if(kupovine.Count == 0)
        {
            Console.WriteLine("Nema kupovina.");
            return;
        }

        foreach(Kupovina kup in kupovine)
        {
            Console.WriteLine(kup);
        }
    }

    private void Add()
    {
        Kupovina kupovina = InputKupovina();
        string? greska = _kupovinaService.Add(kupovina);
        if(greska != null)
        {
            Console.WriteLine($"Greska: {greska}");
            return;
        }

        Console.WriteLine("Kupovina dodata.");
    }

    private void Update()
    {
        int id = InputId();
        if(_kupovinaService.GetById(id) == null)
        {
            Console.WriteLine("Kupovina nije pronadjena.");
            return;
        }

        Kupovina izmenjena = InputKupovina();
        izmenjena.Id = id;

        string? greska = _kupovinaService.Update(izmenjena);
        if(greska != null)
        {
            Console.WriteLine($"Greska: {greska}");
            return;
        }

        Console.WriteLine("Kupovina izmenjena.");
    }

    private void Delete()
    {
        int id = InputId();
        Console.WriteLine("Da li ste sigurni? (da/ne): ");
        string odgovor = ConsoleViewUtils.InputString().Trim().ToLower();
        if( odgovor != "da")
        {
            Console.WriteLine("Brisanje otkazano.");
            return;
        }

        if(_kupovinaService.Delete(id))
        {
            Console.WriteLine("Kupovina obrisana.");
        }
        else
        {
            Console.WriteLine("Kupovina nije pronadjena.");
        }
    }

    private int InputId()
    {
        Console.WriteLine("Unesite ID kupovine: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    private Kupovina InputKupovina()
    {
        Console.WriteLine("Unesite ID posetilac: ");
        int posetilacId = ConsoleViewUtils.SafeInputInt();

        Console.WriteLine("Unesite ID knjige: ");
        int knjigaId = ConsoleViewUtils.SafeInputInt();

        Console.WriteLine("Unesite datum kupovine (dd.MM.yyyy): ");
        DateTime datum = ConsoleViewUtils.SafeInputDate();

        Console.WriteLine("Unesite ocenu (1-5): ");
        int ocena = ConsoleViewUtils.SafeInputInt();

        Console.WriteLine("Unesite komentar: ");
        string komentar = ConsoleViewUtils.InputString();

        return new Kupovina
        {
            PosetilacId = posetilacId,
            KnjigaId = knjigaId,
            DatumKupovine = datum,
            Ocena = ocena,
            Komentar = komentar
        };
    }
}