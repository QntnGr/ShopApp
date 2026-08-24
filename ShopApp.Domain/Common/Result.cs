namespace ShopApp.Domain.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public T? Value { get; }

    public string? ErrorCode { get; }
    public string? ErrorMessage { get; }

    private Result(
        bool isSuccess,
        T? value,
        string? errorCode,
        string? errorMessage)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Success(T value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return new Result<T>(
            isSuccess: true,
            value: value,
            errorCode: null,
            errorMessage: null);
    }

    public static Result<T> Failure(
        string errorCode,
        string errorMessage)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(errorCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(errorMessage);

        return new Result<T>(
            isSuccess: false,
            value: default,
            errorCode: errorCode,
            errorMessage: errorMessage);
    }
}