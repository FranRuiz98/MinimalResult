using Microsoft.AspNetCore.Http;

namespace Result.AspNetCore
{
    internal static class HttpStatusMapper
    {
        public static int FromError(Error error)
        {
            ErrorCategory category = ErrorCategoryParser.GetCategory(error.Code);

            return category switch
            {
                ErrorCategory.Validation => StatusCodes.Status400BadRequest,
                ErrorCategory.NotFound => StatusCodes.Status404NotFound,
                ErrorCategory.Conflict => StatusCodes.Status409Conflict,
                ErrorCategory.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorCategory.Forbidden => StatusCodes.Status403Forbidden,
                ErrorCategory.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
