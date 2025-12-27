namespace Result
{
    /// <summary>
    /// Represents the outcome of an operation that does not return a value.
    /// </summary>
    public readonly struct Result
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
        /// Gets the <see cref="Error"/> associated with a failed result. For a successful
        /// result this will be <see cref="Error.None"/>.
        /// </summary>
        public Error Error { get; }

        private Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// Creates a successful <see cref="Result"/>.
        /// </summary>
        /// <returns>A <see cref="Result"/> representing success.</returns>
        public static Result Success()
        {
            return new Result(true, Error.None);
        }

        /// <summary>
        /// Creates a failed <see cref="Result"/> with the provided <paramref name="error"/>.
        /// </summary>
        /// <param name="error">The error describing the failure.</param>
        /// <returns>A <see cref="Result"/> representing failure.</returns>
        public static Result Failure(Error error)
        {
            return new Result(false, error);
        }
    }
}