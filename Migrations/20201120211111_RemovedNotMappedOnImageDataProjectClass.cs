using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZappitBugTracker.Migrations
{
    public partial class RemovedNotMappedOnImageDataProjectClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Projects");
        }
    }
}
