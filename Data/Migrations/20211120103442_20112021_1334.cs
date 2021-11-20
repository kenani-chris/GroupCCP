using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class _20112021_1334 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffAccount_AspNetUsers_ApplicationUserId",
                table: "StaffAccount");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffAccount_AspNetUsers_ApplicationUserId",
                table: "StaffAccount",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
