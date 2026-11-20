using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;

namespace Oisisi.Core.Services;

public class KnjigaService
{
    private readonly IKnjigaDao _knjigaDao;
    private readonly IAutorDao _autorDao;
    private readonly IIzdavacDao _izdavacDao;
    private readonly IKupovinaDao _kupovinaDao;
    private readonly IPosetilacDao _posetilacDao;

    public KnjigaService( IKnjigaDao knjigaDao, IAutorDao autorDao, IIzdavacDao izdavacDao, IKupovinaDao kupovinaDao, IPosetilacDao posetilacDao)
    {
        _knjigaDao = knjigaDao;
        _autorDao = autorDao;
        _izdavacDao = izdavacDao;
        _kupovinaDao = kupovinaDao;
        _posetilacDao = posetilacDao;
    }

    public List<Knjiga> GetAll()
    {
        return _knjigaDao.GetAll();
    }

    public Knjiga? GetById(int id)
    {
        return _knjigaDao.GetById(id);
    }

    public string? Add(Knjiga knjiga)
    {
        string? greska = Validiraj(knjiga);

        if (greska != null)
        {
            return greska;
        }

        _knjigaDao.Add(knjiga);



        UskladiVeze(
            knjiga.AutoriIds,
            new List<int> { knjiga.IzdavacId });

        return null;
    }

    public string? Update(Knjiga knjiga)
    {
        Knjiga? postojeca = _knjigaDao.GetById(knjiga.Id);

        if (postojeca == null)
        {
            return "Knjiga nije pronadjena.";
        }

        string? greska = Validiraj(knjiga, knjiga.Id);

        if (greska != null)
        {
            return greska;
        }

        List<int> autoriIds = postojeca.AutoriIds
            .Union(knjiga.AutoriIds)
            .ToList();

        List<int> izdavaciIds = new List<int>
        {
            postojeca.IzdavacId,
            knjiga.IzdavacId
        };

        if (!_knjigaDao.Update(knjiga))
        {
            return "Knjiga nije pronadjena.";
        }

        UskladiVeze(autoriIds, izdavaciIds);
        return null;
    }

    public string? Delete(int id)
    {
        Knjiga? knjiga = _knjigaDao.GetById(id);

        if (knjiga == null)
        {
            return "Knjiga nije pronadjena.";
        }

        List<Kupovina> kupovine = _kupovinaDao.GetAll()
            .Where(k => k.KnjigaId == id)
            .ToList();

        foreach (Kupovina kupovina in kupovine)
        {
            _kupovinaDao.Delete(kupovina.Id);
        }

        foreach (Posetilac posetilac in _posetilacDao.GetAll())
        {
            int brojUklonjenih = posetilac.ListaZeljaKnjigaIds
                .RemoveAll(knjigaId => knjigaId == id);

            if (brojUklonjenih > 0)
            {
                _posetilacDao.Update(posetilac);
            }
        }

        if (!_knjigaDao.Delete(id))
        {
            return "Knjiga nije pronadjena.";
        }

        UskladiVeze(
            knjiga.AutoriIds,
            new List<int> { knjiga.IzdavacId });

        return null;
    }

    public List<Posetilac> GetKupci(int knjigaId)
    {
        List<int> kupciIds = _kupovinaDao.GetAll()
            .Where(k => k.KnjigaId == knjigaId)
            .Select(k => k.PosetilacId)
            .Distinct()
            .ToList();

        return _posetilacDao.GetAll()
            .Where(p => kupciIds.Contains(p.Id))
            .ToList();
    }

    public List<Posetilac> GetPosetiociKojiZeleKnjigu(int knjigaId)
    {
        return _posetilacDao.GetAll()
            .Where(p => p.ListaZeljaKnjigaIds.Contains(knjigaId))
            .ToList();
    }
    private string? Validiraj(Knjiga knjiga, int izuzetiId = 0)
    {
        string[] tekstovi =
        {
            knjiga.Isbn,
            knjiga.Naziv,
            knjiga.Zanr
        };

        foreach (string tekst in tekstovi)
        {
            if (string.IsNullOrWhiteSpace(tekst))
            {
                return "ISBN, naziv i zanr su obavezni.";
            }

            if (tekst.Contains('|') ||
                tekst.Contains('\n') ||
                tekst.Contains('\r'))
            {
                return "Tekst ne sme sadrzati znak | ili novi red.";
            }
        }

        string isbn = NormalizujIsbn(knjiga.Isbn);

        if (isbn.Length == 0)
        {
            return "ISBN mora biti unet.";
        }

        bool duplikat = _knjigaDao.GetAll().Any(k =>
            k.Id != izuzetiId &&
            NormalizujIsbn(k.Isbn) == isbn);

        if (duplikat)
        {
            return "Knjiga sa tim ISBN-om vec postoji.";
        }

        if (knjiga.GodinaIzdanja <= 0 ||
            knjiga.GodinaIzdanja > DateTime.Today.Year)
        {
            return "Godina izdanja mora biti pozitivna i ne sme biti buduca.";
        }

        if (knjiga.Cena < 0)
        {
            return "Cena ne moze biti negativna.";
        }

        if (knjiga.BrojStrana <= 0)
        {
            return "Broj strana mora biti veci od nule.";
        }

        if (knjiga.AutoriIds.Count == 0)
        {
            return "Knjiga mora imati najmanje jednog autora.";
        }

        if (knjiga.AutoriIds.Distinct().Count() != knjiga.AutoriIds.Count)
        {
            return "Isti autor ne sme biti naveden vise puta.";
        }

        foreach (int autorId in knjiga.AutoriIds)
        {
            if (_autorDao.GetById(autorId) == null)
            {
                return $"Autor sa ID-jem {autorId} ne postoji.";
            }
        }

        if (_izdavacDao.GetById(knjiga.IzdavacId) == null)
        {
            return "Izabrani izdavac ne postoji.";
        }

        return null;
    }

    private static string NormalizujIsbn(string isbn)
    {
        return isbn.Replace("-", "")
            .Replace(" ", "")
            .ToUpperInvariant();
    }


    /*
     Na osnovu knjiga ponovo odredjuem liste knjiga autora
    i liste knjiga i autora izdavaca obuhvacenih promenom
     */

    private void UskladiVeze(
        List<int> autoriIds,
        List<int> izdavaciIds)
    {
        List<Knjiga> knjige = _knjigaDao.GetAll();

        foreach (int autorId in autoriIds.Distinct())
        {
            Autor? autor = _autorDao.GetById(autorId);

            if (autor == null)
            {
                continue;
            }

            autor.KnjigeIds = knjige
                .Where(k => k.AutoriIds.Contains(autorId))
                .Select(k => k.Id)
                .ToList();

            _autorDao.Update(autor);
        }

        foreach (int izdavacId in izdavaciIds.Distinct())
        {
            Izdavac? izdavac = _izdavacDao.GetById(izdavacId);

            if (izdavac == null)
            {
                continue;
            }

            List<Knjiga> knjigeIzdavaca = knjige
                .Where(k => k.IzdavacId == izdavacId)
                .ToList();

            izdavac.KnjigeIds = knjigeIzdavaca
                .Select(k => k.Id)
                .ToList();

            izdavac.AutoriIds = knjigeIzdavaca
                .SelectMany(k => k.AutoriIds)
                .Distinct()
                .ToList();

            if (izdavac.SefAutorId.HasValue &&
                !izdavac.AutoriIds.Contains(izdavac.SefAutorId.Value))
            {
                izdavac.SefAutorId = null;
            }

            _izdavacDao.Update(izdavac);
        }
    }
}