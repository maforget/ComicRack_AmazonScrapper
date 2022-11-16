using System;
using System.Runtime.Serialization;

namespace BetterINI
{
	[Serializable]
	internal class MissingIniParamException : Exception
	{
		public string ParamName { get; }

		public MissingIniParamException()
		{
		}

		public MissingIniParamException(string iniParamName) : base($"INI file parameter '{iniParamName}' not found")
		{
			ParamName = iniParamName;
		}

		public MissingIniParamException(string iniParamName, Exception innerException) : base($"INI file parameter '{iniParamName}' not found", innerException)
		{
			ParamName = iniParamName;
		}

		protected MissingIniParamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}