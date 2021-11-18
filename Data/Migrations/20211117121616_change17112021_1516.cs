using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class change17112021_1516 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplaintLogStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintLogStatus", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintReceiveMeans",
                columns: table => new
                {
                    MeansId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Means = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintReceiveMeans", x => x.MeansId);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintLogDetail",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeansId = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    StatusClosedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintLogDetail", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_ComplaintLogDetail_ComplaintLogStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ComplaintLogStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplaintLogDetail_ComplaintReceiveMeans_MeansId",
                        column: x => x.MeansId,
                        principalTable: "ComplaintReceiveMeans",
                        principalColumn: "MeansId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplaintLogDetail_Level_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ComplaintLogStatus",
                columns: new[] { "StatusId", "Status" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "Assigned" },
                    { 3, "WIP" },
                    { 4, "Resolved" },
                    { 5, "Closed" },
                    { 6, "Hold" },
                    { 7, "Rejected" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintLogDetail_LevelId",
                table: "ComplaintLogDetail",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintLogDetail_MeansId",
                table: "ComplaintLogDetail",
                column: "MeansId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintLogDetail_StatusId",
                table: "ComplaintLogDetail",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplaintLogDetail");

            migrationBuilder.DropTable(
                name: "ComplaintLogStatus");

            migrationBuilder.DropTable(
                name: "ComplaintReceiveMeans");
        }
    }
}
