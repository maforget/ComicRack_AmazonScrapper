/*
MIT License

Copyright (c) 2016 Heiswayi Nrird

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

public static class SimpleLogger
{
    /// <summary>
    /// Log a DEBUG message
    /// </summary>
    /// <param name="text">Message</param>
    public static void Debug(string text, [CallerMemberName] string callerMemberName = "")
    {
        WriteFormattedLog(LogLevel.DEBUG, text, callerMemberName);
    }

    /// <summary>
    /// Log an ERROR message
    /// </summary>
    /// <param name="text">Message</param>
    public static void Error(string text, [CallerMemberName] string callerMemberName = "")
    {
        WriteFormattedLog(LogLevel.ERROR, text, callerMemberName);
    }

    /// <summary>
    /// Log a FATAL ERROR message
    /// </summary>
    /// <param name="text">Message</param>
    public static void Fatal(string text, [CallerMemberName] string callerMemberName = "")
    {
        WriteFormattedLog(LogLevel.FATAL, text, callerMemberName);
    }

    /// <summary>
    /// Log an INFO message
    /// </summary>
    /// <param name="text">Message</param>
    public static void Info(string text, [CallerMemberName] string callerMemberName = "")
    {
        WriteFormattedLog(LogLevel.INFO, text, callerMemberName);
    }

    /// <summary>
    /// Log a TRACE message
    /// </summary>
    /// <param name="text">Message</param>
    public static void Trace(string text, [CallerMemberName] string callerMemberName = "")
    {
        WriteFormattedLog(LogLevel.TRACE, text, callerMemberName);
    }

    /// <summary>
    /// Log a WARNING message
    /// </summary>
    /// <param name="text">Message</param>
    public static void Warning(string text, [CallerMemberName] string callerMemberName = "")
    {
        WriteFormattedLog(LogLevel.WARNING, text, callerMemberName);
    }

    public static void DividingLine()
    {
        WriteLine(new String('-', 200), true);
    }

    private static string GetFilePath()
    {
        string FILE_EXT = ".log";
        Assembly assembly = Assembly.GetExecutingAssembly();
        string logFilename = Path.Combine(Path.GetDirectoryName(assembly.Location), assembly.GetName().Name + FILE_EXT);
        return logFilename;
    }

    private static void WriteLine(string text, bool append = false)
    {
        string logFilename = GetFilePath();
        object fileLock = new object();

        try
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            lock (fileLock)
            {
                using (StreamWriter writer = new StreamWriter(logFilename, append, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine(text);
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private static void WriteFormattedLog(LogLevel level, string text, string callerMemberName)
    {
        //var config = Config.ReadUserFromFile();
        //int maxLevel = -1;
        //if (Enum.TryParse(config.LogLevel, true, out LogLevel logLevel))
        //    maxLevel = (int) logLevel;

        string datetimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
        string caller = $"[{callerMemberName}]".PadRight(25);
        string pretext;
        switch (level)
        {
            case LogLevel.TRACE:
                pretext = DateTime.Now.ToString(datetimeFormat) + " [TRACE]   " + caller;
                break;
            case LogLevel.INFO:
                pretext = DateTime.Now.ToString(datetimeFormat) + " [INFO]    " + caller;
                break;
            case LogLevel.DEBUG:
                pretext = DateTime.Now.ToString(datetimeFormat) + " [DEBUG]   " + caller;
                break;
            case LogLevel.WARNING:
                pretext = DateTime.Now.ToString(datetimeFormat) + " [WARNING] " + caller;
                break;
            case LogLevel.ERROR:
                pretext = DateTime.Now.ToString(datetimeFormat) + " [ERROR]   " + caller;
                break;
            case LogLevel.FATAL:
                pretext = DateTime.Now.ToString(datetimeFormat) + " [FATAL]   " + caller;
                break;
            default:
                pretext = "";
                break;
        }

        //if ((int)level >= maxLevel)
        WriteLine(pretext + ParseNewLine(text), true);
    }

    private static void DeleteLog()
    {
        string logFilename = GetFilePath();
        try
        {
            File.Delete(logFilename);
        }
        catch
        {

        }
    }

    private static string ParseNewLine(string input)
    {
        if (input.Contains(Environment.NewLine))
            return input.Replace(Environment.NewLine, "\\r\\n");

        return input;
    }

    [System.Flags]
    public enum LogLevel
    {
        TRACE,
        DEBUG,
        INFO,
        WARNING,
        ERROR,
        FATAL
    }
}