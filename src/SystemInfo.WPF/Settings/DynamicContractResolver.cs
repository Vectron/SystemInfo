using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SystemInfo.WPF.Settings;

/// <summary>
/// A <see cref="DefaultContractResolver"/> for resolving json types.
/// </summary>
internal sealed class DynamicContractResolver : DefaultContractResolver
{
    /// <inheritdoc/>
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        var defaultSettings = Activator.CreateInstance(type);
        var properties = base.CreateProperties(type, memberSerialization)
            .Select(p =>
            {
                if (p.PropertyName == null)
                {
                    return p;
                }

                var propertyInfo = type.GetProperty(p.PropertyName);
                if (propertyInfo == null)
                {
                    return p;
                }

                var defaultValue = propertyInfo.GetValue(defaultSettings);
                p.ShouldSerialize = i =>
                {
                    var filledValue = propertyInfo.GetValue(i);
                    if (defaultValue == null)
                    {
                        return filledValue != null;
                    }

                    return !defaultValue.Equals(filledValue);
                };

                return p;
            })
            .ToList();

        return properties;
    }
}
