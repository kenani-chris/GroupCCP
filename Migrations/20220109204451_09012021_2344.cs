using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _09012021_2344 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriorityId",
                table: "Timelines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriorityId",
                table: "ComplaintLogDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Priority",
                columns: table => new
                {
                    PriorityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriorityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityCloseDate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.PriorityId);
                });

            migrationBuilder.InsertData(
                table: "Priority",
                columns: new[] { "PriorityId", "PriorityCloseDate", "PriorityName" },
                values: new object[,]
                {
                    { 1, 4f, "Critical" },
                    { 2, 8f, "High" },
                    { 3, 24f, "Normal" },
                    { 4, 48f, "Low" },
                    { 5, 0f, "Extremely Low" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Timelines_PriorityId",
                table: "Timelines",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintLogDetail_PriorityId",
                table: "ComplaintLogDetail",
                column: "PriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_Priority_PriorityId",
                table: "ComplaintLogDetail",
                column: "PriorityId",
                principalTable: "Priority",
                principalColumn: "PriorityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Timelines_Priority_PriorityId",
                table: "Timelines",
                column: "PriorityId",
                principalTable: "Priority",
                principalColumn: "PriorityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_Priority_PriorityId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Timelines_Priority_PriorityId",
                table: "Timelines");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropIndex(
                name: "IX_Timelines_PriorityId",
                table: "Timelines");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintLogDetail_PriorityId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "Timelines");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "ComplaintLogDetail");
        }
    }
}
