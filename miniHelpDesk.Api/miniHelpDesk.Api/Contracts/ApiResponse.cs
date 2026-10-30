namespace miniHelpDesk.Api.Contracts
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
        public string? Code { get; init; }
        public T? Data { get; init; }
        public object? Errors { get; init; }

        public static ApiResponse<T> Ok(T data, string message = "Sucesso")
            => new() { Success = true, Message = message, Data = data };

        public static ApiResponse<T> Fail(string code, string message, object? errors = null)
            => new() { Success = false, Code = code, Message = message, Errors = errors };
    }
}
