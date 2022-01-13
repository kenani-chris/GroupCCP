using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _05012021_1647 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Timelines",
                columns: table => new
                {
                    TimeLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedToWIP = table.Column<float>(type: "real", nullable: false),
                    AssignedToWIPEscallate = table.Column<bool>(type: "bit", nullable: false),
                    WIPToResolved = table.Column<float>(type: "real", nullable: false),
                    WIPToResolvedEscallate = table.Column<bool>(type: "bit", nullable: false),
                    ResolvedToClose = table.Column<float>(type: "real", nullable: false),
                    ResolvedToCloseEscallate = table.Column<float>(type: "real", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timelines", x => x.TimeLineId);
                    table.ForeignKey(
                        name: "FK_Timelines_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Timelines_CompanyId",
                table: "Timelines",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timelines");
        }
    }
}
