using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;

namespace Oisisi.Core.Services;

public class PosetilacService
{
    private readonly IPosetilacDao _posetilacDao;
    private readonly IKupovinaDao _kupovinaDao;

    public PosetilacService(IPosetilacDao posetilacDao, IKupovinaDao kupovinaDao)
    {
        _posetilacDao = posetilacDao;
        _kupovinaDao = kupovinaDao;
    }

    public List<Posetilac> GetAll()
    {
        return _posetilacDao.GetAll();
    }

    public Posetilac? GetById(int id)
    {
        return _posetilacDao.GetById(id);
    }

    public string? Add(Posetilac posetilac)
    {
        string? greska = Validiraj(posetilac);
        if(greska != null)
        {
            return greska;
        }

        _posetilacDao.Add(posetilac);
        return null;
    }

    public string? Update(Posetilac posetilac)
    {
        string? greska = Validiraj(posetilac);
        if(greska != null)
        {
            return greska;
        }

        bool uspeh = _posetilacDao.Update(posetilac);
        if(!uspeh)
        {
            return "Posetilac nije pronadjen.";
        }

        return null;
    }

    public bool Delete(int id)
    {
        List<Kupovina> kupovine = _kupovinaDao.GetByPosetilacId(id);
        foreach(Kupovina kupovina in kupovine)
        {
            _kupovinaDao.Delete(kupovina.Id);
        }
        return _posetilacDao.Delete(id);
    }

    public double IzracunajProsecnuOcenu(int posetilacId)
    {
        List<Kupovina> kupovine = _kupovinaDao.GetByPosetilacId(posetilacId);
        if(kupovine.Count == 0)
        {
            return 0;
        }

        int zbir = 0;
        foreach(Kupovina kupovina in kupovine)
        {
            zbir += kupovina.Ocena;
        }
        return (double) zbir / kupovine.Count;
    }

    private string? Validiraj(Posetilac posetilac)
    {
        if(string.IsNullOrWhiteSpace(posetilac.Ime) || string.IsNullOrWhiteSpace(posetilac.Prezime))
        {
            return "Ime i prezime su obavezni.";
        }

        if(string.IsNullOrWhiteSpace(posetilac.BrojClanskeKarte))
        {
            return "Broj clanske karte je obavezan.";
        }

        if(string.IsNullOrWhiteSpace(posetilac.KontaktTelefon))
        {
            return "Kontakt telefon je obavezan.";
        }

        if(!posetilac.Email.Contains('@'))
        {
            return "Email nije ispravan.";
        }

        if(posetilac.DatumRodjenja > DateTime.Now)
        {
            return "Datum rodjenja ne moze biti u buducnosti.";
        }

        if(posetilac.TrenutnaGodinaClanstva <= 0)
        {
            return "Godina clanstva mora biti veca od nule.";
        }

        string[] tekstovi =
        {
            posetilac.Ime, posetilac.Prezime, posetilac.KontaktTelefon, posetilac.Email, posetilac.BrojClanskeKarte
        };

        foreach(string tekst in tekstovi)
        {
            if(tekst.Contains('|'))
            {
                return "Tekst ne sme sadrzati znak |.";
            }
        }

        Posetilac? isti = _posetilacDao.GetAll().Find(p => p.BrojClanskeKarte.ToLower() == posetilac.BrojClanskeKarte.ToLower() && p.Id != posetilac.Id);
        if(isti != null)
        {
            return "Broj clanske karte vec postoji.";
        }

        return null;
    }
}