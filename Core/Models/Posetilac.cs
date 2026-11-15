namespace Oisisi.Core.Models;

public class Posetilac
{
    public int Id { get; set; }
    public string Ime { get; set; } = string.Empty;
    public string Prezime { get; set; } = string.Empty;
    public DateTime DatumRodjenja {  get; set; }
    public int AdresaId { get; set; }
    public string KontaktTelefon { get; set; } = string.Empty;
    public string Email {  get; set; } = string.Empty;
    public string BrojClanskeKarte { get; set; } = string.Empty;
    public int TrenutnaGodinaClanstva { get; set; }
    public Status Status { get; set; }
    public List<int> ListaZeljaKnjigaIds { get; set; } = new List<int>();
}
