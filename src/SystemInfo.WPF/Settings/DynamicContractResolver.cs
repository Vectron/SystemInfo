using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemInfo.WPF.Extensions;

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
            var filledObject = serviceProvider.GetOptions(type);

            return base.CreateProperties(type, memberSerialization)
                .Where(p =>
                {
                    var propertyInfo = type.GetProperty(p.PropertyName);
                    var defaultValue = propertyInfo.GetValue(defaultSettings);
                    var filledValue = propertyInfo.GetValue(filledObject);

                    if (defaultValue == null)
                    {
                        return true;
                    }

                    return !defaultValue.Equals(filledValue);
                })
                .ToList();
        }
    }
}