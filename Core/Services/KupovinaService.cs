using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;

namespace Oisisi.Core.Services;

public class KupovinaService
{
    private readonly IKupovinaDao _kupovinaDao;
    private readonly IPosetilacDao _posetilacDao;

    public KupovinaService(IKupovinaDao kupovinaDao, IPosetilacDao posetilacDao)
    {
        _kupovinaDao = kupovinaDao;
        _posetilacDao = posetilacDao; 
    }

    public Kupovina? GetById(int id)
    {
        return _kupovinaDao.GetById(id);
    }

    public List<Kupovina> GetByPosetilacId(int posetilacId)
    {
        return _kupovinaDao.GetByPosetilacId(posetilacId);
    }

    public string? Add(Kupovina kupovina)
    { 
        string? greska = Validiraj(kupovina);
        if (greska != null)
        {
            return greska;
        }

        _kupovinaDao.Add(kupovina);
        return null;
    }

    public string? Update(Kupovina kupovina)
    {
        string? greska = Validiraj(kupovina);
        if(greska != null)
        {
            return greska;
        }

        bool uspeh = _kupovinaDao.Update(kupovina);
        if(!uspeh)
        {
            return "Kupovina nije pronadjena.";
        }

        return null;
    }

    public bool Delete(int id)
    {
        return _kupovinaDao.Delete(id);
    }

    private string? Validiraj(Kupovina kupovina)
    {
        if(_posetilacDao.GetById(kupovina.PosetilacId) == null)
        {
            return "Posetilac sa tim ID-jem ne postoji.";
        }

        if(kupovina.KnjigaId <= 0)
        {
            return "ID knjige mora biti veci od nule.";
        }

        if(kupovina.Ocena < 1 || kupovina.Ocena > 5)
        {
            return "Ocena mora biti od 1 do 5.";
        }

        if(kupovina.DatumKupovine > DateTime.Now)
        {
            return "Datum kupovine ne moze biti u buducnosti.";
        }

        if(kupovina.Komentar.Contains('|'))
        {
            return "Komentar ne smije sadrzati |.";
        }

        return null;
    }
}