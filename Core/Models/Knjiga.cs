using System.Globalization;
using Oisisi.Core.Interfaces;

namespace Oisisi.Core.Models;

public class Knjiga : ISerializable
{
    public int Id { get; set; }
    public string Isbn { get; set; } = string.Empty;
    public string Naziv { get; set; } = string.Empty;
    public string Zanr { get; set; } = string.Empty;
    public int GodinaIzdanja { get; set; }
    public decimal Cena { get; set; }
    public int BrojStrana { get; set; }
    public List<int> AutoriIds { get; set; } = new List<int>();
    public int IzdavacId { get; set; }


    public string[] ToCSV()
    {
        string[] csvValues =
        {
        Id.ToString(),
        Isbn,
        Naziv,
        Zanr,
        GodinaIzdanja.ToString(),
        Cena.ToString(CultureInfo.InvariantCulture),
        BrojStrana.ToString(),
        string.Join(",", AutoriIds),
        IzdavacId.ToString()
    };

        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Isbn = values[1];
        Naziv = values[2];
        Zanr = values[3];
        GodinaIzdanja = int.Parse(values[4]);
        Cena = decimal.Parse(values[5], CultureInfo.InvariantCulture);
        BrojStrana = int.Parse(values[6]);
        IzdavacId = int.Parse(values[8]);

        AutoriIds = new List<int>();

        if (!string.IsNullOrEmpty(values[7]))
        {
            foreach (string id in values[7].Split(','))
            {
                AutoriIds.Add(int.Parse(id));
            }
        }
    }

    public override string ToString()
    {
        return $"ID: {Id} | ISBN: {Isbn} | {Naziv} | {Zanr} | " +
               $"Godina: {GodinaIzdanja} | Cena: {Cena:0.00} | " +
               $"Strana: {BrojStrana} | " +
               $"Autori: {string.Join(", ", AutoriIds)} | " +
               $"Izdavac: {IzdavacId}";
    }
}
