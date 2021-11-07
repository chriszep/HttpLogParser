using System;
using System.Net;

namespace HttpLogParser
{
    public static class RequestParser
    {
        public static Request Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            var segments = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (segments.Length < 7)
            {
                throw new FormatException("Request has invalid format");
            }

            return new Request
            {
                // Use IPAddress.Parse to ensure IP address is valid
                IPAddress = IPAddress.Parse(segments[0]).ToString(),
                Url = segments[6]
            };
        }
    }
}
