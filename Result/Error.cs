namespace Result
{
    /// <summary>
    /// Represents an error produced by an operation.
    /// </summary>
    public readonly struct Error
    {
        /// <summary>
        /// Represents the absence of an error.
        /// </summary>
        public static readonly Error None = new Error(string.Empty, string.Empty);  // Represents no error

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Gets the human readable error message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Error"/> with the specified code and message.
        /// </summary>
        /// <param name="code">A short machine-readable error code.</param>
        /// <param name="message">A human-readable description of the error.</param>
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Returns a string that represents the current error.
        /// </summary>
        /// <returns>A string with the format "{code}: {message}".</returns>
        public override string ToString()
        {
            return $"{Code}: {Message}";
        }
    }
}
