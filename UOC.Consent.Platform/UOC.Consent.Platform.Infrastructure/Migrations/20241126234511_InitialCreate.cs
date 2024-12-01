using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UOC.Consent.Platform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectronicIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Certification = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Seal = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BasedOnCertificate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicIds", x => x.Id);
                    table.UniqueConstraint("AK_ElectronicIds_Reference", x => x.Reference);
                });

            migrationBuilder.CreateTable(
                name: "Ledgers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ledgers", x => x.Id);
                    table.UniqueConstraint("AK_Ledgers_Reference", x => x.Reference);
                });

            migrationBuilder.CreateTable(
                name: "DataSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ElectronicIdRef = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSubjects", x => x.Id);
                    table.UniqueConstraint("AK_DataSubjects_Reference", x => x.Reference);
                    table.ForeignKey(
                        name: "FK_DataSubjects_ElectronicIds_ElectronicIdRef",
                        column: x => x.ElectronicIdRef,
                        principalTable: "ElectronicIds",
                        principalColumn: "Reference",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enterprises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LegalForm = table.Column<int>(type: "int", nullable: false),
                    IsEngagedInEconomicActivity = table.Column<bool>(type: "bit", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    ElectronicIdRef = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprises", x => x.Id);
                    table.UniqueConstraint("AK_Enterprises_Reference", x => x.Reference);
                    table.ForeignKey(
                        name: "FK_Enterprises_ElectronicIds_ElectronicIdRef",
                        column: x => x.ElectronicIdRef,
                        principalTable: "ElectronicIds",
                        principalColumn: "Reference");
                });

            migrationBuilder.CreateTable(
                name: "PlatformServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnterpriseReference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    IsRemuneration = table.Column<bool>(type: "bit", nullable: false),
                    IsDistance = table.Column<bool>(type: "bit", nullable: false),
                    IsElectronic = table.Column<bool>(type: "bit", nullable: false),
                    IsRequested = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformServices", x => x.Id);
                    table.UniqueConstraint("AK_PlatformServices_Reference", x => x.Reference);
                    table.ForeignKey(
                        name: "FK_PlatformServices_Enterprises_EnterpriseReference",
                        column: x => x.EnterpriseReference,
                        principalTable: "Enterprises",
                        principalColumn: "Reference");
                });

            migrationBuilder.CreateTable(
                name: "TransactionRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DraftDecision = table.Column<int>(type: "int", nullable: false),
                    Justification = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Requester = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Responder = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsentsIdentifiers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceReference = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EnterpriseReference = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataSubjectReference = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataSubjectsId = table.Column<int>(type: "int", nullable: true),
                    EnterprisesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionRequests_DataSubjects_DataSubjectReference",
                        column: x => x.DataSubjectReference,
                        principalTable: "DataSubjects",
                        principalColumn: "Reference",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TransactionRequests_DataSubjects_DataSubjectsId",
                        column: x => x.DataSubjectsId,
                        principalTable: "DataSubjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionRequests_Enterprises_EnterpriseReference",
                        column: x => x.EnterpriseReference,
                        principalTable: "Enterprises",
                        principalColumn: "Reference",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TransactionRequests_Enterprises_EnterprisesId",
                        column: x => x.EnterprisesId,
                        principalTable: "Enterprises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionRequests_PlatformServices_ServiceReference",
                        column: x => x.ServiceReference,
                        principalTable: "PlatformServices",
                        principalColumn: "Reference",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceReference = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EnterpriseReference = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataSubjectReference = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LedgerReference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Requester = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Responder = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreviousHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ConsentsIdentifiers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EnterprisesId = table.Column<int>(type: "int", nullable: true),
                    LedgersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.UniqueConstraint("AK_Transactions_Reference", x => x.Reference);
                    table.ForeignKey(
                        name: "FK_Transactions_DataSubjects_DataSubjectReference",
                        column: x => x.DataSubjectReference,
                        principalTable: "DataSubjects",
                        principalColumn: "Reference",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Transactions_Enterprises_EnterpriseReference",
                        column: x => x.EnterpriseReference,
                        principalTable: "Enterprises",
                        principalColumn: "Reference",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Transactions_Enterprises_EnterprisesId",
                        column: x => x.EnterprisesId,
                        principalTable: "Enterprises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Ledgers_LedgerReference",
                        column: x => x.LedgerReference,
                        principalTable: "Ledgers",
                        principalColumn: "Reference");
                    table.ForeignKey(
                        name: "FK_Transactions_Ledgers_LedgersId",
                        column: x => x.LedgersId,
                        principalTable: "Ledgers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_PlatformServices_ServiceReference",
                        column: x => x.ServiceReference,
                        principalTable: "PlatformServices",
                        principalColumn: "Reference",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Consents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionReference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    DateGiven = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessingPurpose = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SupportingInformation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Method = table.Column<int>(type: "int", nullable: false),
                    DataIdentifier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consents_Transactions_TransactionReference",
                        column: x => x.TransactionReference,
                        principalTable: "Transactions",
                        principalColumn: "Reference");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consents_TransactionReference",
                table: "Consents",
                column: "TransactionReference");

            migrationBuilder.CreateIndex(
                name: "IX_DataSubjects_ElectronicIdRef",
                table: "DataSubjects",
                column: "ElectronicIdRef",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enterprises_ElectronicIdRef",
                table: "Enterprises",
                column: "ElectronicIdRef",
                unique: true,
                filter: "[ElectronicIdRef] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformServices_EnterpriseReference",
                table: "PlatformServices",
                column: "EnterpriseReference");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRequests_DataSubjectReference",
                table: "TransactionRequests",
                column: "DataSubjectReference");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRequests_DataSubjectsId",
                table: "TransactionRequests",
                column: "DataSubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRequests_EnterpriseReference",
                table: "TransactionRequests",
                column: "EnterpriseReference");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRequests_EnterprisesId",
                table: "TransactionRequests",
                column: "EnterprisesId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRequests_ServiceReference",
                table: "TransactionRequests",
                column: "ServiceReference");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DataSubjectReference",
                table: "Transactions",
                column: "DataSubjectReference");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_EnterpriseReference",
                table: "Transactions",
                column: "EnterpriseReference");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_EnterprisesId",
                table: "Transactions",
                column: "EnterprisesId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_LedgerReference",
                table: "Transactions",
                column: "LedgerReference");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_LedgersId",
                table: "Transactions",
                column: "LedgersId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ServiceReference",
                table: "Transactions",
                column: "ServiceReference");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consents");

            migrationBuilder.DropTable(
                name: "TransactionRequests");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "DataSubjects");

            migrationBuilder.DropTable(
                name: "Ledgers");

            migrationBuilder.DropTable(
                name: "PlatformServices");

            migrationBuilder.DropTable(
                name: "Enterprises");

            migrationBuilder.DropTable(
                name: "ElectronicIds");
        }
    }
}
