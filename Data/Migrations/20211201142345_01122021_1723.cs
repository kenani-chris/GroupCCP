using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class _01122021_1723 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "ComplaintLogDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintLogDetail_BrandId",
                table: "ComplaintLogDetail",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_Brands_BrandId",
                table: "ComplaintLogDetail",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_Brands_BrandId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintLogDetail_BrandId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "ComplaintLogDetail");
        }
    }
}
