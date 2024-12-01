using static UOC.Consent.Platform.Shared.Result;

namespace UOC.Consent.Platform.Shared;

public static class ResultExtensions
{
    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Result<TIn> res, Func<TIn, Task<Result<TOut>>> output)
    {
        return res.IsSuccess
            ? await output(res.Value)
            : Failure<TOut>(res.Error);
    }

    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> output)
    {
        return result.IsSuccess
            ? output(result.Value)
            : Failure<TOut>(result.Error);
    }

    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> res, Func<TIn, Result<TOut>> bind)
    {
        return (await res).Bind(bind);
    }

    public static async Task<Result<TOut>> Bind<TIn, TOut>(
        this Task<Result<TIn>> res,
        Func<TIn, Task<Result<TOut>>> bind)
    {
        return await (await res).Bind(bind);
    }

    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mapper)
    {
        return result.IsSuccess
            ? mapper(result.Value)
            : Failure<TOut>(result.Error);
    }

    public static async Task<Result<TOut>> Map<TIn, TOut>(this Task<Result<TIn>> result, Func<TIn, TOut> mapper)
    {
        return (await result).Map(mapper);
    }

    public static Task<Result<TR>> Select<T, TR>(this Task<Result<T>> result, Func<T, TR> project)
    {
        return result.Map(project);
    }

    public static Result<TR> SelectMany<T, TK, TR>(this Result<T> res, Func<T, Result<TK>> bind, Func<T, TK, TR> map)
    {
        return res.Bind(bind).Map(input => map(res.Value, input));
    }

    public static async Task<Result<TR>> SelectMany<T, TK, TR>(
        this Task<Result<T>> result,
        Func<T, Result<TK>> binder,
        Func<T, TK, TR> mapper)
    {
        return await result
                     .Bind(binder)
                     .Map(async input => mapper((await result).Value, input)) switch
        {
            { IsSuccess: true } a => await a.Value,
            { IsFailure: true } c => Failure<TR>(c.Error),
            _                     => throw new Exception("Result is in an invalid state")
        };
    }

    public static Task<Result<TR>> SelectMany<T, TK, TR>(
        this Result<T> result,
        Func<T, Task<Result<TK>>> binder,
        Func<T, TK, TR> project)
    {
        return result.Bind(binder).Map(x => project(result.Value, x));
    }

    public static async Task<Result<TR>> SelectMany<T, TK, TR>(
        this Task<Result<T>> result,
        Func<T, Task<Result<TK>>> func,
        Func<T, TK, TR> project)
    {
        var res = await result;
        return await result.Bind(func).Map(x => project(res.Value, x));
    }

    public static async Task<T> Match<T>(
        this Task<Result> resultTask,
        Func<T> onSuccess,
        Func<DomainError, T> onFailure)
    {
        var res = await resultTask;
        return res.IsSuccess
            ? onSuccess()
            : onFailure(res.Error);
    }

    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<DomainError, TOut> onFailure)
    {
        return result.IsSuccess
            ? onSuccess(result.Value)
            : onFailure(result.Error);
    }

    public static async Task<TOut> Match<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, TOut> onSuccess,
        Func<DomainError, TOut> onFailure)
    {
        return (await resultTask).Match(onSuccess, onFailure);
    }
}