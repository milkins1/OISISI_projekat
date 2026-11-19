using System.Globalization;
using Oisisi.Core.Interfaces;

namespace Oisisi.Core.Models;

public class Autor : ISerializable
{
    public int Id { get; set; }
    public string Ime { get; set; } = string.Empty;
    public string Prezime { get; set; } = string.Empty;
    public DateTime DatumRodjenja { get; set; }
    public int AdresaId { get; set; }
    public string KontaktTelefon { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string BrojLicneKarte { get; set; } = string.Empty;
    public int GodineIskustva { get; set; }
    public List<int> KnjigeIds { get; set; } = new List<int>();

    public string[] ToCSV()
    {
        string[] csvValues =
        {
        Id.ToString(),
        Ime,
        Prezime,
        DatumRodjenja.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
        AdresaId.ToString(),
        KontaktTelefon,
        Email,
        BrojLicneKarte,
        GodineIskustva.ToString(),
        string.Join(",", KnjigeIds)
    };

        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Ime = values[1];
        Prezime = values[2];
        DatumRodjenja = DateTime.ParseExact(
            values[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        AdresaId = int.Parse(values[4]);
        KontaktTelefon = values[5];
        Email = values[6];
        BrojLicneKarte = values[7];
        GodineIskustva = int.Parse(values[8]);

        KnjigeIds = new List<int>();

        if (!string.IsNullOrEmpty(values[9]))
        {
            foreach (string id in values[9].Split(','))
            {
                KnjigeIds.Add(int.Parse(id));
            }
        }
    }

    public override string ToString()
    {
        return $"ID: {Id} | {Ime} {Prezime} | LK: {BrojLicneKarte} | " +
               $"Iskustvo: {GodineIskustva} | Knjiga: {KnjigeIds.Count}";
    }
}
