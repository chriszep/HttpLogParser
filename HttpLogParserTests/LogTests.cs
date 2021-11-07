using HttpLogParser;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace HttpLogParserTests
{
    public class LogTests
    {
        [Fact]
        public void CountRequestsByIPAddress_ShouldCountOneRequestPerIP()
        {
            var logEntries = new List<LogEntry>
            {
                new LogEntry { IPAddress = "192.168.0.1" },
                new LogEntry { IPAddress = "192.168.0.2" },
                new LogEntry { IPAddress = "192.168.0.3" },
                new LogEntry { IPAddress = "192.168.0.4" }
            };

            var log = new Log(logEntries);

            var result = log.CountRequestsByIPAddress();

            result.Count.ShouldBe(4);
            result["192.168.0.1"].ShouldBe(1);
            result["192.168.0.2"].ShouldBe(1);
            result["192.168.0.3"].ShouldBe(1);
            result["192.168.0.4"].ShouldBe(1);
        }

        [Fact]
        public void CountRequestsByIPAddress_ShouldCountMultipleRequestsPerIP()
        {
            var logEntries = new List<LogEntry>
            {
                new LogEntry { IPAddress = "192.168.0.1" },
                new LogEntry { IPAddress = "192.168.0.2" },
                new LogEntry { IPAddress = "192.168.0.3" },
                new LogEntry { IPAddress = "192.168.0.1" }
            };

            var log = new Log(logEntries);

            var result = log.CountRequestsByIPAddress();

            result.Count.ShouldBe(3);
            result["192.168.0.1"].ShouldBe(2);
            result["192.168.0.2"].ShouldBe(1);
            result["192.168.0.3"].ShouldBe(1);
        }

        [Fact]
        public void CountRequestsByUrl_ShouldCountOneRequestPerUrl()
        {
            var logEntries = new List<LogEntry>
            {
                new LogEntry { Url = "/index" },
                new LogEntry { Url = "/admin" },
                new LogEntry { Url = "/news" },
                new LogEntry { Url = "/contact-us" }
            };

            var log = new Log(logEntries);

            var result = log.CountRequestsByUrl();

            result.Count.ShouldBe(4);
            result["/index"].ShouldBe(1);
            result["/admin"].ShouldBe(1);
            result["/news"].ShouldBe(1);
            result["/contact-us"].ShouldBe(1);
        }

        [Fact]
        public void CountRequestsByUrl_ShouldCountMultipleRequestsPerUrl()
        {
            var logEntries = new List<LogEntry>
            {
                new LogEntry { Url = "/index" },
                new LogEntry { Url = "/admin" },
                new LogEntry { Url = "/news" },
                new LogEntry { Url = "/admin" }
            };

            var log = new Log(logEntries);

            var result = log.CountRequestsByUrl();

            result.Count.ShouldBe(3);
            result["/index"].ShouldBe(1);
            result["/admin"].ShouldBe(2);
            result["/news"].ShouldBe(1);
        }
    }
}
