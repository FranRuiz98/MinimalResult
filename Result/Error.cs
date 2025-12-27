namespace Result
{
    public readonly struct Error
    {
        public static readonly Error None = new Error(string.Empty, string.Empty);  // Represents no error

        public string Code { get; }
        public string Message { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public override string ToString()
        {
            return $"{Code}: {Message}";
        }
    }
}
