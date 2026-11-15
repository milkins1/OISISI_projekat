namespace Oisisi.Core.Models;

public class Izdavac
{
    public int Id { get; set; }
    public string Sifra { get; set; } = string.Empty;
    public string Naziv { get; set; } = string.Empty;
    public int? SefAutorId { get; set; }
    public List<int> AutoriIds { get; set; } = new List<int>();
    public List<int> KnjigeIds { get; set; } = new List<int>();
}
