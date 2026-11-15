using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;
using Oisisi.Core.Utils;

namespace Oisisi.Core.DAO;

public class KupovinaDao : IKupovinaDao
{
    private readonly string _putanja;
    private readonly List<Kupovina> _kupovine;

    public KupovinaDao(string putanja)
    {
        _putanja = putanja;
        _kupovine = JsonStorage.Load<Kupovina>(_putanja);
    }

    public List<Kupovina> GetAll()
    {
        return new List<Kupovina>(_kupovine);
    }

    public Kupovina? GetById(int id)
    {
        return _kupovine.FirstOrDefault(k => k.Id == id);
    }

    public List<Kupovina> GetByPosetilacId(int posetilacId)
    {
        return _kupovine.Where(k => k.PosetilacId == posetilacId).ToList();
    }

    public void Add(Kupovina kupovina)
    {
        kupovina.Id = GenerisiId();
        _kupovine.Add(kupovina);
    }

    public bool Update(Kupovina kupovina)
    {
        int index = _kupovine.FindIndex(k => k.Id == kupovina.Id);
        if (index == -1)
        {
            return false;
        }

        _kupovine[index] = kupovina;
        return true;
    }

    public bool Delete(int id)
    {
        Kupovina? postojeca = GetById(id);
        if(postojeca == null)
        {
            return false;
        }

        _kupovine.Remove(postojeca);
        return true;
    }

    public void Save()
    {
        JsonStorage.Save(_putanja, _kupovine);
    }

    private int GenerisiId()
    {
        if(_kupovine.Count == 0)
        {
            return 1;
        }

        return _kupovine.Max(k => k.Id) + 1;
    }
}