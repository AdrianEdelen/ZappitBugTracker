using Microsoft.EntityFrameworkCore.Migrations;

namespace ZappitBugTracker.Migrations
{
    public partial class updatedTicketAttachmentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "TicketAttachments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "TicketAttachments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "TicketAttachments");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "TicketAttachments");
        }
    }
}
