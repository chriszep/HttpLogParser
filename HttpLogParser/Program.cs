using System;
using System.Collections.Generic;
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

            var requests = new List<Request>();

            foreach (var line in logLines)
            {
                requests.Add(RequestParser.Parse(line));
            }

            var log = new Log(requests);
            WriteReport(log);
        }

        private static async Task<string[]> ReadLogFileLines(string logFilePath)
        {
            try
            {
                var logLines = await File.ReadAllLinesAsync(logFilePath);
                Console.WriteLine($"Read log file '{logFilePath}'");
                Console.WriteLine();
                return logLines;
            }
            catch
            {
                Console.Error.WriteLine($"Error reading log file: {logFilePath}");
                return new string[0];
            }
        }

        private static void WriteReport(Log log)
        {
            var reporter = new Reporter(log);

            Console.WriteLine($"Number of unique IP addresses: {reporter.CountUniqueIPAddresses()}");
            Console.WriteLine();

            Console.WriteLine($"Top 3 most visited URLs: {string.Join(", ", reporter.GetTopThreeUrls())}");
            Console.WriteLine();

            Console.WriteLine($"Top 3 most active IP addresses: {string.Join(", ", reporter.GetTopThreeIPAddresses())}");
        }
    }
}
