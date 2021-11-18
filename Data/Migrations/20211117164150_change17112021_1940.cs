using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class change17112021_1940 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogId",
                table: "ComplaintAssignment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                    table.ForeignKey(
                        name: "FK_Brands_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintCorrectiveInfo",
                columns: table => new
                {
                    CorrectiveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    RouteCause = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    CorrectiveAction = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintCorrectiveInfo", x => x.CorrectiveId);
                    table.ForeignKey(
                        name: "FK_ComplaintCorrectiveInfo_ComplaintLogDetail_LogId",
                        column: x => x.LogId,
                        principalTable: "ComplaintLogDetail",
                        principalColumn: "LogId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintVehicleInfo",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    LogsLogId = table.Column<int>(type: "int", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_ComplaintAssignment_LogId",
                table: "ComplaintAssignment",
                column: "LogId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_CompanyId",
                table: "Brands",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintCorrectiveInfo_LogId",
                table: "ComplaintCorrectiveInfo",
                column: "LogId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintVehicleInfo_BrandId",
                table: "ComplaintVehicleInfo",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintVehicleInfo_LogsLogId",
                table: "ComplaintVehicleInfo",
                column: "LogsLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintAssignment_ComplaintLogDetail_LogId",
                table: "ComplaintAssignment",
                column: "LogId",
                principalTable: "ComplaintLogDetail",
                principalColumn: "LogId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintAssignment_ComplaintLogDetail_LogId",
                table: "ComplaintAssignment");

            migrationBuilder.DropTable(
                name: "ComplaintCorrectiveInfo");

            migrationBuilder.DropTable(
                name: "ComplaintVehicleInfo");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintAssignment_LogId",
                table: "ComplaintAssignment");

            migrationBuilder.DropColumn(
                name: "LogId",
                table: "ComplaintAssignment");
        }
    }
}
