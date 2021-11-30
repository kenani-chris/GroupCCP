using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class _30112021_1304 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "ComplaintCustomerInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintCustomerInfo_CompanyId",
                table: "ComplaintCustomerInfo",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintCustomerInfo_Company_CompanyId",
                table: "ComplaintCustomerInfo",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintCustomerInfo_Company_CompanyId",
                table: "ComplaintCustomerInfo");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintCustomerInfo_CompanyId",
                table: "ComplaintCustomerInfo");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ComplaintCustomerInfo");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
