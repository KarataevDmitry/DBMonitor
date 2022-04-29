using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBMonitor.DAL.Migrations
{
    public partial class AddNameToRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Rules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

        protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropColumn(
                name: "Name",
                table: "Rules");
    }
}
