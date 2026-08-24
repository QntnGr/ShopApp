
namespace ShopApp.Domain.Common;

public class Result
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    protected Result(
        bool isSuccess,
        Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
    {
        return new Result(
            isSuccess: true,
            Error.None);
    }

    public static Result Failure(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);

        return new Result(
            isSuccess: false,
            error);
    }
}

public sealed class Result<T> : Result
{
    public T? Value { get; }

    private Result(
        bool isSuccess,
        T? value,
        Error error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return new Result<T>(
            isSuccess: true,
            value,
            Error.None);
    }

    public static Result<T> Failure(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);

        return new Result<T>(
            isSuccess: false,
            default,
            error);
    }
}

public sealed record Error(
    string Code,
    string Message)
{
    public static readonly Error None = new(
        string.Empty,
        string.Empty);
}