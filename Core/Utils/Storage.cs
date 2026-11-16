using Oisisi.Core.Interfaces;

namespace Oisisi.Core.Utils;

public class Storage<T> where T : ISerializable, new()
{
    private readonly string _filePath;
    private readonly Serializer<T> _serializer = new();

    public Storage(string fileName)
    {
        _filePath = Path.Combine("..", "..", "..", "Data", fileName);
    }

    public List<T> Load()
    {
        string? directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if(!File.Exists(_filePath))
        {
            FileStream fs = File.Create(_filePath);
            fs.Close();
        }

        IEnumerable<string> lines = File.ReadLines(_filePath);
        List<T> objects = _serializer.FromCSV(lines);

        return objects;
    }

    public void Save(List<T> objects)
    {
        string serializedObjects = _serializer.ToCSV(objects);
        using (StreamWriter sw = new StreamWriter(serializedObjects))
        {
            sw.Write(serializedObjects);
        }
    }
}