using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class _01122021_1716 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplaintVehicleInfo");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "ComplaintLogDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationNumber",
                table: "ComplaintLogDetail",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Model",
                table: "ComplaintLogDetail");

            migrationBuilder.DropColumn(
                name: "RegistrationNumber",
                table: "ComplaintLogDetail");

            migrationBuilder.CreateTable(
                name: "ComplaintVehicleInfo",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    LogsLogId = table.Column<int>(type: "int", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintVehicleInfo", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_ComplaintVehicleInfo_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComplaintVehicleInfo_ComplaintLogDetail_LogsLogId",
                        column: x => x.LogsLogId,
                        principalTable: "ComplaintLogDetail",
                        principalColumn: "LogId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintVehicleInfo_BrandId",
                table: "ComplaintVehicleInfo",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintVehicleInfo_LogsLogId",
                table: "ComplaintVehicleInfo",
                column: "LogsLogId");
        }
    }
}
