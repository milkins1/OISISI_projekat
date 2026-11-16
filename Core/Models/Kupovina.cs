using System.Globalization;
using Oisisi.Core.Interfaces;

namespace Oisisi.Core.Models;

public class Kupovina : ISerializable
{
    public int Id { get; set; }
    public int PosetilacId { get; set; }
    public int KnjigaId { get; set; }
    public DateTime DatumKupovine { get; set; }
    public int Ocena { get; set; }
    public string Komentar { get; set; } = string.Empty;

    public Kupovina()
    {
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            PosetilacId.ToString(),
            KnjigaId.ToString(),
            DatumKupovine.ToString("yyyy-MM-dd"),
            Ocena.ToString(),
            Komentar

        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        PosetilacId = int.Parse(values[1]);
        KnjigaId = int.Parse(values[2]);
        DatumKupovine = DateTime.ParseExact(values[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        Ocena = int.Parse(values[4]);
        Komentar = values[5];
    }

    public override string ToString()
    {
        return $"ID: {Id,3} | Posetilac: {PosetilacId,3} | Knjiga: {KnjigaId,3} | {DatumKupovine:dd.MM.yyyy} | Ocena: {Ocena} | {Komentar}";
    }
}
