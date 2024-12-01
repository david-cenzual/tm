﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;

#nullable disable

namespace UOC.Consent.Platform.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241126234511_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UOC.Consent.Platform.Domain.ConsentAggregate.Consents", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DataIdentifier")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateGiven")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<int>("Method")
                        .HasColumnType("int");

                    b.Property<string>("ProcessingPurpose")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("Reference")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("SupportingInformation")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("TransactionReference")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TransactionReference");

                    b.ToTable("Consents");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.DataSubjectAggregate.DataSubjects", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ElectronicIdRef")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Reference")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ElectronicIdRef")
                        .IsUnique();

                    b.ToTable("DataSubjects");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.ElectronicIdAggregate.ElectronicIds", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BasedOnCertificate")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Certification")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("Reference")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Seal")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Signature")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("ElectronicIds");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.EnterpriseAggregate.Enterprises", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("ElectronicIdRef")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsEngagedInEconomicActivity")
                        .HasColumnType("bit");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<int>("LegalForm")
                        .HasColumnType("int");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("Reference")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ElectronicIdRef")
                        .IsUnique()
                        .HasFilter("[ElectronicIdRef] IS NOT NULL");

                    b.ToTable("Enterprises");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.LedgerAggregate.Ledgers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("Reference")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Ledgers");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.PlatformServiceAggregate.PlatformServices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("EnterpriseReference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDistance")
                        .HasColumnType("bit");

                    b.Property<bool>("IsElectronic")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemuneration")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRequested")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<Guid>("Reference")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseReference");

                    b.ToTable("PlatformServices");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.TransactionAggregate.Transactions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConsentsIdentifiers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Data")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("DataSubjectReference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EnterpriseReference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("EnterprisesId")
                        .HasColumnType("int");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("LedgerReference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("LedgersId")
                        .HasColumnType("int");

                    b.Property<string>("PreviousHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("Reference")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Requester")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Responder")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ServiceReference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DataSubjectReference");

                    b.HasIndex("EnterpriseReference");

                    b.HasIndex("EnterprisesId");

                    b.HasIndex("LedgerReference");

                    b.HasIndex("LedgersId");

                    b.HasIndex("ServiceReference");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.TransactionRequestAggregate.TransactionRequests", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConsentsIdentifiers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("DataSubjectReference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("DataSubjectsId")
                        .HasColumnType("int");

                    b.Property<int>("DraftDecision")
                        .HasColumnType("int");

                    b.Property<Guid?>("EnterpriseReference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("EnterprisesId")
                        .HasColumnType("int");

                    b.Property<string>("Justification")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("Reference")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Requester")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Responder")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ServiceReference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DataSubjectReference");

                    b.HasIndex("DataSubjectsId");

                    b.HasIndex("EnterpriseReference");

                    b.HasIndex("EnterprisesId");

                    b.HasIndex("ServiceReference");

                    b.ToTable("TransactionRequests");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.ConsentAggregate.Consents", b =>
                {
                    b.HasOne("UOC.Consent.Platform.Domain.TransactionAggregate.Transactions", "Transaction")
                        .WithMany("Consents")
                        .HasForeignKey("TransactionReference")
                        .HasPrincipalKey("Reference")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.DataSubjectAggregate.DataSubjects", b =>
                {
                    b.HasOne("UOC.Consent.Platform.Domain.ElectronicIdAggregate.ElectronicIds", "ElectronicId")
                        .WithOne("DataSubject")
                        .HasForeignKey("UOC.Consent.Platform.Domain.DataSubjectAggregate.DataSubjects", "ElectronicIdRef")
                        .HasPrincipalKey("UOC.Consent.Platform.Domain.ElectronicIdAggregate.ElectronicIds", "Reference")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ElectronicId");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.EnterpriseAggregate.Enterprises", b =>
                {
                    b.HasOne("UOC.Consent.Platform.Domain.ElectronicIdAggregate.ElectronicIds", "ElectronicId")
                        .WithOne("Enterprise")
                        .HasForeignKey("UOC.Consent.Platform.Domain.EnterpriseAggregate.Enterprises", "ElectronicIdRef")
                        .HasPrincipalKey("UOC.Consent.Platform.Domain.ElectronicIdAggregate.ElectronicIds", "Reference");

                    b.Navigation("ElectronicId");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.PlatformServiceAggregate.PlatformServices", b =>
                {
                    b.HasOne("UOC.Consent.Platform.Domain.EnterpriseAggregate.Enterprises", "Enterprise")
                        .WithMany("Services")
                        .HasForeignKey("EnterpriseReference")
                        .HasPrincipalKey("Reference")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Enterprise");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.TransactionAggregate.Transactions", b =>
                {
                    b.HasOne("UOC.Consent.Platform.Domain.DataSubjectAggregate.DataSubjects", "DataSubject")
                        .WithMany()
                        .HasForeignKey("DataSubjectReference")
                        .HasPrincipalKey("Reference")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("UOC.Consent.Platform.Domain.EnterpriseAggregate.Enterprises", "Enterprise")
                        .WithMany()
                        .HasForeignKey("EnterpriseReference")
                        .HasPrincipalKey("Reference")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("UOC.Consent.Platform.Domain.EnterpriseAggregate.Enterprises", null)
                        .WithMany("Transactions")
                        .HasForeignKey("EnterprisesId");

                    b.HasOne("UOC.Consent.Platform.Domain.LedgerAggregate.Ledgers", "Ledger")
                        .WithMany()
                        .HasForeignKey("LedgerReference")
                        .HasPrincipalKey("Reference")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("UOC.Consent.Platform.Domain.LedgerAggregate.Ledgers", null)
                        .WithMany("Chain")
                        .HasForeignKey("LedgersId");

                    b.HasOne("UOC.Consent.Platform.Domain.PlatformServiceAggregate.PlatformServices", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceReference")
                        .HasPrincipalKey("Reference")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("DataSubject");

                    b.Navigation("Enterprise");

                    b.Navigation("Ledger");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.TransactionRequestAggregate.TransactionRequests", b =>
                {
                    b.HasOne("UOC.Consent.Platform.Domain.DataSubjectAggregate.DataSubjects", "DataSubject")
                        .WithMany()
                        .HasForeignKey("DataSubjectReference")
                        .HasPrincipalKey("Reference")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("UOC.Consent.Platform.Domain.DataSubjectAggregate.DataSubjects", null)
                        .WithMany("TransactionRequests")
                        .HasForeignKey("DataSubjectsId");

                    b.HasOne("UOC.Consent.Platform.Domain.EnterpriseAggregate.Enterprises", "Enterprise")
                        .WithMany()
                        .HasForeignKey("EnterpriseReference")
                        .HasPrincipalKey("Reference")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("UOC.Consent.Platform.Domain.EnterpriseAggregate.Enterprises", null)
                        .WithMany("TransactionRequests")
                        .HasForeignKey("EnterprisesId");

                    b.HasOne("UOC.Consent.Platform.Domain.PlatformServiceAggregate.PlatformServices", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceReference")
                        .HasPrincipalKey("Reference")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("DataSubject");

                    b.Navigation("Enterprise");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.DataSubjectAggregate.DataSubjects", b =>
                {
                    b.Navigation("TransactionRequests");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.ElectronicIdAggregate.ElectronicIds", b =>
                {
                    b.Navigation("DataSubject");

                    b.Navigation("Enterprise");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.EnterpriseAggregate.Enterprises", b =>
                {
                    b.Navigation("Services");

                    b.Navigation("TransactionRequests");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.LedgerAggregate.Ledgers", b =>
                {
                    b.Navigation("Chain");
                });

            modelBuilder.Entity("UOC.Consent.Platform.Domain.TransactionAggregate.Transactions", b =>
                {
                    b.Navigation("Consents");
                });
#pragma warning restore 612, 618
        }
    }
}