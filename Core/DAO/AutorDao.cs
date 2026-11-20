using Oisisi.Core.Interfaces;
using Oisisi.Core.Models;
using Oisisi.Core.Utils;

namespace Oisisi.Core.DAO;


/*
Osnovni CRUD i rad sa Storage<T> prilagodjeni su prema
primeru VehiclesDAO iz CRUDExample sa 3. vezbi.
 */

public class AutorDao : IAutorDao
{
    private readonly List<Autor> _autori;
    private readonly Storage<Autor> _storage;

    public AutorDao()
    {
        _storage = new Storage<Autor>("autori.txt");
        _autori = _storage.Load();
    }

    public List<Autor> GetAll()
    {
        return new List<Autor>(_autori);
    }

    public Autor? GetById(int id)
    {
        return _autori.Find(a => a.Id == id);
    }

    public void Add(Autor autor)
    {
        autor.Id = GenerisiId();
        _autori.Add(autor);
        Save();
    }

    public bool Update(Autor autor)
    {
        int index = _autori.FindIndex(a => a.Id == autor.Id);

        if (index == -1)
        {
            return false;
        }

        _autori[index] = autor;
        Save();
        return true;
    }

    public bool Delete(int id)
    {
        Autor? postojeci = GetById(id);

        if (postojeci == null)
        {
            return false;
        }

        _autori.Remove(postojeci);
        Save();
        return true;
    }

    public void Save()
    {
        _storage.Save(_autori);
    }

    private int GenerisiId()
    {
        if (_autori.Count == 0)
        {
            return 1;
        }

        return _autori.Max(a => a.Id) + 1;
    }
}