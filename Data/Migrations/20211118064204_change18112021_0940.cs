using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class change18112021_0940 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FollowUpCalls",
                columns: table => new
                {
                    FollowUpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowUpTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FollowUpType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUpCalls", x => x.FollowUpId);
                    table.ForeignKey(
                        name: "FK_FollowUpCalls_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintFollowUp",
                columns: table => new
                {
                    FollowUpId = table.Column<int>(type: "int", nullable: false),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    FollowUp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintFollowUp", x => x.FollowUpId);
                    table.ForeignKey(
                        name: "FK_ComplaintFollowUp_ComplaintLogDetail_LogId",
                        column: x => x.LogId,
                        principalTable: "ComplaintLogDetail",
                        principalColumn: "LogId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComplaintFollowUp_FollowUpCalls_FollowUpId",
                        column: x => x.FollowUpId,
                        principalTable: "FollowUpCalls",
                        principalColumn: "FollowUpId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComplaintFollowUp_StaffAccount_StaffId",
                        column: x => x.StaffId,
                        principalTable: "StaffAccount",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintFollowUp_LogId",
                table: "ComplaintFollowUp",
                column: "LogId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintFollowUp_StaffId",
                table: "ComplaintFollowUp",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpCalls_CompanyId",
                table: "FollowUpCalls",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplaintFollowUp");

            migrationBuilder.DropTable(
                name: "FollowUpCalls");
        }
    }
}
