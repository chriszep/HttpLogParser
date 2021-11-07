using System;
using System.Collections.Generic;

namespace HttpLogParser
{
    public class Log
    {
        private readonly IEnumerable<Request> _requests;

        public Log(IEnumerable<Request> requests)
        {
            _requests = requests ?? throw new ArgumentNullException(nameof(requests));
        }

        public Dictionary<string, int> CountRequestsByIPAddress()
        {
            var requestsByIPAddress = new Dictionary<string, int>();

            foreach (var request in _requests)
            {
                if (requestsByIPAddress.ContainsKey(request.IPAddress))
                {
                    // This IP address has had a request before
                    requestsByIPAddress[request.IPAddress] ++;
                }
                else
                {
                    // First request for this IP address
                    requestsByIPAddress.Add(request.IPAddress, 1);
                }
            }

            return requestsByIPAddress;
        }

        public Dictionary<string, int> CountRequestsByUrl()
        {
            var requestsByUrl = new Dictionary<string, int>();

            foreach (var request in _requests)
            {
                if (requestsByUrl.ContainsKey(request.Url))
                {
                    // This URL has had a request before
                    requestsByUrl[request.Url]++;
                }
                else
                {
                    // First request for this URL
                    requestsByUrl.Add(request.Url, 1);
                }
            }

            return requestsByUrl;
        }
    }
}
