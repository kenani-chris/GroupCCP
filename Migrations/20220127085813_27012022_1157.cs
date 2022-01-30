using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _27012022_1157 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "Entity", "Permission" },
                values: new object[,]
                {
                    { 115, "Admin - Levels", "List" },
                    { 116, "Admin - Levels", "Edit" },
                    { 117, "Admin - Levels", "Delete" },
                    { 118, "Admin - Levels", "Create" },
                    { 119, "Admin - Levels", "View" },
                    { 120, "Admin - LevelCategories", "List" },
                    { 121, "Admin - LevelCategories", "Edit" },
                    { 122, "Admin - LevelCategories", "Delete" },
                    { 123, "Admin - LevelCategories", "Create" },
                    { 124, "Admin - LevelCategories", "View" },
                    { 125, "Admin - StaffAccounts", "List" },
                    { 126, "Admin - StaffAccounts", "Edit" },
                    { 127, "Admin - StaffAccounts", "Delete" },
                    { 128, "Admin - StaffAccounts", "Create" },
                    { 129, "Admin - StaffAccounts", "View" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 129);
        }
    }
}
