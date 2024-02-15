using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SystemInfo.WPF.Settings
{
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
                    var propertyInfo = type.GetProperty(p.PropertyName);
                    var defaultValue = propertyInfo.GetValue(defaultSettings);
                    p.ShouldSerialize = i =>
                    {
                        var filledValue = propertyInfo.GetValue(i);
                        return !defaultValue.Equals(filledValue);
                    };

                    return p;
                })
                .ToList();

            return properties;
        }
    }
}
