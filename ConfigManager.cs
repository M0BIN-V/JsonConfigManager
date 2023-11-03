using Newtonsoft.Json;

namespace JsonConfigManager
{
    public class ConfigManager<TConfig>
    {
        public string? FilePaht { get => _filePath; }
        readonly string _filePath;
        public TConfig? Config { get => _config; }
        TConfig? _config;

        public ConfigManager(string? filePath = null)
        {
            if (filePath is null) _filePath = "Config.json";
            else _filePath = filePath;

            _config = (TConfig)Activator.CreateInstance(typeof(TConfig))!;

            if (File.Exists(_filePath))
                LoadConfiguration();
            else
                SaveConfiguration();
        }

        public void Save()
        {
            var jsonConfig = JsonConvert
                            .SerializeObject(_config, Formatting.Indented);

            File.WriteAllText(_filePath, jsonConfig);
        }

        private void LoadConfiguration()
        {
            var jsonConfig = File.ReadAllText(_filePath!);

            _config = JsonConvert.DeserializeObject<TConfig>(jsonConfig);
        }
    }
}
