using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using AmazonScrapper.Tools;

namespace AmazonScrapper.ComicRack
{
    public class ObjectBase
    {
        public readonly object Object;

        public ObjectBase(object Object)
        {
            this.Object = Object;
        }

        public object InvokeMethod(string Method, params object[] param) => Object.InvokeMethod(param, Method);
        public IEnumerable<T> InvokeMethod<T>(string Method, params object[] param) where T : class
        {
            var enumerable = Object.InvokeMethod(param, Method) as IEnumerable;
            List<T> list = new List<T>();
            foreach (var obj in enumerable)
            {
                if (obj != null)
                    list.Add((T)Activator.CreateInstance(typeof(T), obj)); 
            }
            return list;
        }

        public virtual T GetValue<T>([CallerMemberName] string property = "") => Object.Get<T>(property);

        public virtual void SetValue(string property, object value) => Object.SetPropertyValue(property, value);

        public virtual void AppendValue(string property, object value, bool NewLine = false)
        {
            string existingValue = GetValue<string>(property);
            object newValue = CheckNewLine(value, NewLine, existingValue);
            SetValue(property, newValue);
        }

        public static object CheckNewLine(object value, bool NewLine, string existingValue)
        {
            string newLine = NewLine && !string.IsNullOrEmpty(existingValue) ? Environment.NewLine + Environment.NewLine : " ";
            return value is string ? $"{existingValue}{newLine}{(string)value}".TrimStart() : value;
        }
    }
}