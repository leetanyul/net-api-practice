namespace Tan.Application.Dtos;

public record ApiResponseDto<T>
{
    public int Code { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public static ApiResponseDto<T> Success(T data) => new()
    {
        Code = 0,
        Message = "성공",
        Data = data
    };

    public static ApiResponseDto<T> Fail(string message, int code = -1) => new()
    {
        Code = code,
        Message = message,
        Data = default
    };
}

