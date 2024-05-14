using System.Text.Json;

namespace JsonConfigManager;

public class JsonConfigManager<TConfig> where TConfig : class
{
    public string FilePath { get; }
    public TConfig? Config
    {
        get
        {
            try
            {
                var jsonConfig = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<TConfig>(jsonConfig);
            }
            catch
            {
                return null;
            }
        }
    }

    public JsonConfigManager(string? filePath = null)
    {
        if (filePath is null) FilePath = "Config.json";
        else FilePath = filePath;
    }

    public void Save(TConfig config)
    {
        var jsonConfig = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(FilePath, jsonConfig);
    }
}
