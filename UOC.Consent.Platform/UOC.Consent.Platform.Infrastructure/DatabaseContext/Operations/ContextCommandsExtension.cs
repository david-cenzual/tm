using UOC.Consent.Platform.Domain.DataSubjectAggregate;
using UOC.Consent.Platform.Domain.ElectronicIdAggregate;
using UOC.Consent.Platform.Domain.EnterpriseAggregate;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;

public static class ContextCommandsExtension
{
    public static ApplicationDbContext Create<T>(
        this ApplicationDbContext dbContext,
        T entity)
        where T : class
    {
        dbContext.Add(entity);
        return dbContext;
    }

    public static async Task<Result<T>> TryUpdate<T>(
        this ApplicationDbContext dbContext,
        object?[]? keyValues,
        T entity,
        CancellationToken ct = new())
        where T : class
    {
        if (await dbContext.FindAsync<T>(keyValues, ct) is { } found)
        {
            dbContext.Entry(found).CurrentValues.SetValues(entity);
        }

        return entity;
    }

    public static ApplicationDbContext RegisterDataSubjectWithExistingEidas(
        this ApplicationDbContext dbContext, ElectronicIds eRecord)
    {
        dbContext
            .DataSubjects
            .Add(new DataSubjects { ElectronicIdRef = eRecord.Reference });

        return dbContext;
    }

    public static ApplicationDbContext RegisterEnterpriseWithExistingEidas(
        this ApplicationDbContext dbContext, ElectronicIds eRecord)
    {
        dbContext
            .Enterprises
            .Add(new Enterprises
            {
                ElectronicIdRef = eRecord.Reference, 
                ElectronicId = eRecord
            });

        return dbContext;
    }

    public static ApplicationDbContext RegisterDataSubjectWithNewEidas(this ApplicationDbContext dbContext,
                                                                       ElectronicIds eRecord)
    {
        var res = dbContext.ElectronicIds.Add(new ElectronicIds
        {
            Certification      = eRecord.Certification,
            Signature          = eRecord.Signature,
            Seal               = eRecord.Seal,
            BasedOnCertificate = eRecord.BasedOnCertificate,
        });

        dbContext
            .DataSubjects
            .Add(new DataSubjects { ElectronicId = res.Entity, ElectronicIdRef = res.Entity.Reference });

        return dbContext;
    }
    
    public static ApplicationDbContext RegisterEnterpriseWithNewEidas(
        this ApplicationDbContext dbContext,
        ElectronicIds eRecord)
    {
        var res = dbContext.ElectronicIds.Add(new ElectronicIds
        {
            Certification      = eRecord.Certification,
            Signature          = eRecord.Signature,
            Seal               = eRecord.Seal,
            BasedOnCertificate = eRecord.BasedOnCertificate,
        });

        dbContext
            .Enterprises
            .Add(new Enterprises
            {
                ElectronicId = res.Entity, 
                ElectronicIdRef = res.Entity.Reference
            });

        return dbContext;
    }


    public static async Task<Result<List<T>>> AddRange<T>(
        this ApplicationDbContext dbContext,
        List<T> entities,
        CancellationToken ct)
        where T : class
    {
        try
        {
            await dbContext.AddRangeAsync(entities, ct);
            return Result.Success(entities);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<T>>(DomainError.ValidationError(ex.Message));
        }
    }

    public static Result<IEnumerable<T>> UpdateRange<T>(this ApplicationDbContext dbContext, IEnumerable<T> entities)
        where T : class
    {
        try
        {
            dbContext.UpdateRange([entities]);
            return Result.Success(entities);
        }
        catch (Exception ex)
        {
            return Result.Failure<IEnumerable<T>>(DomainError.ValidationError(ex.Message));
        }
    }
}