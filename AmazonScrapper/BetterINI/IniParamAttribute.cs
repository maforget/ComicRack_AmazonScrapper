using System;
using System.Collections.Generic;
using System.Text;

namespace BetterINI
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class IniParamAttribute : Attribute
	{
		/// <summary>
		/// The name of this INI parameter.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Is this INI parameter required?
		/// </summary>
		public bool Required { get; set; }

		/// <summary>
		/// A default value to use if this INI parameter is not set.
		/// </summary>
		public object Default { get; set; }

		/// <summary>
		/// Should blank (empty) values be counted as "set"?
		/// </summary>
		public bool AllowBlanks { get; set; }


		public IniParamAttribute() { }

		public IniParamAttribute(string name)
		{
			this.Name = name;
		}
	}
}
