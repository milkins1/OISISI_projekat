using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;
using Oisisi.Core.Utils;

namespace Oisisi.Core.DAO;

public class KnjigaDao : IKnjigaDao
{
    private readonly List<Knjiga> _knjige;
    private readonly Storage<Knjiga> _storage;

    public KnjigaDao()
    {
        _storage = new Storage<Knjiga>("knjige.txt");
        _knjige = _storage.Load();
    }

    public List<Knjiga> GetAll()
    {
        return new List<Knjiga>(_knjige);
    }

    public Knjiga? GetById(int id)
    {
        return _knjige.Find(k => k.Id == id);
    }

    public void Add(Knjiga knjiga)
    {
        knjiga.Id = GenerisiId();
        _knjige.Add(knjiga);
        Save();
    }

    public bool Update(Knjiga knjiga)
    {
        int index = _knjige.FindIndex(k => k.Id == knjiga.Id);

        if (index == -1)
        {
            return false;
        }

        _knjige[index] = knjiga;
        Save();
        return true;
    }

    public bool Delete(int id)
    {
        Knjiga? postojeca = GetById(id);

        if (postojeca == null)
        {
            return false;
        }

        _knjige.Remove(postojeca);
        Save();
        return true;
    }

    public void Save()
    {
        _storage.Save(_knjige);
    }

    private int GenerisiId()
    {
        if (_knjige.Count == 0)
        {
            return 1;
        }

        return _knjige.Max(k => k.Id) + 1;
    }
}