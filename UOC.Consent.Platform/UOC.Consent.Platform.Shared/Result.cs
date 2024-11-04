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

    public static Result Success() => new(true, DomainError.None());
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, DomainError.None());
    public static Result Failure(DomainError error) => new(false, error);
    public static Result<TValue> Failure<TValue>(DomainError error) => new(default!, false, error);
}