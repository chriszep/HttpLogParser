using HttpLogParser;
using Shouldly;
using System;
using Xunit;

namespace HttpLogParserTests
{
    public class RequestParserTests
    {
        private const string _validInput = "177.71.128.21 - - [10/Jul/2018:22:21:28 +0200] \"GET /intranet-analytics/ HTTP/1.1\" 200 3574";

        [Fact]
        public void ShouldParseIPAddress()
        {
            var request = RequestParser.Parse(_validInput);

            request.IPAddress.ToString().ShouldBe("177.71.128.21");
        }

        [Fact]
        public void ShouldParseUrl()
        {
            var request = RequestParser.Parse(_validInput);

            request.Url.ShouldBe("/intranet-analytics/");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ShouldThrowExceptionWhenInputNullOrWhiteSpace(string input)
        {
            Assert.Throws<ArgumentNullException>(() => RequestParser.Parse(input));
        }

        [Fact]
        public void ShouldThrowExceptionWhenInputHasLessThan7Segments()
        {
            var input = "177.71.128.21 - - [10/Jul/2018:22:21:28 +0200] \"GET\"";

            var exception = Assert.Throws<FormatException>(() => RequestParser.Parse(input));
            exception.Message.ShouldBe("Request has invalid format");
        }

        [Fact]
        public void ShouldThrowExceptionWhenIPAddressInvalid()
        {
            var input = "<notAnIPAddress> - - [10/Jul/2018:22:21:28 +0200] \"GET /intranet-analytics/ HTTP/1.1\" 200 3574";

            var exception = Assert.Throws<FormatException>(() => RequestParser.Parse(input));
            exception.Message.ShouldBe("An invalid IP address was specified.");
        }
    }
}
