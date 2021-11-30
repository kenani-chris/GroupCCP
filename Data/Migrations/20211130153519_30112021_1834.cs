using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class _30112021_1834 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusSubmitDate",
                table: "ComplaintLogDetail",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusSubmitDate",
                table: "ComplaintLogDetail");
        }
    }
}
