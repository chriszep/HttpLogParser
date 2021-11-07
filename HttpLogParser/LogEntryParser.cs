using System;
using System.Net;

namespace HttpLogParser
{
    public static class LogEntryParser
    {
        public static LogEntry Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            var segments = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (segments.Length < 7)
            {
                throw new FormatException("Log entry has invalid format");
            }

            return new LogEntry
            {
                // Use IPAddress.Parse to ensure IP address is valid
                IPAddress = IPAddress.Parse(segments[0]).ToString(),
                Url = segments[6]
            };
        }
    }
}
