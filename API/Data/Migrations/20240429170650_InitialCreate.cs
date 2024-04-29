using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerCompanies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    BenefitCategoryForStandardUsers = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCompanies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    DiscountForPlatinumUsers = table.Column<decimal>(type: "TEXT", nullable: false),
                    CustomerCompanyID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Merchants_CustomerCompanies_CustomerCompanyID",
                        column: x => x.CustomerCompanyID,
                        principalTable: "CustomerCompanies",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    UserType = table.Column<int>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerCompanyID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_CustomerCompanies_CustomerCompanyID",
                        column: x => x.CustomerCompanyID,
                        principalTable: "CustomerCompanies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Benefits",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    MerchantID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benefits", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Benefits_Merchants_MerchantID",
                        column: x => x.MerchantID,
                        principalTable: "Merchants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CardNumber = table.Column<string>(type: "TEXT", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cards_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "INTEGER", nullable: false),
                    CardID = table.Column<int>(type: "INTEGER", nullable: false),
                    MerchantID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transactions_Cards_CardID",
                        column: x => x.CardID,
                        principalTable: "Cards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Merchants_MerchantID",
                        column: x => x.MerchantID,
                        principalTable: "Merchants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CustomerCompanies",
                columns: new[] { "ID", "BenefitCategoryForStandardUsers", "Name" },
                values: new object[,]
                {
                    { 1, 0, "Novatix" },
                    { 2, 1, "CodeTech" },
                    { 3, 5, "Simplify" }
                });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "ID", "Category", "CustomerCompanyID", "DiscountForPlatinumUsers", "Name" },
                values: new object[,]
                {
                    { 1, 0, null, 0.10m, "Pause" },
                    { 2, 1, null, 0.15m, "MegaGym" },
                    { 3, 3, null, 0.20m, "Museum of Contemporary Art" },
                    { 4, 2, null, 0.25m, "Educons Online Learning " }
                });

            migrationBuilder.InsertData(
                table: "Benefits",
                columns: new[] { "ID", "Category", "MerchantID", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 0, 1, "Discounted Meal", 500m },
                    { 2, 1, 2, "Gym Membership", 1500m },
                    { 3, 3, 3, "Museum Ticket", 1000m },
                    { 4, 2, 4, "Online Course", 10000m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "CustomerCompanyID", "Email", "FirstName", "LastName", "UserType" },
                values: new object[,]
                {
                    { 1, 1, "petar.maric@gmail.com", "Petar", "Maric", 0 },
                    { 2, 1, "milos.saric@gmail.com", "Milos", "Saric", 0 },
                    { 3, 3, "nedeljko.rac@yahoo.com", "Nedeljko", "Racic", 1 },
                    { 4, 2, "stefan.perovic@yahoo.com", "Stefan", "Perovic", 1 },
                    { 5, 1, "mitarjovic@gmail.com", "Mitar", "Jovic", 2 },
                    { 6, 2, "rista88@gmail.com", "Rista", "Gocic", 2 }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "ID", "Balance", "CardNumber", "ExpiryDate", "UserID" },
                values: new object[,]
                {
                    { 1, 120000m, "1234567890123456", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 22000m, "9876543210987654", new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 1600m, "5678901234567890", new DateTime(2023, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, 5000m, "3210987654321098", new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 5, 10000m, "4567890123456789", new DateTime(2024, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 6, 9700m, "9012345678901234", new DateTime(2023, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "ID", "Amount", "CardID", "DateTime", "IsSuccessful", "MerchantID" },
                values: new object[,]
                {
                    { 1, 16000m, 1, new DateTime(2024, 4, 29, 19, 6, 48, 844, DateTimeKind.Local).AddTicks(770), true, 1 },
                    { 2, 250m, 2, new DateTime(2024, 4, 29, 19, 6, 48, 844, DateTimeKind.Local).AddTicks(858), true, 2 },
                    { 3, 10000.55m, 3, new DateTime(2024, 4, 29, 19, 6, 48, 844, DateTimeKind.Local).AddTicks(868), true, 3 },
                    { 4, 1500.12m, 4, new DateTime(2024, 4, 29, 19, 6, 48, 844, DateTimeKind.Local).AddTicks(876), false, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Benefits_MerchantID",
                table: "Benefits",
                column: "MerchantID");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserID",
                table: "Cards",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_CustomerCompanyID",
                table: "Merchants",
                column: "CustomerCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CardID",
                table: "Transactions",
                column: "CardID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_MerchantID",
                table: "Transactions",
                column: "MerchantID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomerCompanyID",
                table: "Users",
                column: "CustomerCompanyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Benefits");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CustomerCompanies");
        }
    }
}
