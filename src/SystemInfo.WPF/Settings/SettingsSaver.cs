using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using SystemInfo.WPF.Extensions;

namespace SystemInfo.WPF.Settings
{
    public class SettingsSaver : ISettingsSaver
    {
        private readonly IServiceCollection serviceDescriptors;
        private readonly IServiceProvider serviceProvider;

        public SettingsSaver(IServiceProvider serviceProvider, IServiceCollection serviceDescriptors)
        {
            this.serviceProvider = serviceProvider;
            this.serviceDescriptors = serviceDescriptors;
        }

        public void SaveConfiguration()
        {
            var serializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DynamicContractResolver(serviceProvider),
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            var generics = serviceDescriptors
                .Where(x => x.ServiceType.IsGenericType)
                .Where(x => x.ServiceType.GetGenericTypeDefinition() == typeof(IConfigureOptions<>))
                //.AsParallel()
                .Select(x =>
                {
                    var genericType = x.ServiceType.GenericTypeArguments[0];
                    var filledSettings = serviceProvider.GetOptions(genericType);
                    var json = JsonConvert
                        .SerializeObject(filledSettings, serializerSettings)
                        .Replace(Environment.NewLine, Environment.NewLine + "  ");
                    return (genericType.Name, json);
                })
                .Where(x => x.Item2.Length > 2);

            StreamWriter sw = null;
            JsonTextWriter writer = null;
            try
            {
                var startWritten = false;
                foreach (var kv in generics)
                {
                    if (!startWritten)
                    {
                        // open the writer when we actually have something to write
                        sw = new StreamWriter(@"settings.json");
                        writer = new JsonTextWriter(sw) { AutoCompleteOnClose = true, Formatting = Formatting.Indented };
                        writer.WriteStartObject();
                        startWritten = true;
                    }

                    writer.WritePropertyName(kv.Item1);
                    writer.WriteRawValue(kv.Item2);
                }
            }
            finally
            {
                var disposable = writer as IDisposable;
                disposable?.Dispose();
                sw?.Dispose();
            }
        }
    }
}