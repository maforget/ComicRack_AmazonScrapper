using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScrapper.Tools
{
    public static class Reflections
    {
        public static object GetPropertyValue(this Type type, object obj, string PropertyName)
        {
            PropertyInfo property = type.GetProperty(PropertyName);
            return property.GetValue(obj);
        }

        public static void SetPropertyValue(this Type type, object obj, string PropertyName, object value)
        {
            PropertyInfo property = type.GetProperty(PropertyName);
            property.SetValue(obj, value);
        }

        public static object GetDefault(this PropertyInfo pi)
        {
            var def = pi.GetCustomAttribute<DefaultValueAttribute>();
            return def != null ? def.Value : 
                pi.PropertyType.IsValueType ? Activator.CreateInstance(pi.PropertyType) : null;
        }
    }
}
