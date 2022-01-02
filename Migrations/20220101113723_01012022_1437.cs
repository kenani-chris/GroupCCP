using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _01012022_1437 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "Entity", "Permission" },
                values: new object[] { 61, "Complaint", "Add" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "Entity", "Permission" },
                values: new object[] { 62, "Customer", "Add" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 62);
        }
    }
}
