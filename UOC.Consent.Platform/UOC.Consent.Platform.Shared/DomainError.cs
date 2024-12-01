namespace UOC.Consent.Platform.Shared;

public abstract record DomainError(string Message)
{
    public static DomainError BadRequestError(string message)
    {
        return new BadRequestError(message);
    }

    public static DomainError ValidationError(string message)
    {
        return new ValidationError(message);
    }

    public static DomainError UnexpectedError(string message)
    {
        return new UnexpectedError(message);
    }

    public static DomainError None()
    {
        return new None();
    }

    public T Match<T>(
        Func<BadRequestError, T> badRequestError,
        Func<ValidationError, T> validationError,
        Func<UnexpectedError, T> unexpectedError
    )
    {
        return this switch
        {
            BadRequestError e => badRequestError(e),
            ValidationError e => validationError(e),
            UnexpectedError e => unexpectedError(e),
            _                 => throw new NotSupportedException()
        };
    }
}

public record BadRequestError(string Message) : DomainError(Message);

public record ValidationError(string Message) : DomainError(Message);

public record UnexpectedError(string Message) : DomainError(Message);

public record None() : DomainError(string.Empty);