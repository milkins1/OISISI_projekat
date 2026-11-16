namespace Oisisi.Core.Interfaces;

public interface ISerializable
{
    string[] ToCSV();
    void FromCSV(string[] values);
}
