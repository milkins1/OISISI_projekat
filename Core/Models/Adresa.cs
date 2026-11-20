using Oisisi.Core.Interfaces;

namespace Oisisi.Core.Models;


public class Adresa : ISerializable
{
    public int Id { get; set; }  //ovako povezujem adresu sa drugim entitetima
    public string Ulica { get; set; } = string.Empty;
    public string Broj { get; set; } = string.Empty; //tring jer adresa moze biti i 18A
    public string Grad { get; set; } = string.Empty;
    public string Drzava { get; set; } = string.Empty;

    public Adresa()
    {
    }

    public Adresa(string ulica, string broj, string grad, string drzava)
    {
        Ulica = ulica;
        Broj = broj;
        Grad = grad;
        Drzava = drzava;
    }


    /*
    Obrazac ToCSV/FromCSV prilagodjen prema primeru CRUDEexample
    sa 3. vezbi. CSV polja prilagodjena su ovom modelu 
    */
    
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Ulica,
            Broj,
            Grad,
            Drzava
        };

        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Ulica = values[1];
        Broj = values[2];
        Grad = values[3];
        Drzava = values[4];
    }

    public override string ToString()
    {
        return $"ID: {Id,3} | {Ulica} {Broj}, {Grad}, {Drzava}";
    }
}