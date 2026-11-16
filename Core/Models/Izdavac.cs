using System.Windows.Markup;
using Oisisi.Core.Interfaces;

namespace Oisisi.Core.Models;

public class Izdavac : ISerializable
{
    public int Id { get; set; }
    public string Sifra { get; set; } = string.Empty;
    public string Naziv { get; set; } = string.Empty;
    public int? SefAutorId { get; set; }
    public List<int> AutoriIds { get; set; } = new List<int>();
    public List<int> KnjigeIds { get; set; } = new List<int>();

    public Izdavac()
    {
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Sifra,
            Naziv,
            SefAutorId.HasValue ? SefAutorId.Value.ToString() : string.Empty,
            string.Join(",", AutoriIds),
            string.Join(",", KnjigeIds)
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Sifra = values[1];
        Naziv = values[2];

        if (string.IsNullOrEmpty(values[3]))
        {
            SefAutorId = null;
        }

        AutoriIds = ParseIds(values[4]);
        KnjigeIds = ParseIds(values[5]);
    }

    private static List<int> ParseIds(string text)
    {
        List<int> ids = new List<int>();
        if(!string.IsNullOrEmpty(text))
        {
            foreach(string id in text.Split(','))
            {
                ids.Add(int.Parse(id));
            }
        }
        return ids;
    }

    public override string ToString()
    {
        string sef = SefAutorId.HasValue ? SefAutorId.Value.ToString() : "nema";
        return $"ID : {Id,3} | {Sifra,-8} | {Naziv,-20} | Sef (ID autora): {sef} | Autora: {AutoriIds.Count} | Knjiga: {KnjigeIds.Count}";
    }
}
