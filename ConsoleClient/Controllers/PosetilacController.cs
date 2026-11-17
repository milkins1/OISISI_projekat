using Oisisi.Core.Models;
using Oisisi.Core.Services;

namespace Oisisi.ConsoleClient.Controllers;

class PosetilacController
{
    private readonly PosetilacService _posetilacService;

    public PosetilacController(PosetilacService posetilacService)
    {
        _posetilacService = posetilacService;
    }

    public void RunMenu()
    {
        while (true)
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
        Console.WriteLine("\n------ POSETIOCI ------");
        Console.WriteLine("1: Prikazi sve posetioce");
        Console.WriteLine("2: Dodaj posetioca");
        Console.WriteLine("3: Izmijeni posetioca");
        Console.WriteLine("4: Obrisi posetioca");
        Console.WriteLine("0: Nazad");
    }

    private void HandleMenuInput(string input)
    {
        switch (input)
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
        List<Posetilac> posetioci = _posetilacService.GetAll();
        Console.WriteLine("\nPosetioci:");
        if (posetioci.Count == 0)
        {
            Console.WriteLine("Nema posetilaca.");
            return;
        }

        foreach (Posetilac p in posetioci)
        {
            double prosek = _posetilacService.IzracunajProsecnuOcenu(p.Id);
            Console.WriteLine($"{p} | Prosecna ocena: {prosek: 0.00}");
        }

    }

    private void Add()
    {
        Posetilac posetilac = InputPosetilac();
        string? greska = _posetilacService.Add(posetilac);
        if (greska != null)
        {
            Console.WriteLine($"Greska: {greska}");
            return;
        }
        Console.WriteLine("Posetilac dodat.");
    }

    private void Update()
    {
        int id = InputId();
        Posetilac? postojeci = _posetilacService.GetById(id);
        if(postojeci == null)
        {
            Console.WriteLine("Posetilac nije pronadjen.");
            return;
        }

        Posetilac izmenjen = InputPosetilac();
        izmenjen.Id = id;

        izmenjen.ListaZeljaKnjigaIds = postojeci.ListaZeljaKnjigaIds;

        string? greska = _posetilacService.Update(izmenjen);
        if(greska != null)
        {
            Console.WriteLine($"Greska: {greska}");
            return;
        }

        Console.WriteLine("Posetilac izmenjen.");
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

        if(_posetilacService.Delete(id))
        {
            Console.WriteLine("Posetilac obrisan.");
        }
        else
        {
            Console.WriteLine("Posetilac nije pronadjen.");
        }
    }

    private int InputId()
    {
        Console.WriteLine("Unesite ID posetioca: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    private Posetilac InputPosetilac()
    {
        Console.WriteLine("Unesite ime: ");
        string ime = ConsoleViewUtils.InputString();

        Console.WriteLine("Unesite prezime: ");
        string prezime = ConsoleViewUtils.InputString();

        Console.WriteLine("Unesite datum rodjenja (dd.MM.yyyy): ");
        DateTime datumRodjenja = ConsoleViewUtils.SafeInputDate();

        Console.WriteLine("Unesite ID adrese: ");
        int adresaId = ConsoleViewUtils.SafeInputInt();

        Console.WriteLine("Unesite kontakt telefon: ");
        string telefon = ConsoleViewUtils.InputString();

        Console.WriteLine("Unesite email: ");
        string email = ConsoleViewUtils.InputString();

        Console.WriteLine("Unesite broj clanske karte (npr. SK-1-2023): ");
        string brojKarte = ConsoleViewUtils.InputString();

        Console.WriteLine("Unesite trenutnu godinu clanstva: ");
        int godina = ConsoleViewUtils.SafeInputInt();

        Status status = InputStatus();

        return new Posetilac
        {
            Ime = ime,
            Prezime = prezime,
            DatumRodjenja = datumRodjenja,
            AdresaId = adresaId,
            KontaktTelefon = telefon,
            Email = email,
            BrojClanskeKarte = brojKarte,
            TrenutnaGodinaClanstva = godina,
            Status = status
        };
    }

    private Status InputStatus()
    {
        Console.WriteLine("Unesite status (R - redovan, V - VIP): ");
        string unos = ConsoleViewUtils.InputString().Trim().ToUpper();

        while(unos != "R" && unos != "V")
        {
            Console.WriteLine("Unesite R ili V: ");
            unos = ConsoleViewUtils.InputString().Trim().ToUpper();
        }

        return unos == "V" ? Status.VIP : Status.Redovan;
    }
}