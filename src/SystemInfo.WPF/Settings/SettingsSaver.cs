using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SystemInfo.WPF.Extensions;

namespace SystemInfo.WPF.Settings;

/// <summary>
/// Implementation of the <see cref="ISettingsSaver"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SettingsSaver"/> class.
/// </remarks>
/// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
/// <param name="serviceDescriptors">The <see cref="IServiceCollection"/>.</param>
public class SettingsSaver(IServiceProvider serviceProvider, IServiceCollection serviceDescriptors) : ISettingsSaver
{
    private static JsonSerializerSettings SerializerSettings
        => new()
        {
            ContractResolver = new DynamicContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            Formatting = Formatting.Indented,
        };

    private IEnumerable<(string Name, string Json)> GetSettingJson
        => serviceDescriptors
        .Where(x => x.ServiceType.IsGenericType && x.ServiceType.GetGenericTypeDefinition() == typeof(IConfigureOptions<>))
        .AsParallel()
        .Select(x =>
        {
            var genericType = x.ServiceType.GenericTypeArguments[0];
            var filledSettings = serviceProvider.GetOptions(genericType);
            var json = string.Empty;
            if (filledSettings == null)
            {
                return (genericType.Name, json);
            }

            json = JsonConvert
                .SerializeObject(filledSettings, SerializerSettings)
                .Replace(Environment.NewLine, Environment.NewLine + "  ", StringComparison.Ordinal);

            return (genericType.Name, json);
        })
        .Where(x => x.json.Length > 2);

    /// <inheritdoc/>
    public void SaveConfiguration()
    {
        // open the writer when we actually have something to write
        if (!GetSettingJson.Any())
        {
            return;
        }

        var configFile = SettingsHelper.GetSettingsFilePath();
        SettingsHelper.CreateIfNotExist(configFile);
        using var sw = new StreamWriter(configFile);
        using var writer = new JsonTextWriter(sw) { AutoCompleteOnClose = true, Formatting = Formatting.Indented };
        writer.WriteStartObject();

        foreach (var (name, json) in GetSettingJson)
        {
            writer.WritePropertyName(name);
            writer.WriteRawValue(json);
        }
    }
}
