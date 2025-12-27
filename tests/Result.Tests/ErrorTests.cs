using Result;
using Xunit;

namespace Result.Tests
{
    public class ErrorTests
    {
        [Fact]
        public void Error_ToString_IncludesCodeAndMessage()
        {
            var err = new Error("E1", "Something went wrong");

            var s = err.ToString();

            Assert.Contains("E1", s);
            Assert.Contains("Something went wrong", s);
        }

        [Fact]
        public void None_IsEmptyError()
        {
            var none = Error.None;

            Assert.Equal(string.Empty, none.Code);
            Assert.Equal(string.Empty, none.Message);
            Assert.Equal(": ", none.ToString());
        }
    }
}
