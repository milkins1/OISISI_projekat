namespace Oisisi.Core.Models;

public class Kupovina
{
    public int Id { get; set; }
    public int PosetilacId { get; set; }
    public int KnjigaId { get; set; }
    public DateTime DatumKupovine { get; set; }
    public int Ocena { get; set; }
    public string Komentar { get; set; } = string.Empty;
}
