using Newtonsoft.Json;

namespace JsonConfigManager
{
    public class ConfigManager<TConfig>
    {
        public string FilePath { get; }
        public TConfig? Config { get => _config; }
        TConfig? _config;

        public ConfigManager(string? filePath = null)
        {
            if (filePath is null) FilePath = "Config.json";
            else FilePath = filePath;

            _config = (TConfig)Activator.CreateInstance(typeof(TConfig))!;

            if (File.Exists(FilePath))
                LoadConfiguration();
            else
                Save();
        }

        public void Save()
        {
            var jsonConfig = JsonConvert
                            .SerializeObject(_config, Formatting.Indented);

            File.WriteAllText(FilePath, jsonConfig);
        }

        private void LoadConfiguration()
        {
            var jsonConfig = File.ReadAllText(FilePath!);

            _config = JsonConvert.DeserializeObject<TConfig>(jsonConfig);
        }
    }
}
