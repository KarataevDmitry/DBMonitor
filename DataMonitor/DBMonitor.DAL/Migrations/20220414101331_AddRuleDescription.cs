using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBMonitor.DAL.Migrations
{
    public partial class AddRuleDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

        protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropColumn(
                name: "Description",
                table: "Rules");
    }
}
