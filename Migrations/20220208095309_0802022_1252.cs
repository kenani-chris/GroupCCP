using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _0802022_1252 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "Entity", "Permission" },
                values: new object[,]
                {
                    { 161, "Responsibility - Submitted Complaints", "View" },
                    { 162, "Responsibility - Submitted Complaints", "Add" },
                    { 163, "Responsibility - Submitted Complaints", "Edit" },
                    { 164, "Responsibility - Submitted Complaints", "Delete" },
                    { 165, "Responsibility - My Complaints", "View" },
                    { 166, "Responsibility - My Complaints", "Add" },
                    { 167, "Responsibility - My Complaints", "Edit" },
                    { 168, "Responsibility - My Complaints", "Delete" },
                    { 169, "Responsibility - Escallated Complaints", "View" },
                    { 170, "Responsibility - Escallated Complaints", "Add" },
                    { 171, "Responsibility - Escallated Complaints", "Edit" },
                    { 172, "Responsibility - Escallated Complaints", "Delete" },
                    { 173, "Responsibility - Level Down Complaints", "View" },
                    { 174, "Responsibility - Level Down Complaints", "Add" },
                    { 175, "Responsibility - Level Down Complaints", "Edit" },
                    { 176, "Responsibility - Level Down Complaints", "Delete" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 176);
        }
    }
}
