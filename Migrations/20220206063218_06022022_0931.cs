using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _06022022_0931 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "Entity", "Permission" },
                values: new object[,]
                {
                    { 130, "Admin - LevelMemberships", "List" },
                    { 131, "Admin - LevelMemberships", "Edit" },
                    { 132, "Admin - LevelMemberships", "Delete" },
                    { 133, "Admin - LevelMemberships", "Create" },
                    { 134, "Admin - LevelMemberships", "View" },
                    { 135, "Admin - RoleAssignments", "List" },
                    { 136, "Admin - RoleAssignments", "Edit" },
                    { 137, "Admin - RoleAssignments", "Delete" },
                    { 138, "Admin - RoleAssignments", "Create" },
                    { 139, "Admin - RoleAssignments", "View" },
                    { 140, "Admin - PermissionAssignments", "List" },
                    { 141, "Admin - PermissionAssignments", "Edit" },
                    { 142, "Admin - PermissionAssignments", "Delete" },
                    { 143, "Admin - PermissionAssignments", "Create" },
                    { 144, "Admin - PermissionAssignments", "View" },
                    { 145, "Admin - Timelines", "List" },
                    { 146, "Admin - Timelines", "Edit" },
                    { 147, "Admin - Timelines", "Delete" },
                    { 148, "Admin - Timelines", "Create" },
                    { 149, "Admin - Timelines", "View" },
                    { 150, "Admin - Companies", "List" },
                    { 151, "Admin - Companies", "Edit" },
                    { 152, "Admin - Companies", "Delete" },
                    { 153, "Admin - Companies", "Create" },
                    { 154, "Admin - Companies", "View" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 154);
        }
    }
}
