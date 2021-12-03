using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class _02122021_0852 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "ComplaintLogDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintLogDetail_StaffId",
                table: "ComplaintLogDetail",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_StaffAccount_StaffId",
                table: "ComplaintLogDetail",
                column: "StaffId",
                principalTable: "StaffAccount",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_StaffAccount_StaffId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintLogDetail_StaffId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "ComplaintLogDetail");
        }
    }
}
