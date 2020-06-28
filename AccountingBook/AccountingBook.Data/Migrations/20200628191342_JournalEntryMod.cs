using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingBook.Data.Migrations
{
    public partial class JournalEntryMod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GeneralLedgerHeaderId",
                table: "JournalEntryHeaders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryHeaders_GeneralLedgerHeaderId",
                table: "JournalEntryHeaders",
                column: "GeneralLedgerHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntryHeaders_GeneralLedgerHeaders_GeneralLedgerHeade~",
                table: "JournalEntryHeaders",
                column: "GeneralLedgerHeaderId",
                principalTable: "GeneralLedgerHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntryHeaders_GeneralLedgerHeaders_GeneralLedgerHeade~",
                table: "JournalEntryHeaders");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntryHeaders_GeneralLedgerHeaderId",
                table: "JournalEntryHeaders");

            migrationBuilder.DropColumn(
                name: "GeneralLedgerHeaderId",
                table: "JournalEntryHeaders");
        }
    }
}
