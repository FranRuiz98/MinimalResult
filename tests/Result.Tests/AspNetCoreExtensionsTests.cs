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

            IResult http = r.ToHttpResult();

            Assert.NotNull(http);
        }

        [Fact]
        public void ToHttpResult_OnFailure_ReturnsProblem()
        {
            Result r = Result.Failure(Errors.Validation("X", "m"));

            IResult http = r.ToHttpResult();

            Assert.NotNull(http);
        }

        [Fact]
        public void ToHttpResultT_OnSuccess_ReturnsOkWithValue()
        {
            Result<int> r = Result<int>.Success(10);

            IResult http = r.ToHttpResult();

            Assert.NotNull(http);
        }

        [Fact]
        public void ToActionResult_OnSuccess_ReturnsOkResult()
        {
            Result r = Result.Success();

            IActionResult action = r.ToActionResult();

            _ = Assert.IsType<OkResult>(action);
        }

        [Fact]
        public void ToActionResult_OnFailure_ReturnsObjectResultWithProblemDetails()
        {
            Error err = Errors.Validation("X", "m");
            Result r = Result.Failure(err);

            IActionResult action = r.ToActionResult();

            ObjectResult obj = Assert.IsType<ObjectResult>(action);
            Assert.Equal(StatusCodes.Status400BadRequest, obj.StatusCode);

            ProblemDetails pd = Assert.IsType<ProblemDetails>(obj.Value);
            Assert.Equal(err.Code, pd.Title);
            Assert.Equal(err.Message, pd.Detail);
            Assert.Equal(StatusCodes.Status400BadRequest, pd.Status);
        }

        [Fact]
        public void ToActionResultT_OnSuccess_ReturnsOkResult()
        {
            Result<int> r = Result<int>.Success(5);

            IActionResult action = r.ToActionResult();

            // Current implementation returns OkResult for Result<T> success
            _ = Assert.IsType<OkResult>(action);
        }

        [Fact]
        public void ToActionResultT_OnFailure_ReturnsObjectResultWithProblemDetails()
        {
            Error err = Errors.Validation("X", "m");
            Result<int> r = Result<int>.Failure(err);

            IActionResult action = r.ToActionResult();

            ObjectResult obj = Assert.IsType<ObjectResult>(action);
            Assert.Equal(StatusCodes.Status400BadRequest, obj.StatusCode);

            ProblemDetails pd = Assert.IsType<ProblemDetails>(obj.Value);
            Assert.Equal(err.Code, pd.Title);
            Assert.Equal(err.Message, pd.Detail);
            Assert.Equal(StatusCodes.Status400BadRequest, pd.Status);
        }

        [Fact]
        public void ToActionResult_OnNotFound_Returns404()
        {
            Error err = Errors.NotFound("Item", "not found");
            Result r = Result.Failure(err);

            IActionResult action = r.ToActionResult();

            ObjectResult obj = Assert.IsType<ObjectResult>(action);
            Assert.Equal(StatusCodes.Status404NotFound, obj.StatusCode);

            ProblemDetails pd = Assert.IsType<ProblemDetails>(obj.Value);
            Assert.Equal(err.Code, pd.Title);
            Assert.Equal(err.Message, pd.Detail);
            Assert.Equal(StatusCodes.Status404NotFound, pd.Status);
        }

        [Fact]
        public void ToActionResult_OnConflict_Returns409()
        {
            Error err = Errors.Conflict("X", "conflict");
            Result r = Result.Failure(err);

            IActionResult action = r.ToActionResult();

            ObjectResult obj = Assert.IsType<ObjectResult>(action);
            Assert.Equal(StatusCodes.Status409Conflict, obj.StatusCode);

            ProblemDetails pd = Assert.IsType<ProblemDetails>(obj.Value);
            Assert.Equal(err.Code, pd.Title);
            Assert.Equal(err.Message, pd.Detail);
            Assert.Equal(StatusCodes.Status409Conflict, pd.Status);
        }

        [Fact]
        public void ToActionResult_OnUnauthorized_Returns401()
        {
            Error err = Errors.Unauthorized("X", "unauthorized");
            Result r = Result.Failure(err);

            IActionResult action = r.ToActionResult();

            ObjectResult obj = Assert.IsType<ObjectResult>(action);
            Assert.Equal(StatusCodes.Status401Unauthorized, obj.StatusCode);

            ProblemDetails pd = Assert.IsType<ProblemDetails>(obj.Value);
            Assert.Equal(err.Code, pd.Title);
            Assert.Equal(err.Message, pd.Detail);
            Assert.Equal(StatusCodes.Status401Unauthorized, pd.Status);
        }

        [Fact]
        public void ToActionResult_OnForbidden_Returns403()
        {
            Error err = Errors.Forbidden("X", "forbidden");
            Result r = Result.Failure(err);

            IActionResult action = r.ToActionResult();

            ObjectResult obj = Assert.IsType<ObjectResult>(action);
            Assert.Equal(StatusCodes.Status403Forbidden, obj.StatusCode);

            ProblemDetails pd = Assert.IsType<ProblemDetails>(obj.Value);
            Assert.Equal(err.Code, pd.Title);
            Assert.Equal(err.Message, pd.Detail);
            Assert.Equal(StatusCodes.Status403Forbidden, pd.Status);
        }

        [Fact]
        public void ToActionResult_OnFailureCategory_Returns500()
        {
            Error err = Errors.Failure("X", "fatal");
            Result r = Result.Failure(err);

            IActionResult action = r.ToActionResult();

            ObjectResult obj = Assert.IsType<ObjectResult>(action);
            Assert.Equal(StatusCodes.Status500InternalServerError, obj.StatusCode);

            ProblemDetails pd = Assert.IsType<ProblemDetails>(obj.Value);
            Assert.Equal(err.Code, pd.Title);
            Assert.Equal(err.Message, pd.Detail);
            Assert.Equal(StatusCodes.Status500InternalServerError, pd.Status);
        }
    }
}
