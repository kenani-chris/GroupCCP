using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class _18112021_1502 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffAccount_AspNetUsers_UserId",
                table: "StaffAccount");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "StaffAccount",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffAccount_UserId",
                table: "StaffAccount",
                newName: "IX_StaffAccount_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffAccount_AspNetUsers_ApplicationUserId",
                table: "StaffAccount",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffAccount_AspNetUsers_ApplicationUserId",
                table: "StaffAccount");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "StaffAccount",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffAccount_ApplicationUserId",
                table: "StaffAccount",
                newName: "IX_StaffAccount_UserId");

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
