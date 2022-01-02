using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _22122021_1640 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "Entity", "Permission" },
                values: new object[,]
                {
                    { 1, "Complaint - Submitted Complaints", "List" },
                    { 2, "Complaint - Submitted Complaints", "Create" },
                    { 3, "Complaint - Submitted Complaints", "Edit" },
                    { 4, "Complaint - Submitted Complaints", "Delete" },
                    { 5, "Complaint - Submitted Complaints", "Read" },
                    { 6, "Complaint - Submitted Complaints", "Export" },
                    { 7, "FolllowUp - Submitted Complaints", "Add" },
                    { 8, "FolllowUp - Submitted Complaints", "Edit" },
                    { 9, "FolllowUp - Submitted Complaints", "Delete" },
                    { 10, "Corrective - Submitted Complaints", "Add" },
                    { 11, "Corrective - Submitted Complaints", "Edit" },
                    { 12, "Corrective - Submitted Complaints", "Delete" },
                    { 13, "Assignment - Submitted Complaints", "Add" },
                    { 14, "Assignment - Submitted Complaints", "Edit" },
                    { 15, "Assignment - Submitted Complaints", "Delete" },
                    { 16, "Complaint - My Complaints", "List" },
                    { 17, "Complaint - My Complaints", "Create" },
                    { 18, "Complaint - My Complaints", "Edit" },
                    { 19, "Complaint - My Complaints", "Delete" },
                    { 20, "Complaint - My Complaints", "Read" },
                    { 21, "Complaint - My Complaints", "Export" },
                    { 22, "FolllowUp - My Complaints", "Add" },
                    { 23, "FolllowUp - My Complaints", "Edit" },
                    { 24, "FolllowUp - My Complaints", "Delete" },
                    { 25, "Corrective - My Complaints", "Add" },
                    { 26, "Corrective - My Complaints", "Edit" },
                    { 27, "Corrective - My Complaints", "Delete" },
                    { 28, "Assignment - My Complaints", "Add" },
                    { 29, "Assignment - My Complaints", "Edit" },
                    { 30, "Assignment - My Complaints", "Delete" },
                    { 31, "Complaint - Escallated Complaints", "List" },
                    { 32, "Complaint - Escallated Complaints", "Create" },
                    { 33, "Complaint - Escallated Complaints", "Edit" },
                    { 34, "Complaint - Escallated Complaints", "Delete" },
                    { 35, "Complaint - Escallated Complaints", "Read" },
                    { 36, "Complaint - Escallated Complaints", "Export" },
                    { 37, "FolllowUp - Escallated Complaints", "Add" },
                    { 38, "FolllowUp - Escallated Complaints", "Edit" },
                    { 39, "FolllowUp - Escallated Complaints", "Delete" },
                    { 40, "Corrective - Escallated Complaints", "Add" },
                    { 41, "Corrective - Escallated Complaints", "Edit" },
                    { 42, "Corrective - Escallated Complaints", "Delete" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "Entity", "Permission" },
                values: new object[,]
                {
                    { 43, "Assignment - Escallated Complaints", "Add" },
                    { 44, "Assignment - Escallated Complaints", "Edit" },
                    { 45, "Assignment - Escallated Complaints", "Delete" },
                    { 46, "Complaint - Level Down Complaints", "List" },
                    { 47, "Complaint - Level Down Complaints", "Create" },
                    { 48, "Complaint - Level Down Complaints", "Edit" },
                    { 49, "Complaint - Level Down Complaints", "Delete" },
                    { 50, "Complaint - Level Down Complaints", "Read" },
                    { 51, "Complaint - Level Down Complaints", "Export" },
                    { 52, "FolllowUp - Level Down Complaints", "Add" },
                    { 53, "FolllowUp - Level Down Complaints", "Edit" },
                    { 54, "FolllowUp - Level Down Complaints", "Delete" },
                    { 55, "Corrective - Level Down Complaints", "Add" },
                    { 56, "Corrective - Level Down Complaints", "Edit" },
                    { 57, "Corrective - Level Down Complaints", "Delete" },
                    { 58, "Assignment - Level Down Complaints", "Add" },
                    { 59, "Assignment - Level Down Complaints", "Edit" },
                    { 60, "Assignment - Level Down Complaints", "Delete" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                table: "Permissions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
