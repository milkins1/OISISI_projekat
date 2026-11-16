using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;
using Oisisi.Core.Utils;

namespace Oisisi.Core.DAO;

public class PosetilacDao : IPosetilacDao
{
    private readonly List<Posetilac> _posetioci;
    private readonly Storage<Posetilac> _storage;

    public PosetilacDao()
    {
        _storage = new Storage<Posetilac>("posetioci.txt");
        _posetioci = _storage.Load();
    }

    public List<Posetilac> GetAll()
    {
        return new List<Posetilac>(_posetioci);
    }

    public Posetilac? GetById(int id)
    {
        return _posetioci.Find(p =>  p.Id == id);
    }

    public void Add(Posetilac posetilac)
    {
        posetilac.Id = GenerisiId();
        _posetioci.Add(posetilac);
        Save();
    }

    public bool Update(Posetilac posetilac)
    {
        int index = _posetioci.FindIndex(p => p.Id == posetilac.Id);
        if(index == -1)
        {
            return false;
        }

        _posetioci[index] = posetilac;
        Save();
        return true;
    }

    public bool Delete(int id)
    {
        Posetilac? postojeci = GetById(id);
        if(postojeci == null)
        {
            return false;
        }

        _posetioci.Remove(postojeci);
        Save();
        return true;
    }

    public void Save()
    {
        _storage.Save(_posetioci);
    }

    private int GenerisiId()
    {
        if(_posetioci.Count == 0)
        {
            return 1;
        }

        return _posetioci[^1].Id + 1;
    }
}
