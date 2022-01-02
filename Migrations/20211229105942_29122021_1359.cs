using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _29122021_1359 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LevelMemberships",
                columns: table => new
                {
                    MembershipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    MembershipRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MembershipActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelMemberships", x => x.MembershipId);
                    table.ForeignKey(
                        name: "FK_LevelMemberships_Level_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LevelMemberships_StaffAccount_StaffId",
                        column: x => x.StaffId,
                        principalTable: "StaffAccount",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LevelMemberships_LevelId",
                table: "LevelMemberships",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelMemberships_StaffId",
                table: "LevelMemberships",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LevelMemberships");
        }
    }
}
