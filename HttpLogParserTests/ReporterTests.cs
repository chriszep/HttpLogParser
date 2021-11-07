using HttpLogParser;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HttpLogParserTests
{
    public class ReporterTests
    {
        [Fact]
        public void CountUniqueIPAddresses_ShouldCountIPAddressWithOneRequestOnce()
        {
            var logEntries = new List<LogEntry>
            {
                new LogEntry { IPAddress = "192.168.0.1" },
                new LogEntry { IPAddress = "192.168.0.2" },
                new LogEntry { IPAddress = "192.168.0.3" },
                new LogEntry { IPAddress = "192.168.0.4" }
            };

            var log = new Log(logEntries);
            var reporter = new Reporter(log);

            var result = reporter.CountUniqueIPAddresses();

            result.ShouldBe(4);
        }

        [Fact]
        public void CountUniqueIPAddresses_ShouldCountIPAddressWithMultipleRequestsOnce()
        {
            var logEntries = new List<LogEntry>
            {
                new LogEntry { IPAddress = "192.168.0.1" },
                new LogEntry { IPAddress = "192.168.0.2" },
                new LogEntry { IPAddress = "192.168.0.1" },
                new LogEntry { IPAddress = "192.168.0.2" }
            };

            var log = new Log(logEntries);
            var reporter = new Reporter(log);

            var result = reporter.CountUniqueIPAddresses();

            result.ShouldBe(2);
        }

        [Fact]
        public void GetTopThreeUrls_ShouldReturnThreeUrlsWithMostRequests()
        {
            var logEntries = new List<LogEntry>
            {
                new LogEntry { Url = "/index" },
                new LogEntry { Url = "/admin" },
                new LogEntry { Url = "/news" },
                new LogEntry { Url = "/contact-us" },
                new LogEntry { Url = "/index" },
                new LogEntry { Url = "/admin" },
                new LogEntry { Url = "/news" },
                new LogEntry { Url = "/news" }
            };

            var log = new Log(logEntries);
            var reporter = new Reporter(log);

            var result = reporter.GetTopThreeUrls();

            result.Count().ShouldBe(3);
            result.ElementAt(0).ShouldBe("/news");
            result.ElementAt(1).ShouldBe("/index");
            result.ElementAt(2).ShouldBe("/admin");
        }

        [Fact]
        public void GetTopThreeIPAddressess_ShouldReturnThreeIPsWithMostRequests()
        {
            var logEntries = new List<LogEntry>
            {
                new LogEntry { IPAddress = "192.168.0.1" },
                new LogEntry { IPAddress = "192.168.0.2" },
                new LogEntry { IPAddress = "192.168.0.3" },
                new LogEntry { IPAddress = "192.168.0.4" },
                new LogEntry { IPAddress = "192.168.0.1" },
                new LogEntry { IPAddress = "192.168.0.2" },
                new LogEntry { IPAddress = "192.168.0.3" },
                new LogEntry { IPAddress = "192.168.0.2" },
            };

            var log = new Log(logEntries);
            var reporter = new Reporter(log);

            var result = reporter.GetTopThreeIPAddresses();

            result.Count().ShouldBe(3);
            result.ElementAt(0).ShouldBe("192.168.0.2");
            result.ElementAt(1).ShouldBe("192.168.0.1");
            result.ElementAt(2).ShouldBe("192.168.0.3");
        }
    }
}
