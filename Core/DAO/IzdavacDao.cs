using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;
using Oisisi.Core.Utils;

namespace Oisisi.Core.DAO;

public class IzdavacDao : IIzdavacDao
{
    private readonly List<Izdavac> _izdavaci;
    private readonly Storage<Izdavac> _storage;

    public IzdavacDao()
    {
        _storage = new Storage<Izdavac>("izdavaci.txt");
        _izdavaci = _storage.Load();
    }

    public List<Izdavac> GetAll()
    {
        return new List<Izdavac>(_izdavaci);
    }

    public Izdavac? GetById(int id)
    {
        return _izdavaci.Find(i => i.Id == id);
    }

    public Izdavac? GetBySifra(string sifra)
    {
        return _izdavaci.Find(i => i.Sifra.ToLower() == sifra.ToLower());
    }

    public void Add(Izdavac izdavac)
    {
        izdavac.Id = GenerisiId();
        _izdavaci.Add(izdavac);
        Save();
    }

    public bool Update(Izdavac izdavac)
    {
        int index = _izdavaci.FindIndex(i  => i.Id == izdavac.Id);
        if(index == -1)
        {
            return false;
        }
        _izdavaci[index] = izdavac;
        Save();
        return true;
    }

    public bool Delete(int id)
    {
        Izdavac? postojeci = GetById(id);
        if(postojeci == null)
        {
            return false;
        }
        _izdavaci.Remove(postojeci);
        Save();
        return true;
    }

    public void Save()
    {
        _storage.Save(_izdavaci);
    }

    private int GenerisiId()
    {
        if(_izdavaci.Count == 0)
        {
            return 1;
        }

        return _izdavaci[^1].Id + 1;
    }
}