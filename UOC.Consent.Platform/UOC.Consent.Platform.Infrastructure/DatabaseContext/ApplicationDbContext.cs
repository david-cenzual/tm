using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UOC.Consent.Platform.Domain.ConsentAggregate;
using UOC.Consent.Platform.Domain.DataSubjectAggregate;
using UOC.Consent.Platform.Domain.ElectronicIdAggregate;
using UOC.Consent.Platform.Domain.EnterpriseAggregate;
using UOC.Consent.Platform.Domain.PlatformServiceAggregate;
using UOC.Consent.Platform.Domain.LedgerAggregate;
using UOC.Consent.Platform.Domain.TransactionAggregate;
using UOC.Consent.Platform.Domain.TransactionRequestAggregate;

namespace UOC.Consent.Platform.Infrastructure.DatabaseContext;

public class EnumCollectionJsonValueConverter<T>() : ValueConverter<ICollection<T>, string>(
    v => JsonSerializer.Serialize(
        v.Select(e => e.ToString()).ToList(), JsonSerializerOptions.Default),
    v => (JsonSerializer.Deserialize<ICollection<string>>(v, JsonSerializerOptions.Default) ?? new List<string>())
         .Select(e => (T)Enum.Parse(typeof(T), e)).ToList())
    where T : Enum;

public class CollectionValueComparer<T>() : ValueComparer<ICollection<T>>(
    (c1, c2) => c1.SequenceEqual(c2),
    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => c.ToHashSet());

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<DataSubjects>        DataSubjects        { get; set; }
    public DbSet<ElectronicIds>       ElectronicIds       { get; set; }
    public DbSet<Consents>            Consents            { get; set; }
    public DbSet<Ledgers>             Ledgers             { get; set; }
    public DbSet<Enterprises>         Enterprises         { get; set; }
    public DbSet<TransactionRequests> TransactionRequests { get; set; }
    public DbSet<PlatformServices>    PlatformServices    { get; set; }
    public DbSet<Transactions>        Transactions        { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<DataSubjects>(DataSubjectRelationshipContext)
            .Entity<Enterprises>(EnterpriseRelationshipContext)
            .Entity<PlatformServices>(PlatformServiceRelationshipContext)
            .Entity<TransactionRequests>(TransactionRequestRelationshipContext)
            .Entity<Transactions>(TransactionRelationshipContext)
            .Entity<Consents>(ConsentRelationshipContext);
    }

    private static void EnterpriseRelationshipContext(EntityTypeBuilder<Enterprises> entity)
    {
        entity
            .HasOne(e => e.ElectronicId)
            .WithOne(ei => ei.Enterprise)
            .HasForeignKey<Enterprises>(e => e.ElectronicIdRef)
            .HasPrincipalKey<ElectronicIds>(ei => ei.Reference);
    }

    private static void PlatformServiceRelationshipContext(EntityTypeBuilder<PlatformServices> entity)
    {
        entity
            .HasOne(e => e.Enterprise)
            .WithMany(e => e.Services)
            .HasForeignKey(e => e.EnterpriseReference)
            .HasPrincipalKey(ei => ei.Reference)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void DataSubjectRelationshipContext(EntityTypeBuilder<DataSubjects> entity)
    {
        entity
            .HasOne(ds => ds.ElectronicId)
            .WithOne(ei => ei.DataSubject)
            .HasForeignKey<DataSubjects>(ds => ds.ElectronicIdRef)
            .HasPrincipalKey<ElectronicIds>(ei => ei.Reference);
    }

    private static void ConsentRelationshipContext(EntityTypeBuilder<Consents> entity)
    {
        entity
            .HasOne(c => c.Transaction)
            .WithMany(t => t.Consents)
            .HasForeignKey(c => c.TransactionReference)
            .HasPrincipalKey(t => t.Reference)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void TransactionRequestRelationshipContext(EntityTypeBuilder<TransactionRequests> entity)
    {
        var converter = new EnumCollectionJsonValueConverter<PersonalDataIdentifier>();
        var comparer  = new CollectionValueComparer<PersonalDataIdentifier>();

        entity
            .Property(e => e.ConsentsIdentifiers)
            .HasConversion(converter)
            .Metadata.SetValueComparer(comparer);

        entity.HasOne(tr => tr.Service)
              .WithMany()
              .HasForeignKey(tr => tr.ServiceReference)
              .HasPrincipalKey(s => s.Reference)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(tr => tr.Enterprise)
              .WithMany()
              .HasForeignKey(tr => tr.EnterpriseReference)
              .HasPrincipalKey(e => e.Reference)
              .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(tr => tr.DataSubject)
              .WithMany()
              .HasForeignKey(tr => tr.DataSubjectReference)
              .HasPrincipalKey(ds => ds.Reference)
              .OnDelete(DeleteBehavior.SetNull);
    }

    private static void TransactionRelationshipContext(EntityTypeBuilder<Transactions> entity)
    {
        var converter = new EnumCollectionJsonValueConverter<PersonalDataIdentifier>();
        var comparer  = new CollectionValueComparer<PersonalDataIdentifier>();

        entity
            .Property(e => e.ConsentsIdentifiers)
            .HasConversion(converter)
            .Metadata.SetValueComparer(comparer);

        entity
            .HasOne(t => t.Service)
            .WithMany()
            .HasForeignKey(t => t.ServiceReference)
            .HasPrincipalKey(s => s.Reference)
            .OnDelete(DeleteBehavior.SetNull);

        entity
            .HasOne(t => t.Enterprise)
            .WithMany()
            .HasForeignKey(t => t.EnterpriseReference)
            .HasPrincipalKey(e => e.Reference)
            .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(tr => tr.DataSubject)
              .WithMany()
              .HasForeignKey(tr => tr.DataSubjectReference)
              .HasPrincipalKey(ds => ds.Reference)
              .OnDelete(DeleteBehavior.SetNull);

        entity
            .HasOne(t => t.Ledger)
            .WithMany()
            .HasForeignKey(t => t.LedgerReference)
            .HasPrincipalKey(l => l.Reference)
            .OnDelete(DeleteBehavior.NoAction);
    }
}