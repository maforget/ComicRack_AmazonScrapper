using System;
using System.Collections.Generic;
using System.Text;

namespace BetterINI
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
	public sealed class IniIgnoreAttribute : Attribute
	{
	}
}
