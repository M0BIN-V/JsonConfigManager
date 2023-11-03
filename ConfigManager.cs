using Newtonsoft.Json;
using System.Reflection;

namespace JsonConfigManager
{
    public class ConfigManager
    {
        public string? FilePaht
        {
            get => _filePath;
            init
            {
                _filePath = value;
            }
        }
        readonly string? _filePath;
        public object? Config { get => _config; }
        object? _config;

        public ConfigManager()
        {
            var inheritedType = GetInheritedType(Assembly.GetCallingAssembly());
            _config = Activator.CreateInstance(inheritedType!);

            if (File.Exists(_filePath))
                LoadConfiguration();
            else
                SaveConfiguration();
        }

        public void SaveConfiguration()
        {
            var jsonConfig = JsonConvert
                            .SerializeObject(_config, Formatting.Indented);

            File.WriteAllText(_filePath ?? "Config.json", jsonConfig);
        }

        private void LoadConfiguration()
        {
            var jsonConfig = File.ReadAllText(_filePath!);

            _config = JsonConvert.DeserializeObject<object>(jsonConfig);
        }

        private static Type? GetInheritedType(Assembly callingAssembly)
        {
            var types = callingAssembly.GetTypes();
            var inheritedType = types
                .Where(type => type
                    .IsSubclassOf(
                        typeof(ConfigTemplate))).FirstOrDefault();
            return inheritedType;
        }
    }
}
