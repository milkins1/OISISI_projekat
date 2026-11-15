using Oisisi.Core.Models;

namespace Oisisi.Core.Interfaces;

public interface IKupovinaDao
{
    List<Kupovina> GetAll();
    Kupovina? GetById(int id);
    List<Kupovina> GetByPosetilacId(int posetilacId);
    void Add(Kupovina kupovina);
    bool Update(Kupovina kupovina);
    bool Delete(int id);
    void Save();
}
