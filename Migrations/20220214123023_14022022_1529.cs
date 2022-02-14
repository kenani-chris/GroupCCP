using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _14022022_1529 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VehicleMileage",
                table: "ComplaintLogDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress",
                table: "ComplaintCustomerInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleMileage",
                table: "ComplaintLogDetail");

            migrationBuilder.DropColumn(
                name: "CustomerAddress",
                table: "ComplaintCustomerInfo");
        }
    }
}
