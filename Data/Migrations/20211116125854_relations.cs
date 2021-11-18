using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LevelCategory",
                columns: table => new
                {
                    LevelCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategorName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelCategory", x => x.LevelCategoryId);
                    table.ForeignKey(
                        name: "FK_LevelCategory_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LevelCategory_LevelCategory_ParentId",
                        column: x => x.ParentId,
                        principalTable: "LevelCategory",
                        principalColumn: "LevelCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    LevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ParentLevelLevelId = table.Column<int>(type: "int", nullable: true),
                    LevelCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.LevelId);
                    table.ForeignKey(
                        name: "FK_Level_Level_ParentLevelLevelId",
                        column: x => x.ParentLevelLevelId,
                        principalTable: "Level",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Level_LevelCategory_LevelCategoryId",
                        column: x => x.LevelCategoryId,
                        principalTable: "LevelCategory",
                        principalColumn: "LevelCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Level_LevelCategoryId",
                table: "Level",
                column: "LevelCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Level_ParentLevelLevelId",
                table: "Level",
                column: "ParentLevelLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelCategory_CompanyId",
                table: "LevelCategory",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelCategory_ParentId",
                table: "LevelCategory",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropTable(
                name: "LevelCategory");
        }
    }
}
