using System.Net.Mail;
using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;

namespace Oisisi.Core.Services;

public class AutorService
{
    private readonly IAutorDao _autorDao;
    private readonly IAdresaDao _adresaDao;
    private readonly IIzdavacDao _izdavacDao;

    public AutorService(IAutorDao autorDao, IAdresaDao adresaDao, IIzdavacDao izdavacDao)
    {
        _autorDao = autorDao;
        _adresaDao = adresaDao;
        _izdavacDao = izdavacDao;
    }

    public List<Autor> GetAll()
    {
        return _autorDao.GetAll();
    }

    public Autor? GetById(int id)
    {
        return _autorDao.GetById(id);
    }

    public string? Add(Autor autor)
    {
        string? greska = Validiraj(autor);

        if (greska != null)
        {
            return greska;
        }

        _autorDao.Add(autor);
        return null;
    }

    public string? Update(Autor autor)
    {
        if (_autorDao.GetById(autor.Id) == null)
        {
            return "Autor nije pronadjen.";
        }

        string? greska = Validiraj(autor, autor.Id);

        if (greska != null)
        {
            return greska;
        }

        if (!_autorDao.Update(autor))
        {
            return "Autor nije pronadjen.";
        }

        return null;
    }


    public string? Delete(int id)
    {
        Autor? autor = _autorDao.GetById(id);

        if (autor == null)
        {
            return "Autor nije pronadjen.";
        }

        if (autor.KnjigeIds.Count > 0)
        {
            return "Autor ima povezane knjige i ne moze biti obrisan.";
        }

        bool povezanSaIzdavacem = _izdavacDao.GetAll().Any(i =>
            i.SefAutorId == id || i.AutoriIds.Contains(id));

        if (povezanSaIzdavacem)
        {
            return "Autor je povezan sa izdavacem i ne moze biti obrisan.";
        }

        if (!_autorDao.Delete(id))
        {
            return "Autor nije pronadjen.";
        }

        return null;
    }

    private string? Validiraj(Autor autor, int izuzetiId = 0)
    {
        string[] tekstovi =
        {
            autor.Ime,
            autor.Prezime,
            autor.KontaktTelefon,
            autor.Email,
            autor.BrojLicneKarte
        };

        foreach (string tekst in tekstovi)
        {
            if (string.IsNullOrWhiteSpace(tekst))
            {
                return "Ime, prezime, telefon, email i broj licne karte su obavezni.";
            }

            if (tekst.Contains('|') ||
                tekst.Contains('\n') ||
                tekst.Contains('\r'))
            {
                return "Tekst ne sme sadrzati znak | ili novi red.";
            }
        }

        if (autor.DatumRodjenja == default ||
            autor.DatumRodjenja.Date > DateTime.Today)
        {
            return "Datum rodjenja mora biti unet i ne sme biti u buducnosti.";
        }

        if (_adresaDao.GetById(autor.AdresaId) == null)
        {
            return "Izabrana adresa ne postoji.";
        }

        if (!MailAddress.TryCreate(autor.Email.Trim(), out var email) ||
            email.Address != autor.Email.Trim())
        {
            return "Email nije ispravan.";
        }

        if (autor.GodineIskustva < 0)
        {
            return "Godine iskustva ne mogu biti negativne.";
        }

        Autor? isti = _autorDao.GetAll().Find(a =>
            a.Id != izuzetiId &&
            string.Equals(
                a.BrojLicneKarte.Trim(),
                autor.BrojLicneKarte.Trim(),
                StringComparison.OrdinalIgnoreCase));

        if (isti != null)
        {
            return "Autor sa tim brojem licne karte vec postoji.";
        }

        return null;
    }
}