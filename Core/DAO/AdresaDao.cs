using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;
using Oisisi.Core.Utils;

namespace Oisisi.Core.DAO;

public class AdresaDao : IAdresaDao
{
    private readonly List<Adresa> _adrese;
    private readonly Storage<Adresa> _storage;

    public AdresaDao()
    {
        _storage = new Storage<Adresa>("adrese.txt");
        _adrese = _storage.Load();
    }

    public List<Adresa> GetAll()
    {
        return new List<Adresa>(_adrese);
    }

    public Adresa? GetById(int id)
    {
        return _adrese.Find(a => a.Id == id);
    }

    public void Add(Adresa adresa)
    {
        adresa.Id = GenerisiId();
        _adrese.Add(adresa);
        Save();
    }

    public bool Update(Adresa adresa)
    {
        int index = _adrese.FindIndex(a => a.Id == adresa.Id);

        if (index == -1)
        {
            return false;
        }

        _adrese[index] = adresa;
        Save();
        return true;
    }

    public bool Delete(int id)
    {
        Adresa? postojeca = GetById(id);

        if (postojeca == null)
        {
            return false;
        }

        _adrese.Remove(postojeca);
        Save();
        return true;
    }

    public void Save()
    {
        _storage.Save(_adrese);
    }

    private int GenerisiId()
    {
        int najveciId = 0;

        foreach (Adresa adresa in _adrese)
        {
            if (adresa.Id > najveciId)
            {
                najveciId = adresa.Id;
            }
        }

        return najveciId + 1;
    }
}