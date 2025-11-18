using System;
using System.IO;

namespace Hotel.Common
{
    /// <summary>
    /// Simple rolling-file logger for demo use. Writes logs to <app base>\Logs\log_yyyyMMdd.txt
    /// </summary>
    public static class Logger
    {
        private static readonly string LogFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? ".", "Logs");

        public static void LogException(Exception ex)
        {
            try
            {
                Directory.CreateDirectory(LogFolder);
                var filename = Path.Combine(LogFolder, $"log_{DateTime.UtcNow:yyyyMMdd}.txt");
                File.AppendAllText(filename, $"{DateTime.UtcNow:O} | ERROR | {ex}\r\n");
            }
            catch
            {
                // swallow - logging must not throw
            }
        }

        public static void LogInfo(string message)
        {
            try
            {
                Directory.CreateDirectory(LogFolder);
                var filename = Path.Combine(LogFolder, $"log_{DateTime.UtcNow:yyyyMMdd}.txt");
                File.AppendAllText(filename, $"{DateTime.UtcNow:O} | INFO | {message}\r\n");
            }
            catch { }
        }
    }
}
