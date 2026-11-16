using System.Globalization;
using Oisisi.Core.Interfaces;

namespace Oisisi.Core.Models;

public class Posetilac : ISerializable
{
    public int Id { get; set; }
    public string Ime { get; set; } = string.Empty;
    public string Prezime { get; set; } = string.Empty;
    public DateTime DatumRodjenja { get; set; }
    public int AdresaId { get; set; }
    public string KontaktTelefon { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string BrojClanskeKarte { get; set; } = string.Empty;
    public int TrenutnaGodinaClanstva { get; set; }
    public Status Status { get; set; }
    public List<int> ListaZeljaKnjigaIds { get; set; } = new List<int>();

    public Posetilac()
    {
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Ime,
            Prezime,
            DatumRodjenja.ToString("yyyy-MM-dd"),
            AdresaId.ToString(),
            KontaktTelefon,
            Email,
            BrojClanskeKarte,
            TrenutnaGodinaClanstva.ToString(),
            Status.ToString(),
            string.Join(",", ListaZeljaKnjigaIds)
        };

        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Ime = values[1];
        Prezime = values[2];
        DatumRodjenja = DateTime.ParseExact(values[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        AdresaId = int.Parse(values[4]);
        KontaktTelefon = values[5];
        Email = values[6];
        BrojClanskeKarte = values[7]; 
        TrenutnaGodinaClanstva = int.Parse(values[8]);
        Status = Enum.Parse<Status>(values[9]);

        ListaZeljaKnjigaIds = new List<int>();
        if(!string.IsNullOrEmpty(values[10]))
        {
            foreach(string id in values[10].Split(','))
            {
                ListaZeljaKnjigaIds.Add(int.Parse(id));
            }
        }
    }

    public override string ToString()
    {
        return $"ID: {Id,3} | {BrojClanskeKarte,-12} | {Ime} {Prezime} | {DatumRodjenja:dd.MM.yyyy} | {KontaktTelefon} | {Email} | {Status}";
    }
}



