using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BetterINI
{
    /// <summary>
    /// Represents a Key=Value style configuration file in memory.
    /// <para />
    /// See <see cref="Parse"/>.
    /// </summary>
    public class IniFile
    {
        private readonly Dictionary<string, IniValue> data;

        /// <summary>
        /// Get a value from this IniFile.
        /// </summary>
        /// <param name="key">The name of the key.</param>
        /// <exception cref="KeyNotFoundException" />
        public IniValue this[string key]
        {
            get
            {
                if (KeysCaseInsensitive)
                    key = key.ToLowerInvariant();

                if (!KeyExists(key))
                    throw new KeyNotFoundException($"Key {key} not found in IniFile");

                return data[key];
            }
        }

        /// <summary>
        /// The keys in the IniFile.
        /// </summary>
        public Dictionary<string, IniValue>.KeyCollection Keys => data.Keys;

        /// <summary>
        /// Are the keys in this case-insensitive?
        /// </summary>
        public bool KeysCaseInsensitive { get; }

        /// <summary>
        /// The key / value separator character
        /// </summary>
        public char KeyValueSeparator { get; }

        /// <summary>
        /// Initialize a new, empty IniFile instance.
        /// </summary>
        /// <param name="keysCaseInsensitive">Should key names be treated as case insensitive?</param>
        /// <param name="keyValueSeparator">The key / value pair separator character.</param>
        public IniFile(bool keysCaseInsensitive = false, char keyValueSeparator = '=')
        {
            KeysCaseInsensitive = keysCaseInsensitive;
            KeyValueSeparator = keyValueSeparator;

            this.data = new Dictionary<string, IniValue>();
        }

        /// <summary>
        /// Read an INI file from disk and parse it.
        /// </summary>
        /// <param name="fileName">The full path to the ini file.</param>
        /// <returns>An ini file object.</returns>
        public static IniFile ParseFromDisk(string fileName, char keyValSep = '=', bool emptyIsUnset = false, bool keysCaseSensitive = false)
        {
            return Parse(File.ReadAllText(fileName), keyValSep, emptyIsUnset, keysCaseSensitive);
        }

        /// <summary>
        /// Parse INI key=value pairs from the provided string.
        /// </summary>
        /// <param name="data">The raw data.</param>
        /// <param name="keyValSep">The key / value separator character.</param>
        /// <param name="emptyIsUnset">Should empty values be treated as unset (and not added to the file)?</param>
        /// <param name="keysCaseSensitive">Should key names be treated as case insensitive?</param>
        /// <exception cref="ArgumentNullException" />
        public static IniFile Parse(string data, char keyValSep = '=', bool emptyIsUnset = false, bool keysCaseSensitive = false)
        {
            IniFile f = new IniFile(keysCaseSensitive, keyValSep);

            StringReader sr = new StringReader(data);
            string section = string.Empty;

            while (true)
            {
                string line = sr.ReadLine();
                if (line == null) break;

                if (ParseLine(line, keyValSep, out string key, out string value, out bool isSection))
                {
                    if (emptyIsUnset && value.Length == 0)
                        continue;

                    if (isSection)
                        section = key;
                    else
                        f.Add(key, new IniValue(value, section));
                }
            }

            return f;
        }

        /// <summary>
        /// Parse an INI file out of a stream. Will return when the end of stream is reached. The stream will not be disposed.
        /// </summary>
        /// <param name="stream">A stream to read the INI data from.</param>
        /// <returns></returns>
        public static async Task<IniFile> ParseAsync(Stream stream, char keyValSep = '=', bool emptyIsUnset = false, bool keysCaseSensitive = false)
        {
            StreamReader reader = new StreamReader(stream);
            IniFile ini = new IniFile(keysCaseSensitive, keyValSep);
            string section = string.Empty;

            while (true)
            {
                string line = await reader.ReadLineAsync();
                if (line == null) break;

                if (ParseLine(line, keyValSep, out string key, out string value, out bool isSection))
                {
                    if (emptyIsUnset && value.Length == 0)
                        continue;

                    if (isSection)
                        section = key;
                    else
                        ini.Add(key, new IniValue(value, section));
                }
            }

            return ini;
        }

        /// <summary>
        /// Parse an INI file out of a stream. Will return when the end of stream is reached. The stream will not be disposed.
        /// </summary>
        /// <param name="stream">A stream to read the INI data from.</param>
        public static IniFile Parse(Stream stream, char keyValSep = '=', bool emptyIsUnset = false, bool keysCaseSensitive = false)
        {
            return ParseAsync(stream, keyValSep, emptyIsUnset, keysCaseSensitive).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Combine a set of IniFiles into one. Keys with null values are overwritten with non-null values from other files if possible.
        /// If multiple keys with the same name exist, then the first found value will be used.
        /// </summary>
        /// <param name="files">A list of files to combine.</param>
        public static IniFile Combine(params IniFile[] files)
        {
            IniFile result = new IniFile();

            foreach (IniFile ini in files)
            {
                foreach (string key in ini.Keys)
                {
                    if (!result.IsSet(key) && ini.IsSet(key))
                    {
                        result.Add(key, ini[key]);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Parse a single line of INI garbage into a key=value pair.
        /// </summary>
        /// <param name="line">The source line.</param>
        /// <param name="outKey">The parsed key name.</param>
        /// <param name="outValue">The parsed value.</param>
        /// <returns><see langword="true"/> if the line was valid, <see langword="false"/> if it was not.</returns>
        private static bool ParseLine(string line, char keyValSep, out string outKey, out string outValue, out bool isSection)
        {
            outKey = null;
            outValue = null;
            isSection = false;

            if (line == null)
                return false;

            line = line.Trim();

            if (line.StartsWith("[") && line.EndsWith("]") && !line.Contains(keyValSep) && !line.StartsWith("#"))
            {
                outKey = Regex.Match(line, @"\[([^][]+)\]", RegexOptions.IgnoreCase).Groups[1].Value;
                isSection = true;
                return true;
            }

            if (line.StartsWith("#") || !line.Contains(keyValSep) || line.StartsWith(keyValSep.ToString()))
                return false;

            outKey = line.Substring(0, line.IndexOf(keyValSep)).Trim();
            outValue = line.IndexOf(keyValSep) + 1 >= line.Length ? string.Empty : line.Substring(line.IndexOf(keyValSep) + 1).Trim();

            //if (Boolean.TryParse(outValue, out bool outV))
            //	return outV;

            return true;
        }

        /// <summary>
        /// Get an array of strings from a key=value pair by splitting the value by the specified character.
        /// </summary>
        /// <param name="key">The name of the key.</param>
        /// <param name="split">The character to split the value by.</param>
        /// <exception cref="KeyNotFoundException" />
        public string[] GetArray(string key, char split)
        {
            return this[key].Value.Split(split).Select(x => x.Trim()).ToArray();
        }

        /// <summary>
        /// Like <see cref="GetArray(string, char)"/>, but will return an empty string array if the key does not exist, instead of throwing an exception.
        /// </summary>
        /// <param name="key">The name of the key.</param>
        /// <param name="split">The character to split the value by.</param>
        public string[] SafeGetArray(string key, char split)
        {
            if (!IsSet(key))
                return new string[] { };

            return GetArray(key, split);
        }

        /// <summary>
        /// Appends or overwrites a key/value pair in the internal dictionary.
        /// </summary>
        /// <param name="key">The name of the key. This is case-sensitive.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentNullException" />
        public void Add(string key, IniValue value)
        {
            if (KeysCaseInsensitive)
                key = key.ToLowerInvariant();

            if (data.ContainsKey(key))
                data[key] = value;
            else
                data.Add(key, value);
        }

        /// <summary>
        /// Gets a value from this IniFile by name <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The name of the key.</param>
        /// <returns>The value found.</returns>
        /// <exception cref="KeyNotFoundException" />
        public IniValue Get(string key)
        {
            return this[key];
        }

        /// <summary>
        /// Attempt to add or overwrite a key/value pair in the IniFile. Will not throw if key or value is null.
        /// </summary>
        /// <param name="key">The name of the key.</param>
        /// <param name="value">The value.</param>
        public void SafeAdd(string key, IniValue value)
        {
            if (key == null || value == null)
                return;

            Add(key, value);
        }

        /// <summary>
        /// Get an integer value from the configuration data. Will throw <see cref="KeyNotFoundException"/> if the key does not exist.
        /// </summary>
        /// <param name="key">The name of the value.</param>
        /// <exception cref="KeyNotFoundException" />
        /// <exception cref="FormatException" />
        /// <exception cref="OverflowException" />
        /// <exception cref="ArgumentNullException" />
        public int GetInt(string key)
        {
            if (!IsSet(key))
                throw new KeyNotFoundException($"Key {key} not found in IniFile");

            return int.Parse(this[key].Value);
        }

        /// <summary>
        /// Get an integer value from the configuration data. Will return zero (0) or the specified default if the key does not exist.
        /// </summary>
        /// <param name="key">The name of the key.</param>
        public int SafeGetInt(string key, int defaultValue = 0)
        {
            if (!IsSet(key))
                return defaultValue;

            if (int.TryParse(this[key].Value, out int result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Try to get a value. Returns null or <paramref name="defaultValue"/> if the key is not found.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">A default fallback value to use in case the specified key does not exist.</param>
        public string SafeGet(string key, string defaultValue = null)
        {
            if (IsSet(key))
                return this[key].Value;

            return defaultValue;
        }

        /// <summary>
        /// Parse an Enum from the configuration data. The param <paramref name="result"/> will be null on failure.
        /// </summary>
        /// <param name="enumType">The type of the enum to parse.</param>
        /// <param name="key">The name of the value in the configuration file.</param>
        public bool TryGetEnum<TEnum>(string key, out TEnum? result) where TEnum : struct
        {
            if (IsSet(key))
            {
                if (Enum.TryParse<TEnum>(this[key].Value, out TEnum value))
                {
                    result = value;
                    return true;
                }
            }

            result = null;
            return false;
        }

        /// <summary>
        /// Parse an Enum of type <typeparamref name="TEnum"/> from the configuration data. If no <paramref name="defaultValue"/> is provided
        /// and the enum value could not be parsed, this method will throw <see cref="ArgumentException"/>.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum to parse.</typeparam>
        /// <param name="key">The name of the value in the configuration file.</param>
        /// <param name="defaultValue">Some default enum value, if any. Will return this on parse failure if set, or throw an exception otherwise.</param>
        /// <returns>The parsed enum value, or <paramref name="defaultValue"/> if it is provided.</returns>
        /// <exception cref="ArgumentException" />
        public TEnum GetEnum<TEnum>(string key, TEnum? defaultValue = null) where TEnum : struct
        {
            if (TryGetEnum<TEnum>(key, out TEnum? result))
                return result.Value;

            if (defaultValue == null && result == null)
                throw new ArgumentException("Could not parse the value of provided key as provided enum type.");

            return defaultValue.Value;
        }

        /// <summary>
        /// Get a boolean value. Returns <paramref name="defaultValue"/> if the key is not found.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value to return if the key is not found.</param>
        /// <returns></returns>
        public bool SafeGetBool(string key, bool defaultValue = false)
        {
            if (!IsSet(key))
                return defaultValue;

            return GetBool(key);
        }

        /// <summary>
        /// Get a bool. Will throw <see cref="KeyNotFoundException"/> if the key is not found.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool GetBool(string key)
        {
            if (!IsSet(key))
                throw new KeyNotFoundException($"Key {key} not found in IniFile");

            return this[key].Value.ToLower() == "true";
        }

        /// <summary>
        /// Return true if all the specified key names exist in this configuration file.
        /// </summary>
        /// <param name="keys">A list of key names.</param>
        public bool DoAllExist(params string[] keys)
        {
            foreach (string k in keys)
            {
                if (!IsSet(k))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns this IniFile as a string.
        /// </summary>
        public override string ToString()
        {
            return ToString(false);
        }

        /// <summary>
        /// Returns this IniFile as a string.
        /// </summary>
        /// <param name="align">Align the value separators to the longest key length, for visual goodness.</param>
        public string ToString(bool align = false)
        {
            StringBuilder sb = new StringBuilder();

            int longest = -1;

            // i hate myself for this line of code
            if (align) foreach (string key in data.Keys) if (key.Length > longest) longest = key.Length;

            string oldSection = string.Empty;
            foreach (string key in data.Keys)
            {
                string section = data[key].Section;
                if (!string.IsNullOrEmpty(section) && section != oldSection)
                {
                    oldSection = section;
                    sb.AppendLine($"[{data[key].Section}]");
                }

                string k = align ? key.PadRight(longest) : key;
                sb.AppendLine($"{k} {KeyValueSeparator} {data[key].Value}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns <see langword="true"/> if the specified key exists.
        /// </summary>
        /// <param name="key">The name of the key. This is case-sensitive.</param>
        /// <returns>Returns true if the key exists.</returns>
        public bool KeyExists(string key) => data != null && data.ContainsKey(key);

        /// <summary>
        /// Returns true if the specified key exists and its associated value is not null, empty, or whitespace.
        /// </summary>
        /// <param name="key">The name of the key.</param>
        public bool IsSet(string key) => KeyExists(key) && !string.IsNullOrWhiteSpace(this[key].Value);

        /// <summary>
        /// Throw an exception if the specified key is not set, null, empty, or whitespace.
        /// </summary>
        /// <param name="key">The key</param>
        public void Assert(string key)
        {
            if (!IsSet(key))
                throw new Exception($"The key \"{key}\" was not set.");
        }
    }
}
