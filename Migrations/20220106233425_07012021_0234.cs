using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _07012021_0234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OverdueReminder",
                columns: table => new
                {
                    ReminderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverdueReminder", x => x.ReminderId);
                    table.ForeignKey(
                        name: "FK_OverdueReminder_ComplaintLogDetail_LogId",
                        column: x => x.LogId,
                        principalTable: "ComplaintLogDetail",
                        principalColumn: "LogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OverdueReminder_StaffAccount_StaffId",
                        column: x => x.StaffId,
                        principalTable: "StaffAccount",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OverdueReminder_LogId",
                table: "OverdueReminder",
                column: "LogId");

            migrationBuilder.CreateIndex(
                name: "IX_OverdueReminder_StaffId",
                table: "OverdueReminder",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OverdueReminder");
        }
    }
}
