using Result;
using Xunit;

namespace Result.Tests
{
    public class ResultTests
    {
        [Fact]
        public void Success_HasIsSuccessTrue_AndNoError()
        {
            var r = Result.Success();

            Assert.True(r.IsSuccess);
            Assert.Equal(Error.None, r.Error);
        }

        [Fact]
        public void Failure_HasIsSuccessFalse_AndHasError()
        {
            var e = new Error("E", "bad");
            var r = Result.Failure(e);

            Assert.False(r.IsSuccess);
            Assert.Equal(e, r.Error);
        }
    }
}
