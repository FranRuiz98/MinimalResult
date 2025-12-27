namespace Result
{
    public readonly struct Result<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T Value { get; }
        public Error Error { get; }

        private Result(T value)
        {
            IsSuccess = true;
            Value = value;
            Error = Error.None;
        }

        private Result(Error error)
        {
            IsSuccess = false;
            Value = default!;
            Error = error;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Failure(Error error)
        {
            return new Result<T>(error);
        }
    }
}
