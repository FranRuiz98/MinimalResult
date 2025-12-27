namespace Result.AspNetCore
{
    public static class Errors
    {
        public static Error Validation(string code, string message)
        {
            return new($"Validation.{code}", message);
        }

        public static Error NotFound(string code, string message)
        {
            return new($"NotFound.{code}", message);
        }

        public static Error Conflict(string code, string message)
        {
            return new($"Conflict.{code}", message);
        }

        public static Error Unauthorized(string code, string message)
        {
            return new($"Unauthorized.{code}", message);
        }

        public static Error Forbidden(string code, string message)
        {
            return new($"Forbidden.{code}", message);
        }

        public static Error Failure(string code, string message)
        {
            return new($"Failure.{code}", message);
        }
    }

}
