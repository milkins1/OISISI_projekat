using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;
using Oisisi.Core.Utils;

namespace Oisisi.Core.DAO;

public class KupovinaDao : IKupovinaDao
{
    private readonly List<Kupovina> _kupovine;
    private readonly Storage<Kupovina> _storage;

    public KupovinaDao()
    {
        _storage = new Storage<Kupovina>("kupovine.txt");
        _kupovine = _storage.Load();
    }

    public List<Kupovina> GetAll()
    {
        return new List<Kupovina>(_kupovine);
    }

    public Kupovina? GetById(int id)
    {
        return _kupovine.Find(k => k.Id == id);
    }

    public List<Kupovina> GetByPosetilacId(int posetilacId)
    {
        return _kupovine.FindAll(k => k.PosetilacId == posetilacId);
    }

    public void Add(Kupovina kupovina)
    {
        kupovina.Id = GenerisiId();
        _kupovine.Add(kupovina);
        Save();
    }

    public bool Update(Kupovina kupovina)
    {
        int index = _kupovine.FindIndex(k => k.Id == kupovina.Id);
        if (index == -1)
        {
            return false;
        }

        _kupovine[index] = kupovina;
        Save();
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
        Save();
        return true;
    }

    public void Save()
    {
        _storage.Save(_kupovine);
    }

    private int GenerisiId()
    {
        if(_kupovine.Count == 0)
        {
            return 1;
        }

        return _kupovine[^1].Id + 1;
    }
}