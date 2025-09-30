using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScrapper.Tools
{
    public static class Reflections
    {
		public static object GetPropertyValue(this Type type, object obj, string PropertyName)
		{
			try
			{
				PropertyInfo property = type.GetProperty(PropertyName);
				return property.GetValue(obj);
			}
			catch (Exception e)
			{
				SimpleLogger.Debug($"Exception: {e.Message}. PropertyName: {PropertyName}");
				return null;
			}
		}

		public static void SetPropertyValue(this object obj, string PropertyName, object value)
		{
			try
			{
				Type type = obj.GetType();
				PropertyInfo property = type.GetProperty(PropertyName);
				property.SetValue(obj, value);
			}
			catch (Exception e)
			{
				SimpleLogger.Error($"Exception: {e.Message}. PropertyName: {PropertyName}");
			}
		}

		public static object GetDefault(this PropertyInfo pi)
		{
			var def = pi.GetCustomAttribute<DefaultValueAttribute>();
			return def != null ? def.Value :
				pi.PropertyType.IsValueType ? Activator.CreateInstance(pi.PropertyType) : null;
		}

		public static T GetStatic<T>(this Type type, [CallerMemberName] string PropertyName = "")
		{
			try
			{
				PropertyInfo property = type.GetProperty(PropertyName, BindingFlags.Static | BindingFlags.Public);
				var method = property?.GetGetMethod() ?? default;
				var ret = method.Invoke(null, null);
				return (T)Convert.ChangeType(ret, typeof(T));
			}
			catch (Exception e)
			{
				SimpleLogger.Error($"Exception: {e.Message}. PropertyName: {nameof(type)}");
				return default(T);
			}
		}

		public static T GetField<T>(this Type type, [CallerMemberName] string FieldName = "")
		{
			FieldInfo field = type.GetField(FieldName, BindingFlags.Static | BindingFlags.Public);
			var ret = field.GetValue(null) ?? default;
			return (T)Convert.ChangeType(ret, typeof(T));
		}

		public static T Get<T>(this object sourceObject, string property)
		{
			try
			{
				var type = sourceObject.GetType();
				var ret = type.GetPropertyValue(sourceObject, property);
				return (T)Convert.ChangeType(ret, typeof(T));
			}
			catch (Exception e)
			{
				SimpleLogger.Error($"Exception: {e.Message}. PropertyName: {property}");
				return default(T);
			}
		}

		public static T InvokeStaticMethod<T>(this Type type, [CallerMemberName] string Method = "", object[] param = null)
		{
			try
			{
				var ret = type.GetMethod(Method)?.Invoke(null, param);
				return (T)Convert.ChangeType(ret, typeof(T));
			}
			catch (Exception e)
			{
				SimpleLogger.Error($"Exception: {e.Message}");
				return default;
			}
		}

		public static void InvokeStaticMethod(this Type type, object[] param = null, [CallerMemberName] string Method = "")
		{
			try
			{
				var ret = type.GetMethod(Method)?.Invoke(null, param);
			}
			catch (Exception e)
			{
				SimpleLogger.Error($"Exception: {e.Message}");
			}
		}

		public static object InvokeMethod(this object sourceObject, object[] param = null, [CallerMemberName] string Method = "")
		{
			try
			{
				return sourceObject.GetType().GetMethod(Method).Invoke(sourceObject, param);
			}
			catch (Exception e)
			{
				SimpleLogger.Error($"Exception: {e.Message}");
				return null;
			}
		}
	}
}
