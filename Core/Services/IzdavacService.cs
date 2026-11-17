using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;

namespace Oisisi.Core.Services;

public class IzdavacService
{
    private readonly IIzdavacDao _izdavacDao;

    public IzdavacService(IIzdavacDao izdavacDao)
    {
        _izdavacDao = izdavacDao;
    }

    public List<Izdavac> GetAll()
    {
        return _izdavacDao.GetAll();
    }

    public Izdavac? GetById(int id)
    {
        return _izdavacDao.GetById(id);
    }

    public string? Add(Izdavac izdavac)
    {
        string? greska = Validiraj(izdavac);
        if (greska != null)
        {
            return greska;
        }

        _izdavacDao.Add(izdavac);
        return null;
    }

    public string? Update(Izdavac izdavac)
    {
        string? greska = Validiraj(izdavac);
        if (greska != null)
        {
            return greska;
        }

        bool uspeh = _izdavacDao.Update(izdavac);
        if(!uspeh)
        {
            return "Izdavac nije pronadjen.";
        }

        return null;
    }

    public bool Delete(int id)
    {
        return _izdavacDao.Delete(id);
    }

    private string? Validiraj(Izdavac izdavac)
    {
        if(string.IsNullOrWhiteSpace(izdavac.Sifra))
        {
            return "Sifra je obavezna.";
        }

        if(string.IsNullOrWhiteSpace(izdavac.Naziv))
        {
            return "Naziv je obavezan.";
        }

        if(izdavac.Sifra.Contains('|') || izdavac.Naziv.Contains('|'))
        {
            return "Tekst ne smije sadrzati znak |.";
        }

        Izdavac? isti = _izdavacDao.GetBySifra(izdavac.Sifra);
        if(isti != null && isti.Id != izdavac.Id)
        {
            return "Sifra vec postoji.";
        }

        return null;
    }
}