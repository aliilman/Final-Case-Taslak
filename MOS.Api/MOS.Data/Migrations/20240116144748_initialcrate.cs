using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MOS.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialcrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Admin",
                schema: "dbo",
                columns: table => new
                {
                    AdminNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminNumber);
                });

            migrationBuilder.CreateTable(
                name: "Personal",
                schema: "dbo",
                columns: table => new
                {
                    PersonalNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IBAN = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.PersonalNumber);
                });

            migrationBuilder.CreateTable(
                name: "Expense",
                schema: "dbo",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalNumber = table.Column<int>(type: "int", nullable: false),
                    ExpenseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExpenseCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    ExpenseCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpenseAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    ExpenseDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InvoiceImageFilePath = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DeciderAdminNumber = table.Column<int>(type: "int", nullable: true),
                    AdminNumber = table.Column<int>(type: "int", nullable: true),
                    DecisionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DecisionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_Expense_Admin_AdminNumber",
                        column: x => x.AdminNumber,
                        principalSchema: "dbo",
                        principalTable: "Admin",
                        principalColumn: "AdminNumber");
                    table.ForeignKey(
                        name: "FK_Expense_Personal_PersonalNumber",
                        column: x => x.PersonalNumber,
                        principalSchema: "dbo",
                        principalTable: "Personal",
                        principalColumn: "PersonalNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "dbo",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseId = table.Column<int>(type: "int", nullable: false),
                    IBAN = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_Expense_ExpenseId",
                        column: x => x.ExpenseId,
                        principalSchema: "dbo",
                        principalTable: "Expense",
                        principalColumn: "ExpenseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Admin",
                columns: new[] { "AdminNumber", "Email", "FirstName", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "ali.ilman@akbank.com", "Ali", "İlman", "Admin", "aliilman" },
                    { 2, "veli.liman@akbank.com", "Veli", "liman", "Admin", "veliliman" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Personal",
                columns: new[] { "PersonalNumber", "Email", "FirstName", "IBAN", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "ferdi.kadi@akbank.com", "Ferdi", "12345678981234456798", "Kadi", "personal", "ferdikadi" },
                    { 2, "Arda.gul@akbank.com", "Arda", "56412345678981234456798", "Gul", "personal", "ardagul" },
                    { 3, "sebastian.simanski@akbank.com", "Sebastian", "1233456789856451234456798", "simanski", "personal", "sebastiansimanski" },
                    { 4, "edin.ceko@akbank.com", "Edin", "12345678981541654234456798", "Ceko", "personal", "edinceko" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_AdminNumber",
                schema: "dbo",
                table: "Admin",
                column: "AdminNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expense_AdminNumber",
                schema: "dbo",
                table: "Expense",
                column: "AdminNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_ExpenseId",
                schema: "dbo",
                table: "Expense",
                column: "ExpenseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expense_PersonalNumber",
                schema: "dbo",
                table: "Expense",
                column: "PersonalNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ExpenseId",
                schema: "dbo",
                table: "Payment",
                column: "ExpenseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentId",
                schema: "dbo",
                table: "Payment",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personal_PersonalNumber",
                schema: "dbo",
                table: "Personal",
                column: "PersonalNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Expense",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Admin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Personal",
                schema: "dbo");
        }
    }
}
