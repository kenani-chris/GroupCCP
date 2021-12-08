using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class _06122021_1350 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FollowUpManadatory",
                table: "FollowUpCalls",
                newName: "FollowUpMandatory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FollowUpMandatory",
                table: "FollowUpCalls",
                newName: "FollowUpManadatory");
        }
    }
}
