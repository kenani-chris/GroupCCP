using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class change17112021_1615 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogCustomerId",
                table: "ComplaintLogDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ComplaintCustomerInfo",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    CustomerCell = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    CustomerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintCustomerInfo", x => x.CustomerId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintLogDetail_LogCustomerId",
                table: "ComplaintLogDetail",
                column: "LogCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintCustomerInfo_LogCustomerId",
                table: "ComplaintLogDetail",
                column: "LogCustomerId",
                principalTable: "ComplaintCustomerInfo",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintCustomerInfo_LogCustomerId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropTable(
                name: "ComplaintCustomerInfo");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintLogDetail_LogCustomerId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropColumn(
                name: "LogCustomerId",
                table: "ComplaintLogDetail");
        }
    }
}
