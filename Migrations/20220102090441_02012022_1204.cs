using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _02012022_1204 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "Entity", "Permission" },
                values: new object[,]
                {
                    { 63, "FolllowUp - Submitted Complaints", "View" },
                    { 64, "Corrective - Submitted Complaints", "View" },
                    { 65, "Assignment - Submitted Complaints", "View" },
                    { 66, "FolllowUp - My Complaints", "View" },
                    { 67, "Corrective - My Complaints", "View" },
                    { 68, "Assignment - My Complaints", "View" },
                    { 69, "FolllowUp - Escallated Complaints", "View" },
                    { 70, "Corrective - Escallated Complaints", "View" },
                    { 71, "Assignment - Escallated Complaints", "View" },
                    { 72, "FolllowUp - Level Down Complaints", "View" },
                    { 73, "Corrective - Level Down Complaints", "View" },
                    { 76, "Assignment - Level Down Complaints", "View" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 76);
        }
    }
}
