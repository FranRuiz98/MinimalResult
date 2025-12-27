using Microsoft.AspNetCore.Http;

namespace Result.AspNetCore
{
    public static class ResultHttpExtensions
    {
        public static IResult ToHttpResult(this Result result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok();
            }

            int statusCode = HttpStatusMapper.FromError(result.Error);

            return Results.Problem(
                statusCode: statusCode,
                title: result.Error.Code,
                detail: result.Error.Message);
        }

        public static IResult ToHttpResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }

            int statusCode = HttpStatusMapper.FromError(result.Error);

            return Results.Problem(
                statusCode: statusCode,
                title: result.Error.Code,
                detail: result.Error.Message);
        }
    }
}

