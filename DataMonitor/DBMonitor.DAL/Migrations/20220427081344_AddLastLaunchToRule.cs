using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBMonitor.DAL.Migrations
{
    public partial class AddLastLaunchToRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLaunch",
                table: "Rules",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLaunch",
                table: "Rules");
        }
    }
}
