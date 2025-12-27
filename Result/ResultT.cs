namespace Result
{
    /// <summary>
    /// Represents the outcome of an operation that returns a value of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value returned on success.</typeparam>
    public readonly struct Result<T>
    {
        /// <summary>
        /// Gets a value indicating whether the operation succeeded.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets a value indicating whether the operation failed.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Gets the value produced by a successful operation. The property is undefined when
        /// <see cref="IsSuccess"/> is <c>false</c>.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets the <see cref="Error"/> associated with a failed result. For a successful
        /// result this will be <see cref="Error.None"/>.
        /// </summary>
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

        /// <summary>
        /// Creates a successful <see cref="Result{T}"/> containing the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to wrap in a successful result.</param>
        /// <returns>A <see cref="Result{T}"/> representing success.</returns>
        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        /// <summary>
        /// Creates a failed <see cref="Result{T}"/> with the provided <paramref name="error"/>.
        /// </summary>
        /// <param name="error">The error describing the failure.</param>
        /// <returns>A <see cref="Result{T}"/> representing failure.</returns>
        public static Result<T> Failure(Error error)
        {
            return new Result<T>(error);
        }
    }
}
