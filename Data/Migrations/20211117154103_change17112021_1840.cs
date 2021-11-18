using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class change17112021_1840 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintAssignment_StaffAccount_StaffAssigned",
                table: "ComplaintAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintCustomerInfo_LogCustomerId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintLogStatus_LogStatusId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintReceiveMeans_LogMeansId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_Level_LogLevelId",
                table: "ComplaintLogDetail");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "StaffAccount",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Entity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Permission = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_Roles_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionAssignment",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionAssignment", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_PermissionAssignment_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionAssignment_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleAssignment",
                columns: table => new
                {
                    RoleAssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAssignment", x => x.RoleAssignmentId);
                    table.ForeignKey(
                        name: "FK_RoleAssignment_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleAssignment_StaffAccount_StaffId",
                        column: x => x.StaffId,
                        principalTable: "StaffAccount",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionAssignment_PermissionId",
                table: "PermissionAssignment",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionAssignment_RoleId",
                table: "PermissionAssignment",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAssignment_RoleID",
                table: "RoleAssignment",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAssignment_StaffId",
                table: "RoleAssignment",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CompanyId",
                table: "Roles",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintAssignment_StaffAccount_StaffAssigned",
                table: "ComplaintAssignment",
                column: "StaffAssigned",
                principalTable: "StaffAccount",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintCustomerInfo_LogCustomerId",
                table: "ComplaintLogDetail",
                column: "LogCustomerId",
                principalTable: "ComplaintCustomerInfo",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintLogStatus_LogStatusId",
                table: "ComplaintLogDetail",
                column: "LogStatusId",
                principalTable: "ComplaintLogStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintReceiveMeans_LogMeansId",
                table: "ComplaintLogDetail",
                column: "LogMeansId",
                principalTable: "ComplaintReceiveMeans",
                principalColumn: "MeansId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_Level_LogLevelId",
                table: "ComplaintLogDetail",
                column: "LogLevelId",
                principalTable: "Level",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintAssignment_StaffAccount_StaffAssigned",
                table: "ComplaintAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintCustomerInfo_LogCustomerId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintLogStatus_LogStatusId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintReceiveMeans_LogMeansId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_Level_LogLevelId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropTable(
                name: "PermissionAssignment");

            migrationBuilder.DropTable(
                name: "RoleAssignment");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "StaffAccount",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintAssignment_StaffAccount_StaffAssigned",
                table: "ComplaintAssignment",
                column: "StaffAssigned",
                principalTable: "StaffAccount",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintCustomerInfo_LogCustomerId",
                table: "ComplaintLogDetail",
                column: "LogCustomerId",
                principalTable: "ComplaintCustomerInfo",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintLogStatus_LogStatusId",
                table: "ComplaintLogDetail",
                column: "LogStatusId",
                principalTable: "ComplaintLogStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintReceiveMeans_LogMeansId",
                table: "ComplaintLogDetail",
                column: "LogMeansId",
                principalTable: "ComplaintReceiveMeans",
                principalColumn: "MeansId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_Level_LogLevelId",
                table: "ComplaintLogDetail",
                column: "LogLevelId",
                principalTable: "Level",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
