namespace UOC.Consent.Platform.Shared;

public abstract record DomainError(string Message)
{
    public static DomainError BadRequestError(string message) => new BadRequestError(message);
    public static DomainError ValidationError(string message) => new ValidationError(message);
    public static DomainError UnexpectedError(string message) => new UnexpectedError(message);
    public static DomainError None() => new None();
    
    public T Match<T>(
        Func<BadRequestError, T> badRequestError,
        Func<ValidationError, T> validationError,
        Func<UnexpectedError, T> unexpectedError
    ) => this switch
    {
        BadRequestError e => badRequestError(e),
        ValidationError e => validationError(e),
        UnexpectedError e => unexpectedError(e),
        _                 => throw new NotSupportedException()
    };
}

public record BadRequestError(string Message) : DomainError(Message);
public record ValidationError(string Message) : DomainError(Message);
public record UnexpectedError(string Message) : DomainError(Message);

public record None() : DomainError(string.Empty);