using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _16122021_1620 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FollowUpTypeId",
                table: "ComplaintFollowUp",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintFollowUp_FollowUpTypeId",
                table: "ComplaintFollowUp",
                column: "FollowUpTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintFollowUp_FollowUpCalls_FollowUpTypeId",
                table: "ComplaintFollowUp",
                column: "FollowUpTypeId",
                principalTable: "FollowUpCalls",
                principalColumn: "FollowUpId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintFollowUp_FollowUpCalls_FollowUpTypeId",
                table: "ComplaintFollowUp");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintFollowUp_FollowUpTypeId",
                table: "ComplaintFollowUp");

            migrationBuilder.DropColumn(
                name: "FollowUpTypeId",
                table: "ComplaintFollowUp");
        }
    }
}
