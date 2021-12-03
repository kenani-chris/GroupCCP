using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class _02122021_1832 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "ComplaintCorrectiveInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintCorrectiveInfo_StaffId",
                table: "ComplaintCorrectiveInfo",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintCorrectiveInfo_StaffAccount_StaffId",
                table: "ComplaintCorrectiveInfo",
                column: "StaffId",
                principalTable: "StaffAccount",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintCorrectiveInfo_StaffAccount_StaffId",
                table: "ComplaintCorrectiveInfo");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintCorrectiveInfo_StaffId",
                table: "ComplaintCorrectiveInfo");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "ComplaintCorrectiveInfo");
        }
    }
}
