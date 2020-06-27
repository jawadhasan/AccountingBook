using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingBook.Data.Migrations
{
    public partial class ParentAccountSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountCode", "AccountName", "AccountType", "DrOrCrSide", "ParentAccountId" },
                values: new object[,]
                {
                    { 1L, 10000, "Assets", 1, 1, null },
                    { 2L, 20000, "Liabilities", 2, 2, null },
                    { 3L, 30000, "Equity", 3, 2, null },
                    { 4L, 40000, "Revenue", 4, 2, null },
                    { 5L, 50000, "Expense", 5, 1, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5L);
        }
    }
}
