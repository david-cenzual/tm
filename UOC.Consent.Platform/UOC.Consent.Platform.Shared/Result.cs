namespace UOC.Consent.Platform.Shared;

public class Result
{
    protected Result(bool isSuccess, DomainError error)
    {
        IsSuccess = isSuccess;
        Error     = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public DomainError Error { get; }

    public static Result Success()
    {
        return new Result(true, DomainError.None());
    }

    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, DomainError.None());
    }

    public static Result Failure(DomainError error)
    {
        return new Result(false, error);
    }

    public static Result<TValue> Failure<TValue>(DomainError error)
    {
        return new Result<TValue>(default!, false, error);
    }
}