using Microsoft.AspNetCore.Mvc;

namespace Result.AspNetCore
{
    public static class ResultMvcExtensions
    {
        public static IActionResult ToActionResult(this Result result)
        {
            if (result.IsSuccess)
            {
                return new OkResult();
            }

            int statusCode = HttpStatusMapper.FromError(result.Error);

            return new ObjectResult(new ProblemDetails
            {
                Status = statusCode,
                Title = result.Error.Code,
                Detail = result.Error.Message
            })
            {
                StatusCode = statusCode
            };
        }

        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkResult();
            }

            int statusCode = HttpStatusMapper.FromError(result.Error);

            return new ObjectResult(new ProblemDetails
            {
                Status = statusCode,
                Title = result.Error.Code,
                Detail = result.Error.Message
            })
            {
                StatusCode = statusCode
            };
        }
    }
}
