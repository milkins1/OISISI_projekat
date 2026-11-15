using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;
using Oisisi.Core.Utils;

namespace Oisisi.Core.DAO;

public class PosetilacDao : IPosetilacDao
{
    private readonly string _putanja;
    private readonly List<Posetilac> _posetioci;

    public PosetilacDao(string putanja)
    {
        _putanja = putanja;
        _posetioci = JsonStorage.Load<Posetilac>(_putanja);
    }

    public List<Posetilac> GetAll()
    {
        return new List<Posetilac>(_posetioci);
    }

    public Posetilac? GetById(int id)
    {
        return _posetioci.FirstOrDefault(p =>  p.Id == id);
    }

    public void Add(Posetilac posetilac)
    {
        posetilac.Id = GenerisiId();
        _posetioci.Add(posetilac);
    }

    public bool Update(Posetilac posetilac)
    {
        int index = _posetioci.FindIndex(p => p.Id == posetilac.Id);
        if(index == -1)
        {
            return false;
        }

        _posetioci[index] = posetilac;
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
        return true;
    }

    public void Save()
    {
        JsonStorage.Save(_putanja, _posetioci);
    }

    private int GenerisiId()
    {
        if(_posetioci.Count == 0)
        {
            return 1;
        }

        return _posetioci.Max(p => p.Id) + 1;
    }
}
