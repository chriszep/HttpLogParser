using System;
using System.Collections.Generic;
using System.Linq;

namespace HttpLogParser
{
    public class Reporter
    {
        private readonly Log _log;

        public Reporter(Log log)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public int CountUniqueIPAddresses()
        {
            return _log.CountRequestsByIPAddress().Keys.Count;
        }

        public IEnumerable<string> GetTopThreeUrls()
        {
            return _log.CountRequestsByUrl()
                .OrderByDescending(x => x.Value)
                .Take(3)
                .Select(x => x.Key);
        }

        public IEnumerable<string> GetTopThreeIPAddresses()
        {
            return _log.CountRequestsByIPAddress()
                .OrderByDescending(x => x.Value)
                .Take(3)
                .Select(x => x.Key);
        }
    }
}
