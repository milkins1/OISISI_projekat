using Oisisi.Core.Models;

namespace Oisisi.Core.Interfaces;

public interface IAutorDao
{
    List<Autor> GetAll();
    Autor? GetById(int id);
    void Add(Autor autor);
    bool Update(Autor autor);
    bool Delete(int id);
    void Save();
}