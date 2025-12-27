using Xunit;

namespace Result.Tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void Match_Result_CallsCorrectFunc()
        {
            Result success = Result.Success();
            Result failure = Result.Failure(new Error("E", "m"));

            var s = success.Match(() => "ok", () => "no");
            var f = failure.Match(() => "ok", () => "no");

            Assert.Equal("ok", s);
            Assert.Equal("no", f);
        }

        [Fact]
        public void Match_ResultT_CallsCorrectFunc()
        {
            Result<int> success = Result<int>.Success(5);
            Result<int> failure = Result<int>.Failure(new Error("E", "m"));

            var s = success.Match(x => x * 2, e => -1);
            var f = failure.Match(x => x * 2, e => -1);

            Assert.Equal(10, s);
            Assert.Equal(-1, f);
        }

        [Fact]
        public void Map_ResultT_MapsValue()
        {
            Result<int> r = Result<int>.Success(3);

            var m = r.Map(x => x + 1);

            Assert.True(m.IsSuccess);
            Assert.Equal(4, m.Value);
        }

        [Fact]
        public void Map_ResultT_OnFailure_PreservesFailure()
        {
            Error err = new("E", "bad");
            Result<int> r = Result<int>.Failure(err);

            var m = r.Map(x => x + 1);

            Assert.True(m.IsFailure);
            Assert.Equal(err, m.Error);
        }

        [Fact]
        public void Map_ResultT_ToDifferentType_MapsValue()
        {
            Result<int> r = Result<int>.Success(7);

            var m = r.Map(x => x.ToString());

            Assert.True(m.IsSuccess);
            Assert.Equal("7", m.Value);
        }

        [Fact]
        public void Bind_Result_Chains()
        {
            Result r = Result.Success();

            var chained = r.Bind(() => Result.Failure(new Error("E", "m")));

            Assert.True(chained.IsFailure);
        }

        [Fact]
        public void Bind_Result_OnFailure_ReturnsSameFailure()
        {
            Error err = new("E", "bad");
            Result r = Result.Failure(err);

            var chained = r.Bind(Result.Success);

            Assert.True(chained.IsFailure);
            Assert.Equal(err, chained.Error);
        }

        [Fact]
        public void Bind_Result_OnSuccess_ReturnsResultFromFunc()
        {
            Result r = Result.Success();

            var chained = r.Bind(Result.Success);

            Assert.True(chained.IsSuccess);
        }
    }
}
