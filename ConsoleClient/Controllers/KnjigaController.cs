using Oisisi.Core.Models;
using Oisisi.Core.Services;

namespace Oisisi.ConsoleClient.Controllers;

class KnjigaController
{
    private readonly KnjigaService _knjigaService;
    private readonly AutorService _autorService;
    private readonly IzdavacService _izdavacService;

    public KnjigaController(
        KnjigaService knjigaService,
        AutorService autorService,
        IzdavacService izdavacService)
    {
        _knjigaService = knjigaService;
        _autorService = autorService;
        _izdavacService = izdavacService;
    }

    public void RunMenu()
    {
        while (true)
        {
            Console.WriteLine("\n------ KNJIGE ------");
            Console.WriteLine("1: Prikazi sve knjige");
            Console.WriteLine("2: Dodaj knjigu");
            Console.WriteLine("3: Izmeni knjigu");
            Console.WriteLine("4: Obrisi knjigu");
            Console.WriteLine("5: Prikazi kupce knjige");
            Console.WriteLine("6: Prikazi posetioce koji zele knjigu");
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
                case "5":
                    ShowPosetioci(true);
                    break;
                case "6":
                    ShowPosetioci(false);
                    break;
                default:
                    Console.WriteLine("Nepostojeca opcija.");
                    break;
            }
        }
    }

    private void ShowAll()
    {
        List<Knjiga> knjige = _knjigaService.GetAll();

        if (knjige.Count == 0)
        {
            Console.WriteLine("Nema knjiga.");
            return;
        }

        foreach (Knjiga knjiga in knjige)
        {
            Console.WriteLine(knjiga);
        }
    }

    private void Add()
    {
        Knjiga? knjiga = InputKnjiga();

        if (knjiga == null)
        {
            return;
        }

        string? greska = _knjigaService.Add(knjiga);

        Console.WriteLine(greska == null
            ? "Knjiga dodata."
            : $"Greska: {greska}");
    }

    private void Update()
    {
        int id = InputId();

        Knjiga? postojeca = _knjigaService.GetById(id);

        if (postojeca == null)
        {
            Console.WriteLine("Knjiga nije pronadjena.");
            return;
        }

        Console.WriteLine($"Trenutni podaci: {postojeca}");
        Console.WriteLine("Unesite podatke za izmenu:");

        Knjiga? izmenjena = InputKnjiga();

        if (izmenjena == null)
        {
            return;
        }

        izmenjena.Id = id;

        string? greska = _knjigaService.Update(izmenjena);

        Console.WriteLine(greska == null
            ? "Knjiga izmenjena."
            : $"Greska: {greska}");
    }

    private void Delete()
    {
        int id = InputId();

        Knjiga? knjiga = _knjigaService.GetById(id);

        if (knjiga == null)
        {
            Console.WriteLine("Knjiga nije pronadjena.");
            return;
        }

        Console.WriteLine($"Brisete knjigu: {knjiga.Naziv}");
        Console.WriteLine(
            "Bice uklonjene i njene kupovine i stavke lista zelja.");
        Console.WriteLine("Da li ste sigurni? (da/ne):");

        string odgovor = ConsoleViewUtils.InputString().Trim();

        if (!odgovor.Equals("da", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Brisanje otkazano.");
            return;
        }

        string? greska = _knjigaService.Delete(id);

        Console.WriteLine(greska == null
            ? "Knjiga obrisana."
            : $"Greska: {greska}");
    }

    private void ShowPosetioci(bool kupci)
    {
        int id = InputId();

        if (_knjigaService.GetById(id) == null)
        {
            Console.WriteLine("Knjiga nije pronadjena.");
            return;
        }

        List<Posetilac> posetioci = kupci
            ? _knjigaService.GetKupci(id)
            : _knjigaService.GetPosetiociKojiZeleKnjigu(id);

        Console.WriteLine(kupci
            ? "\nKupci knjige:"
            : "\nPosetioci koji zele knjigu:");

        if (posetioci.Count == 0)
        {
            Console.WriteLine("Nema posetilaca za izabranu knjigu.");
            return;
        }

        foreach (Posetilac posetilac in posetioci)
        {
            Console.WriteLine(posetilac);
        }
    }

    private int InputId()
    {
        Console.WriteLine("Unesite ID knjige:");
        return ConsoleViewUtils.SafeInputInt();
    }

    private Knjiga? InputKnjiga()
    {
        List<Autor> autori = _autorService.GetAll();
        List<Izdavac> izdavaci = _izdavacService.GetAll();

        if (autori.Count == 0 || izdavaci.Count == 0)
        {
            Console.WriteLine(
                "Prvo dodajte najmanje jednog autora i jednog izdavaca.");
            return null;
        }

        Console.WriteLine("\nDostupni autori:");

        foreach (Autor autor in autori)
        {
            Console.WriteLine(autor);
        }

        Console.WriteLine("\nDostupni izdavaci:");

        foreach (Izdavac izdavac in izdavaci)
        {
            Console.WriteLine(izdavac);
        }

        Knjiga knjiga = new Knjiga();

        Console.WriteLine("Unesite ISBN:");
        knjiga.Isbn = ConsoleViewUtils.InputString().Trim();

        Console.WriteLine("Unesite naziv:");
        knjiga.Naziv = ConsoleViewUtils.InputString().Trim();

        Console.WriteLine("Unesite zanr:");
        knjiga.Zanr = ConsoleViewUtils.InputString().Trim();

        Console.WriteLine("Unesite godinu izdanja:");
        knjiga.GodinaIzdanja = ConsoleViewUtils.SafeInputInt();

        Console.WriteLine("Unesite cenu, npr. 1250,50:");
        knjiga.Cena = ConsoleViewUtils.SafeInputDecimal();

        Console.WriteLine("Unesite broj strana:");
        knjiga.BrojStrana = ConsoleViewUtils.SafeInputInt();

        Console.WriteLine("Koliko autora ima knjiga?");
        int brojAutora = ConsoleViewUtils.SafeInputInt();

        for (int i = 0; i < brojAutora; i++)
        {
            Console.WriteLine($"Unesite ID autora broj {i + 1}:");
            knjiga.AutoriIds.Add(ConsoleViewUtils.SafeInputInt());
        }

        Console.WriteLine("Unesite ID izdavaca:");
        knjiga.IzdavacId = ConsoleViewUtils.SafeInputInt();

        return knjiga;
    }
}