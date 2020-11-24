using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZappitBugTracker.Migrations
{
    public partial class removedRequiredFromFileDataInTicketAttachMents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "FileData",
                table: "TicketAttachments",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "FileData",
                table: "TicketAttachments",
                type: "bytea",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }
    }
}
