namespace MiniOrderSystem.Shared.Result
{
    public record Error
    {
        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);

        public static implicit operator Result(Error error)
            => Result.Failure(error);

        private Error(string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }

        public string Code { get; set; }
        public string Message { get; set; }
        public ErrorType Type { get; set; }

        public static Error NotFound(string code, string message)
          => new(code, message, ErrorType.NotFound);

        public static Error Exception(string code, string message)
            => new(code, message, ErrorType.Failure);

        public static Error Failure(string code, string message)
            => new(code, message, ErrorType.Failure);

        public static Error Validation(string code, string message)
            => new(code, message, ErrorType.Validation);

        public static Error UnAuthorized(string code, string message)
            => new(code, message, ErrorType.UnAuthorized);

        public static Error Forbbiden(string code, string message)
            => new(code, message, ErrorType.Forbidden);
    }
}
