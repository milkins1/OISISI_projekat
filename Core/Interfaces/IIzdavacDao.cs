using Oisisi.Core.Models;

namespace Oisisi.Core.Interfaces;

public interface IIzdavacDao
{
    List<Izdavac> GetAll();
    Izdavac? GetById(int id);
    Izdavac? GetBySifra(string sifra);
    void Add(Izdavac izdavac);
    bool Update(Izdavac izdavac);
    bool Delete(int id);
    void Save();
}