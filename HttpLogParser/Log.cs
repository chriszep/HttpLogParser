using System;
using System.Collections.Generic;

namespace HttpLogParser
{
    public class Log
    {
        private readonly IEnumerable<LogEntry> _logEntries;

        public Log(IEnumerable<LogEntry> logEntries)
        {            
            _logEntries = logEntries ?? throw new ArgumentNullException(nameof(logEntries));
        }

        public Dictionary<string, int> CountRequestsByIPAddress()
        {
            var requestsByIPAddress = new Dictionary<string, int>();

            foreach (var entry in _logEntries)
            {
                if (requestsByIPAddress.ContainsKey(entry.IPAddress))
                {
                    // This IP address has had a request before
                    requestsByIPAddress[entry.IPAddress] ++;
                }
                else
                {
                    // First request for this IP address
                    requestsByIPAddress.Add(entry.IPAddress, 1);
                }
            }

            return requestsByIPAddress;
        }

        public Dictionary<string, int> CountRequestsByUrl()
        {
            var requestsByUrl = new Dictionary<string, int>();

            foreach (var entry in _logEntries)
            {
                if (requestsByUrl.ContainsKey(entry.Url))
                {
                    // This URL has had a request before
                    requestsByUrl[entry.Url]++;
                }
                else
                {
                    // First request for this URL
                    requestsByUrl.Add(entry.Url, 1);
                }
            }

            return requestsByUrl;
        }
    }
}
