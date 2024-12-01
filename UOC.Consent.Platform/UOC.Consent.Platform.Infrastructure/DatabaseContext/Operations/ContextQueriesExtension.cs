using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;

public static class ContextQueriesExtension
{
    public static async Task<Result<T>> GetByPk<T>(this ApplicationDbContext dbContext, int pk)
        where T : class =>
        await dbContext.FindAsync<T>(pk) switch
        {
            null       => Result.Failure<T>(DomainError.ValidationError("EnterpriseService not found by Id")),
            var exists => Result.Success(exists),
        };
    
    public static IEnumerable<T> GetList<T>(this ApplicationDbContext dbContext, Func<T, bool>? predicate = null)
        where T : class
        => dbContext
           .Set<T>()
           .Where(predicate ?? All<T>());

    private static Func<T, bool> All<T>() => _ => true;
}