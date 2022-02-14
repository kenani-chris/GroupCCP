using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _14022022_1631 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogKaizenImplememntDate",
                table: "ComplaintLogDetail",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogKaizenImplememntDate",
                table: "ComplaintLogDetail");
        }
    }
}
