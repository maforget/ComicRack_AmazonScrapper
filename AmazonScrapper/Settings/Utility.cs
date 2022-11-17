using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BetterINI;
using System.IO;
using System.Reflection;
using System.Windows;

namespace AmazonScrapper.Settings
{
    public static class Utility
    {
        private static string iniFile = Path.Combine(GetExecutingAssemblyDirectory(), "user.ini");

        public static string GetExecutingAssemblyDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        #region Text Replace Append
        public static string ReplaceSection(this string exisitingText, string sectionName, string newSectionText)
        {
            string newFile = string.Empty;
            string textToFind = $"[{sectionName}]";

            if (!string.IsNullOrEmpty(newSectionText))
            {
                //Get the text without the requested section
                newFile = RemoveSectionFromText(exisitingText, textToFind);
                //Append the new Section to the file
                newFile = newFile.AppendSection(newSectionText);
            }

            return newFile;
        }

        private static string RemoveSectionFromText(string exisitingText, string textToFind)
        {
            string newFile;
            StringReader sr = new StringReader(exisitingText);
            StringBuilder sb = new StringBuilder();
            bool inSection = false;
            int lineIndex = 0;

            while (true)
            {
                //We will be removing the requested section to append to the file later.
                string line = sr.ReadLine()?.Trim();
                if (line == null)
                    break;

                var emptySectionBreakLine = false;
                //We found our header
                if (line == textToFind)
                    inSection = true;
                //We are not in the section anymore, because we found an empty line
                else if (inSection && string.IsNullOrEmpty(line))
                    inSection = false;
                //We have attained the end of the section, because a new one is starting
                else if (inSection && line.StartsWith("[") && line.EndsWith("]"))
                    inSection = false;
                //Found another section just add an empty line to seperate them
                else if (line.StartsWith("[") && line.EndsWith("]"))
                    emptySectionBreakLine = true;

                //Add a line for an Empty Section Break, except for the First line
                if (emptySectionBreakLine && lineIndex > 0)
                    sb.AppendLine();

                //Add lines that aren't part of the selected section
                if (!inSection && !string.IsNullOrEmpty(line))
                    sb.AppendLine(line);

                lineIndex++;
            }

            //Get the final file without the previous section
            newFile = sb.ToString();
            return newFile;
        }

        public static string AppendSection(this string exisitingText, string newSectionText)
        {
            StringBuilder sb = new StringBuilder(exisitingText);
            sb.AppendLine();
            sb.Append(newSectionText);

            return sb.ToString();
        }
        #endregion

        #region Generic
        public static T ReadFromFile<T>() where T : class, new()
        {
            try
            {
                if (File.Exists(iniFile))
                {
                    string ini = File.ReadAllText(iniFile);
                    return IniSerializer.Deserialize<T>(ini);
                }

                return new T();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void WriteToFile<T>(this T user) where T : class, new()
        {
            try
            {
                IniFile ini = IniSerializer.Serialize(user);
                var sectionText = ini.ToString();

                var section = typeof(T).GetCustomAttribute<IniSectionAttribute>();
                if (!File.Exists(iniFile) || section == null)
                    File.WriteAllText(iniFile, ini.ToString());
                else
                {
                    var existingFile = File.ReadAllText(iniFile);
                    var newFile = existingFile.ReplaceSection(section.SectionName, ini.ToString());
                    File.WriteAllText(iniFile, newFile);
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public static Dictionary<string, UserConfig> ToDictionary<T>(this T user)
        {
            var keys = new Dictionary<string, UserConfig>();
            if (user == null)
                return null;

            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                var userParamAttribute = pi.GetCustomAttribute<UserParamAttribute>();
                var displayText = userParamAttribute == null ? pi.Name : userParamAttribute.DisplayText;
                var append = userParamAttribute == null ? null : userParamAttribute.Append;

                if (pi.PropertyType == typeof(bool))
                    keys.Add(pi.Name, new UserConfig(pi.Name, displayText, (bool)pi.GetValue(user), append));
            }

            return keys;
        }

        public static T ToConfig<T>(this Dictionary<string, UserConfig> dict) where T : new()
        {
            T user = new T();
            if (dict == null)
                return default;

            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                if (pi.PropertyType == typeof(bool))
                    pi.SetValue(user, dict[pi.Name].Enabled);
            }

            return user;
        }
        #endregion
    }
}
