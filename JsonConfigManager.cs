using Newtonsoft.Json;

namespace JsonConfigManager
{
    public class JsonConfigManager<TConfig>
    {
        public string FilePath { get; }
        public TConfig? Config
        {
            get
            {
                var jsonConfig = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<TConfig>(jsonConfig);
            }
        }

        public JsonConfigManager(string? filePath = null)
        {
            if (filePath is null) FilePath = "Config.json";
            else FilePath = filePath;
        }

        public void Save(TConfig config)
        {
            var jsonConfig = JsonConvert.SerializeObject(config, Formatting.Indented);

            File.WriteAllText(FilePath, jsonConfig);
        }
    }
}
