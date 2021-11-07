using System;
using System.IO;
using System.Threading.Tasks;

namespace HttpLogParser
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Starting...");

            const string filePath = "programming-task-example-data.log";

            var logLines = await ReadLogFileLines(filePath);

            foreach (var line in logLines)
            {
                var logEntry = LogEntryParser.Parse(line);

                Console.WriteLine();
                Console.WriteLine($"IP address: {logEntry.IPAddress}");
                Console.WriteLine($"URL: {logEntry.Url}");
            }
        }

        private static async Task<string[]> ReadLogFileLines(string logFilePath)
        {
            try
            {
                var logLines = await File.ReadAllLinesAsync(logFilePath);
                Console.WriteLine($"Read log file '{logFilePath}'");
                return logLines;
            }
            catch
            {
                Console.Error.WriteLine($"Error reading log file: {logFilePath}");
                return new string[0];
            }
        }
    }
}
