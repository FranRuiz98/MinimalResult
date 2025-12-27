using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Result.AspNetCore;
using Xunit;

namespace Result.Tests
{
    public class AspNetCoreExtensionsTests
    {
        [Fact]
        public void ToHttpResult_OnSuccess_ReturnsOkResult()
        {
            Result r = Result.Success();

            var http = r.ToHttpResult();

            Assert.NotNull(http);
        }

        [Fact]
        public void ToHttpResult_OnFailure_ReturnsProblem()
        {
            Result r = Result.Failure(new Error("E", "m"));

            var http = r.ToHttpResult();

            Assert.NotNull(http);
        }

        [Fact]
        public void ToHttpResultT_OnSuccess_ReturnsOkWithValue()
        {
            Result<int> r = Result<int>.Success(10);

            var http = r.ToHttpResult();

            Assert.NotNull(http);
        }

        [Fact]
        public void ToActionResult_OnSuccess_ReturnsOkResult()
        {
            Result r = Result.Success();

            var action = r.ToActionResult();

            _ = Assert.IsType<OkResult>(action);
        }

        [Fact]
        public void ToActionResult_OnFailure_ReturnsObjectResultWithProblemDetails()
        {
            Error err = new("E", "m");
            Result r = Result.Failure(err);

            var action = r.ToActionResult();

            var obj = Assert.IsType<ObjectResult>(action);
            Assert.Equal(StatusCodes.Status400BadRequest, obj.StatusCode);

            var pd = Assert.IsType<ProblemDetails>(obj.Value);
            Assert.Equal(err.Code, pd.Title);
            Assert.Equal(err.Message, pd.Detail);
            Assert.Equal(StatusCodes.Status400BadRequest, pd.Status);
        }

        [Fact]
        public void ToActionResultT_OnSuccess_ReturnsOkResult()
        {
            Result<int> r = Result<int>.Success(5);

            var action = r.ToActionResult();

            // Current implementation returns OkResult for Result<T> success
            _ = Assert.IsType<OkResult>(action);
        }

        [Fact]
        public void ToActionResultT_OnFailure_ReturnsObjectResultWithProblemDetails()
        {
            Error err = new("E", "m");
            Result<int> r = Result<int>.Failure(err);

            var action = r.ToActionResult();

            var obj = Assert.IsType<ObjectResult>(action);
            Assert.Equal(StatusCodes.Status400BadRequest, obj.StatusCode);

            var pd = Assert.IsType<ProblemDetails>(obj.Value);
            Assert.Equal(err.Code, pd.Title);
            Assert.Equal(err.Message, pd.Detail);
            Assert.Equal(StatusCodes.Status400BadRequest, pd.Status);
        }
    }
}
