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
            var requests = new List<Request>
            {
                new Request { IPAddress = "192.168.0.1" },
                new Request { IPAddress = "192.168.0.2" },
                new Request { IPAddress = "192.168.0.3" },
                new Request { IPAddress = "192.168.0.4" }
            };

            var log = new Log(requests);
            var reporter = new Reporter(log);

            var result = reporter.CountUniqueIPAddresses();

            result.ShouldBe(4);
        }

        [Fact]
        public void CountUniqueIPAddresses_ShouldCountIPAddressWithMultipleRequestsOnce()
        {
            var requests = new List<Request>
            {
                new Request { IPAddress = "192.168.0.1" },
                new Request { IPAddress = "192.168.0.2" },
                new Request { IPAddress = "192.168.0.1" },
                new Request { IPAddress = "192.168.0.2" }
            };

            var log = new Log(requests);
            var reporter = new Reporter(log);

            var result = reporter.CountUniqueIPAddresses();

            result.ShouldBe(2);
        }

        [Fact]
        public void GetTopThreeUrls_ShouldReturnThreeUrlsWithMostRequests()
        {
            var requests = new List<Request>
            {
                new Request { Url = "/index" },
                new Request { Url = "/admin" },
                new Request { Url = "/news" },
                new Request { Url = "/contact-us" },
                new Request { Url = "/index" },
                new Request { Url = "/admin" },
                new Request { Url = "/news" },
                new Request { Url = "/news" }
            };

            var log = new Log(requests);
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
            var requests = new List<Request>
            {
                new Request { IPAddress = "192.168.0.1" },
                new Request { IPAddress = "192.168.0.2" },
                new Request { IPAddress = "192.168.0.3" },
                new Request { IPAddress = "192.168.0.4" },
                new Request { IPAddress = "192.168.0.1" },
                new Request { IPAddress = "192.168.0.2" },
                new Request { IPAddress = "192.168.0.3" },
                new Request { IPAddress = "192.168.0.2" },
            };

            var log = new Log(requests);
            var reporter = new Reporter(log);

            var result = reporter.GetTopThreeIPAddresses();

            result.Count().ShouldBe(3);
            result.ElementAt(0).ShouldBe("192.168.0.2");
            result.ElementAt(1).ShouldBe("192.168.0.1");
            result.ElementAt(2).ShouldBe("192.168.0.3");
        }
    }
}
