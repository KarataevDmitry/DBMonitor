using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBMonitor.DAL.Migrations
{
    public partial class FixLHRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_History_RuleId",
                table: "History");

            migrationBuilder.CreateIndex(
                name: "IX_History_RuleId",
                table: "History",
                column: "RuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_History_RuleId",
                table: "History");

            migrationBuilder.CreateIndex(
                name: "IX_History_RuleId",
                table: "History",
                column: "RuleId",
                unique: true);
        }
    }
}
