using System.Text.Json;
using System.Text.Json.Serialization;

namespace Oisisi.Core.Utils;

public static class JsonStorage
{
    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public static List<T> Load<T>(string path)
    {
        if(!File.Exists(path))
        {
            return new List<T>();
        }

        string json = File.ReadAllText(path);
        if(string.IsNullOrWhiteSpace(json))
        {
            return new List<T>();
        }

        return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
    }

    public static void Save<T>(string path, List<T> items)
    {
        string? directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(items, Options);
        File.WriteAllText(path, json);
    }
}
