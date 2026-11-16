using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;
using Oisisi.Core.Utils;

namespace Oisisi.Core.DAO;

public class IzdavacDao : IIzdavacDao
{
    private readonly string _putanja;
    private readonly List<Izdavac> _izdavaci;

    public IzdavacDao(string putanja)
    {
        _putanja = putanja;
        _izdavaci = JsonStorage.Load<Izdavac>(_putanja);
    }

    public List<Izdavac> GetAll()
    {
        return new List<Izdavac>(_izdavaci);
    }

    public Izdavac? GetById(int id)
    {
        return _izdavaci.FirstOrDefault(i => i.Id == id);
    }

    public Izdavac? GetBySifra(string sifra)
    {
        return _izdavaci.FirstOrDefault(i => string.Equals(i.Sifra, sifra, StringComparison.OrdinalIgnoreCase));
    }

    public void Add(Izdavac izdavac)
    {
        izdavac.Id = GenerisiId();
        _izdavaci.Add(izdavac);
    }

    public bool Update(Izdavac izdavac)
    {
        int index = _izdavaci.FindIndex(i  => i.Id == izdavac.Id);
        if(index == -1)
        {
            return false;
        }
        _izdavaci[index] = izdavac;
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
        return true;
    }

    public void Save()
    {
        JsonStorage.Save(_putanja, _izdavaci);
    }

    private int GenerisiId()
    {
        if(_izdavaci.Count == 0)
        {
            return 1;
        }

        return _izdavaci.Max(i => i.Id) + 1;
    }
}