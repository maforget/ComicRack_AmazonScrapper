using System;
using System.Collections.Generic;
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

        public static object GetDefault(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
