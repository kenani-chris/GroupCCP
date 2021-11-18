using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class change17112021_1716 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Level_Level_ParentLevelLevelId",
                table: "Level");

            migrationBuilder.DropIndex(
                name: "IX_Level_ParentLevelLevelId",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "ParentLevelLevelId",
                table: "Level");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentLevelLevelId",
                table: "Level",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Level_ParentLevelLevelId",
                table: "Level",
                column: "ParentLevelLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Level_Level_ParentLevelLevelId",
                table: "Level",
                column: "ParentLevelLevelId",
                principalTable: "Level",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
