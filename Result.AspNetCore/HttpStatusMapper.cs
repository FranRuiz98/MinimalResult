using Microsoft.AspNetCore.Http;

namespace Result.AspNetCore
{
    internal static class HttpStatusMapper
    {
        public static int FromError(Error error)
        {
            if (error.Code.StartsWith("Validation.", StringComparison.Ordinal))
                return StatusCodes.Status400BadRequest;

            if (error.Code.StartsWith("NotFound.", StringComparison.Ordinal))
                return StatusCodes.Status404NotFound;

            if (error.Code.StartsWith("Conflict.", StringComparison.Ordinal))
                return StatusCodes.Status409Conflict;

            if (error.Code.StartsWith("Unauthorized.", StringComparison.Ordinal))
                return StatusCodes.Status401Unauthorized;

            if (error.Code.StartsWith("Forbidden.", StringComparison.Ordinal))
                return StatusCodes.Status403Forbidden;

            if (error.Code.StartsWith("Failure.", StringComparison.Ordinal))
                return StatusCodes.Status500InternalServerError;

            return StatusCodes.Status400BadRequest;
        }
    }
}
