using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class _07122021_1248 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FollowUpDate",
                table: "ComplaintFollowUp",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FollowUpFeedback",
                table: "ComplaintFollowUp",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorrectiveInfoDate",
                table: "ComplaintCorrectiveInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowUpDate",
                table: "ComplaintFollowUp");

            migrationBuilder.DropColumn(
                name: "FollowUpFeedback",
                table: "ComplaintFollowUp");

            migrationBuilder.DropColumn(
                name: "CorrectiveInfoDate",
                table: "ComplaintCorrectiveInfo");
        }
    }
}
