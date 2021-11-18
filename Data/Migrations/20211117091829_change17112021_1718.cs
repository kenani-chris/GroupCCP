using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class change17112021_1718 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Level_ParentId",
                table: "Level",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Level_Level_ParentId",
                table: "Level",
                column: "ParentId",
                principalTable: "Level",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Level_Level_ParentId",
                table: "Level");

            migrationBuilder.DropIndex(
                name: "IX_Level_ParentId",
                table: "Level");
        }
    }
}
