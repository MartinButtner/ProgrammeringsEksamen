using System.Text.Json;

namespace Eksamenprogrammering3_2025.Strategier;

public class Configurer
{
    private readonly string _filePath;

    public Configurer(string filePath)
    {
        _filePath = filePath;
    }

    public void SaveConfiguration(List<IntervalConfiguration> intervals)
    {
        var json = JsonSerializer.Serialize(intervals, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public List<IntervalConfiguration> LoadConfiguration()
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException("Konfigurationsfilen findes ikke.");
        }

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<IntervalConfiguration>>(json);
    }
}