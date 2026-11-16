using System.Text;
using Oisisi.Core.Interfaces;

namespace Oisisi.Core.Utils;

class Serializer<T> where T : ISerializable, new()
{
    private const char Delimeter = '|';
    public string ToCSV(List<T> objects)
    {
        StringBuilder sb = new StringBuilder();

        foreach (ISerializable obj in objects) 
        {
            string line = string.Join(Delimeter.ToString(), obj.ToCSV());
            sb.AppendLine(line);
        }
        return sb.ToString();
    }

    public List<T> FromCSV(IEnumerable<string> lines)
    {
        List<T> objects = new List<T>();

        foreach(string line in lines)
        {
            string[] csvValues = line.Split(Delimeter);
            T obj = new T();
            obj.FromCSV(csvValues);
            objects.Add(obj);
        }
        return objects;
    }
}
