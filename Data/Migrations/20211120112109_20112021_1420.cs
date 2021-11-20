using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class _20112021_1420 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffAccount_AspNetUsers_UserId",
                table: "StaffAccount");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffAccount_AspNetUsers_UserId",
                table: "StaffAccount",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffAccount_AspNetUsers_UserId",
                table: "StaffAccount");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffAccount_AspNetUsers_UserId",
                table: "StaffAccount",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
