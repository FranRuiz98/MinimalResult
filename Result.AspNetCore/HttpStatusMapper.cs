using Microsoft.AspNetCore.Http;

namespace Result.AspNetCore
{
    internal enum ErrorCategory
    {
        Unknown = 0,
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        Failure
    }

    internal static class HttpStatusMapper
    {
        public static int FromError(Error error)
        {
            ErrorCategory category = GetCategory(error.Code);

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

        private static ErrorCategory GetCategory(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return ErrorCategory.Unknown;
            }

            var dotIndex = code.IndexOf('.');
            var token = dotIndex > 0 ? code[..dotIndex] : code;

            return token switch
            {
                "Validation" => ErrorCategory.Validation,
                "NotFound" => ErrorCategory.NotFound,
                "Conflict" => ErrorCategory.Conflict,
                "Unauthorized" => ErrorCategory.Unauthorized,
                "Forbidden" => ErrorCategory.Forbidden,
                "Failure" => ErrorCategory.Failure,
                _ => ErrorCategory.Unknown
            };
        }
    }
}
