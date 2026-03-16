namespace BlogApp.DTOs;

public class ApiResponseDto<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public T? Data { get; set; }
    public int StatusCode { get; set; }

    //Success
    public static ApiResponseDto<T> Ok(T data, string message = "Success") => new()
    {
        Success = true,
        Message = message,
        Data = data,
        StatusCode = 200
    };

    public static ApiResponseDto<T> Created(T data, string message = "Created Successfully") => new()
    {
        Success = true,
        Message = message,
        Data = data,
        StatusCode = 201
    };

    public static ApiResponseDto<T> Fail(string message, int statusCode = 400) => new()
    {
        Success = false,
        Message = message,
        Data = default,
        StatusCode = statusCode
    };

}