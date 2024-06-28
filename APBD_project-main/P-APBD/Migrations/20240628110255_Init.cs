using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicationUser",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationUser", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "promotion",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountValue = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promotion", x => x.PromotionId);
                });

            migrationBuilder.CreateTable(
                name: "software",
                columns: table => new
                {
                    SoftwareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoftwareName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftwareDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftwareVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnnualCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_software", x => x.SoftwareId);
                });

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_company_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "individual",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_individual", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_individual_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "serviceSubscription",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoftwareId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RenewalPeriodInMonths = table.Column<int>(type: "int", nullable: false),
                    SubscriptionCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serviceSubscription", x => x.SubscriptionId);
                    table.ForeignKey(
                        name: "FK_serviceSubscription_software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "software",
                        principalColumn: "SoftwareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "softwareContract",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SoftwareId = table.Column<int>(type: "int", nullable: false),
                    SoftwareVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupportDuration = table.Column<int>(type: "int", nullable: false),
                    PromotionId = table.Column<int>(type: "int", nullable: true),
                    DiscountValue = table.Column<int>(type: "int", nullable: true),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_softwareContract", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_softwareContract_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_softwareContract_promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "promotion",
                        principalColumn: "PromotionId");
                    table.ForeignKey(
                        name: "FK_softwareContract_software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "software",
                        principalColumn: "SoftwareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contractPayment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    PaymentDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contractPayment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_contractPayment_softwareContract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "softwareContract",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contractPayment_ContractId",
                table: "contractPayment",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_serviceSubscription_SoftwareId",
                table: "serviceSubscription",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_softwareContract_CustomerId",
                table: "softwareContract",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_softwareContract_PromotionId",
                table: "softwareContract",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_softwareContract_SoftwareId",
                table: "softwareContract",
                column: "SoftwareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationUser");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "contractPayment");

            migrationBuilder.DropTable(
                name: "individual");

            migrationBuilder.DropTable(
                name: "serviceSubscription");

            migrationBuilder.DropTable(
                name: "softwareContract");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "promotion");

            migrationBuilder.DropTable(
                name: "software");
        }
    }
}
