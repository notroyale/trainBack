namespace Messaging;

internal class ResultError<TValue> : Result<TValue>
{
    private readonly Error _error;

    protected internal ResultError(bool isSuccess, Error error)
        : base(isSuccess)
    {
        _error = error;
    }

    public Error Error => !IsSuccess
        ? _error
        : throw new InvalidOperationException("The error of a failure result cannot be accessed.");

    public static implicit operator ResultError<TValue>(Error error) => Failure(error);
}