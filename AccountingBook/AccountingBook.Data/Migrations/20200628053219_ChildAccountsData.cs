using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingBook.Data.Migrations
{
    public partial class ChildAccountsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountCode", "AccountName", "AccountType", "DrOrCrSide", "ParentAccountId" },
                values: new object[,]
                {
                    { 6L, 10111, "Regular Checking Account", 1, 1, 1L },
                    { 30L, 50500, "Purchase price Variance", 5, 1, 5L },
                    { 29L, 50400, "Purchase Discounts", 5, 1, 5L },
                    { 28L, 50300, "Cost of Goods Sold", 5, 1, 5L },
                    { 27L, 50200, "Purchase A/C", 5, 1, 5L },
                    { 26L, 50101, "Salary Expenses", 5, 1, 5L },
                    { 25L, 40500, "Shipping and Handling", 4, 2, 4L },
                    { 24L, 40200, "Sales Discounts", 4, 2, 4L },
                    { 23L, 40100, "Sales A/C", 4, 2, 4L },
                    { 22L, 30500, "Accumulated Losses", 3, 2, 3L },
                    { 21L, 30400, "Accumulated Profits", 3, 2, 3L },
                    { 20L, 30300, "Retained Surplus", 3, 2, 3L },
                    { 31L, 50600, "Other Expenses", 5, 1, 5L },
                    { 19L, 30200, "Capital Surplus", 3, 2, 3L },
                    { 17L, 20300, "Sales Tax", 2, 2, 2L },
                    { 16L, 20202, "Wages Payable", 2, 2, 2L },
                    { 15L, 20120, "Customer Advances", 2, 2, 2L },
                    { 14L, 20110, "Account Payable", 2, 2, 2L },
                    { 13L, 10810, "Goods Received Clearing Account", 1, 1, 1L },
                    { 12L, 10800, "Inventory", 1, 1, 1L },
                    { 11L, 10150, "Employee Advances", 1, 1, 1L },
                    { 10L, 10140, "Prepaid Expenses", 1, 1, 1L },
                    { 9L, 10120, "Accounts Receivable", 1, 1, 1L },
                    { 8L, 10113, "Cash in Hand A/C", 1, 1, 1L },
                    { 7L, 10112, "Savings Account", 1, 1, 1L },
                    { 18L, 30100, "Member Capital", 3, 2, 3L },
                    { 32L, 50700, "Purchase Tax", 5, 1, 5L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 32L);
        }
    }
}
