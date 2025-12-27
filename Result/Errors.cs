namespace Result
{
    public enum ErrorCategory
    {
        Unknown = 0,
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        Failure
    }

    public static class Errors
    {
        public static Error Validation(string code, string message)
        {
            return new Error($"Validation.{code}", message);
        }

        public static Error NotFound(string code, string message)
        {
            return new Error($"NotFound.{code}", message);
        }

        public static Error Conflict(string code, string message)
        {
            return new Error($"Conflict.{code}", message);
        }

        public static Error Unauthorized(string code, string message)
        {
            return new Error($"Unauthorized.{code}", message);
        }

        public static Error Forbidden(string code, string message)
        {
            return new Error($"Forbidden.{code}", message);
        }

        public static Error Failure(string code, string message)
        {
            return new Error($"Failure.{code}", message);
        }
    }

    public static class ErrorCategoryParser
    {
        public static ErrorCategory GetCategory(string code)
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
