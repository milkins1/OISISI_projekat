using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;

namespace Oisisi.Core.Services;

public class AdresaService
{
    private readonly IAdresaDao _adresaDao;

    public AdresaService(IAdresaDao adresaDao)
    {
        _adresaDao = adresaDao;
    }

    public List<Adresa> GetAll()
    {
        return _adresaDao.GetAll();
    }

    public Adresa? GetById(int id)
    {
        return _adresaDao.GetById(id);
    }

    public string? Add(Adresa adresa)
    {
        string? greska = Validiraj(adresa);

        if (greska != null)
        {
            return greska;
        }

        _adresaDao.Add(adresa);
        return null;
    }

    public string? Update(Adresa adresa)
    {
        string? greska = Validiraj(adresa);

        if (greska != null)
        {
            return greska;
        }

        bool uspeh = _adresaDao.Update(adresa);

        if (!uspeh)
        {
            return "Adresa nije pronadjena.";
        }

        return null;
    }

    public bool Delete(int id)
    {
        return _adresaDao.Delete(id);
    }

    private string? Validiraj(Adresa adresa)
    {
        string[] tekstovi =
        {
            adresa.Ulica,
            adresa.Broj,
            adresa.Grad,
            adresa.Drzava
        };

        foreach (string tekst in tekstovi)
        {
            if (string.IsNullOrWhiteSpace(tekst))
            {
                return "Ulica, broj, grad i drzava su obavezni.";
            }

            if (tekst.Contains('|') ||
                tekst.Contains('\n') ||
                tekst.Contains('\r'))
            {
                return "Polja adrese ne smeju sadrzati znak | ili novi red.";
            }
        }

        return null;
    }
}