using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataHandler.CdrDbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddedFilterIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "Idx_call_detail_record_call_date",
                table: "call_detail_record",
                column: "call_date");

            migrationBuilder.CreateIndex(
                name: "Idx_call_detail_record_caller_reci_date",
                table: "call_detail_record",
                columns: new[] { "caller_id", "recipient", "call_date" });

            migrationBuilder.CreateIndex(
                name: "Idx_call_detail_record_caller_reci_date_du",
                table: "call_detail_record",
                columns: new[] { "caller_id", "recipient", "call_date", "duration" });

            migrationBuilder.CreateIndex(
                name: "Idx_call_detail_record_callerId",
                table: "call_detail_record",
                column: "caller_id");

            migrationBuilder.CreateIndex(
                name: "Idx_call_detail_record_cost",
                table: "call_detail_record",
                column: "cost");

            migrationBuilder.CreateIndex(
                name: "Idx_call_detail_record_duration",
                table: "call_detail_record",
                column: "duration");

            migrationBuilder.CreateIndex(
                name: "Idx_call_detail_record_recipient",
                table: "call_detail_record",
                column: "recipient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Idx_call_detail_record_call_date",
                table: "call_detail_record");

            migrationBuilder.DropIndex(
                name: "Idx_call_detail_record_caller_reci_date",
                table: "call_detail_record");

            migrationBuilder.DropIndex(
                name: "Idx_call_detail_record_caller_reci_date_du",
                table: "call_detail_record");

            migrationBuilder.DropIndex(
                name: "Idx_call_detail_record_callerId",
                table: "call_detail_record");

            migrationBuilder.DropIndex(
                name: "Idx_call_detail_record_cost",
                table: "call_detail_record");

            migrationBuilder.DropIndex(
                name: "Idx_call_detail_record_duration",
                table: "call_detail_record");

            migrationBuilder.DropIndex(
                name: "Idx_call_detail_record_recipient",
                table: "call_detail_record");
        }
    }
}
