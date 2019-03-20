using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemInfo.WPF.Settings
{
    internal class DynamicContractResolver : DefaultContractResolver
    {
        private readonly IServiceProvider serviceProvider;

        public DynamicContractResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

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