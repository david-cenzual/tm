namespace UOC.Consent.Platform.Shared;

public class Result<T> : Result
{
    private readonly T _value;
    protected internal Result(T value, bool isSuccess, DomainError error) : base(isSuccess, error) => _value = value;

    public static implicit operator Result<T>(T value) => Success(value);
    public T Value => IsSuccess ? _value : throw new InvalidOperationException("Accessing value of Failure Result");
}