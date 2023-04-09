namespace Messaging;

public class Result<TValue>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public TValue Value { get; }

    protected Result(bool isSuccess, TValue value)
    {
        IsSuccess = isSuccess;
        Value = value;
    }

    protected Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
        Value = default!;
    }

    internal static Result<TValue> Success() => new(true, default!);

    internal static Result<TValue> Success(TValue value) => new(true, value);

    internal static ResultError<TValue> Failure(Error error) 
        => new(false, error);

    internal static Result<TValue> Create(TValue value, Error error)
        => value is null
        ? Failure(error)
        : Success(value);

    internal static Result<TValue> FirstFailureOrSuccess(params Result<TValue>[] results)
    {
        foreach (Result<TValue> result in results)
        {
            if (result.IsFailure)
                return result;
        }

        return Success();
    }

    public static implicit operator Result<TValue>(TValue value) => Success(value);
    public static implicit operator Result<TValue>(Error error) => Failure(error);
}