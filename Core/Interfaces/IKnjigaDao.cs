using Oisisi.Core.Models;

namespace Oisisi.Core.Interfaces;

public interface IKnjigaDao
{
    List<Knjiga> GetAll();
    Knjiga? GetById(int id);
    void Add(Knjiga knjiga);
    bool Update(Knjiga knjiga);
    bool Delete(int id);
    void Save();
}