using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZappitBugTracker.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Projects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Projects",
                type: "bytea",
                nullable: true);
        }
    }
}
