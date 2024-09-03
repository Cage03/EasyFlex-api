using System.Text.Json;
using Interface.Config;
using Interface.Interface;
using Interface.Shared;

namespace EasyFlex_api.Utils;

public class ConfigLoader : IConfigLoader
{
    private readonly Config _config = JsonSerializer.Deserialize<Config>(File.ReadAllText("config.json"), JsonOptionData.Default)!;

    public T GetConfig<T>()
    {
        return typeof(T) switch
        {
            _ when typeof(T) == typeof(Config) => (T)(object)_config,
            _ => throw new Exception("Config type not found")
        };
    }
}