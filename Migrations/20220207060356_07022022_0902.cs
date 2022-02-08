using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _07022022_0902 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_Brands_BrandId",
                table: "ComplaintLogDetail");

            migrationBuilder.RenameColumn(
                name: "RegistrationNumber",
                table: "ComplaintLogDetail",
                newName: "VehicleOnSite");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "ComplaintLogDetail",
                newName: "LogPreventiveAction");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "ComplaintLogDetail",
                newName: "VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintLogDetail_BrandId",
                table: "ComplaintLogDetail",
                newName: "IX_ComplaintLogDetail_VehicleId");

            migrationBuilder.AddColumn<bool>(
                name: "LogCustomerSatisfied",
                table: "ComplaintLogDetail",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LogKaizenAction",
                table: "ComplaintLogDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerCompany",
                table: "ComplaintCustomerInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "ComplaintCustomerInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerOccupation",
                table: "ComplaintCustomerInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComplaintSubComponent",
                table: "ComplaintCorrectiveInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CorrectiveComponentId",
                table: "ComplaintCorrectiveInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CorrectiveCustomerExplanation",
                table: "ComplaintCorrectiveInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorrectiveDiagnosisTimeTaken",
                table: "ComplaintCorrectiveInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CorrectiveOtherCostKSH",
                table: "ComplaintCorrectiveInfo",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "CorrectivePartsCostKSH",
                table: "ComplaintCorrectiveInfo",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "CorrectiveRectifyTimeTaken",
                table: "ComplaintCorrectiveInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComplaintProductComponent",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductComponent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintProductComponent", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintResponsibility",
                columns: table => new
                {
                    ResponsibilityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    ResponsibilityPIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibilityLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibilityReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintResponsibility", x => x.ResponsibilityId);
                    table.ForeignKey(
                        name: "FK_ComplaintResponsibility_ComplaintLogDetail_LogId",
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
                    VehicleBrandId = table.Column<int>(type: "int", nullable: false),
                    VehicleModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehilcleVIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehiclePurchaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintVehicleInfo", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_ComplaintVehicleInfo_Brands_VehicleBrandId",
                        column: x => x.VehicleBrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintCorrectiveInfo_CorrectiveComponentId",
                table: "ComplaintCorrectiveInfo",
                column: "CorrectiveComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintResponsibility_LogId",
                table: "ComplaintResponsibility",
                column: "LogId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintVehicleInfo_VehicleBrandId",
                table: "ComplaintVehicleInfo",
                column: "VehicleBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintCorrectiveInfo_ComplaintProductComponent_CorrectiveComponentId",
                table: "ComplaintCorrectiveInfo",
                column: "CorrectiveComponentId",
                principalTable: "ComplaintProductComponent",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintVehicleInfo_VehicleId",
                table: "ComplaintLogDetail",
                column: "VehicleId",
                principalTable: "ComplaintVehicleInfo",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintCorrectiveInfo_ComplaintProductComponent_CorrectiveComponentId",
                table: "ComplaintCorrectiveInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintVehicleInfo_VehicleId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropTable(
                name: "ComplaintProductComponent");

            migrationBuilder.DropTable(
                name: "ComplaintResponsibility");

            migrationBuilder.DropTable(
                name: "ComplaintVehicleInfo");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintCorrectiveInfo_CorrectiveComponentId",
                table: "ComplaintCorrectiveInfo");

            migrationBuilder.DropColumn(
                name: "LogCustomerSatisfied",
                table: "ComplaintLogDetail");

            migrationBuilder.DropColumn(
                name: "LogKaizenAction",
                table: "ComplaintLogDetail");

            migrationBuilder.DropColumn(
                name: "CustomerCompany",
                table: "ComplaintCustomerInfo");

            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "ComplaintCustomerInfo");

            migrationBuilder.DropColumn(
                name: "CustomerOccupation",
                table: "ComplaintCustomerInfo");

            migrationBuilder.DropColumn(
                name: "ComplaintSubComponent",
                table: "ComplaintCorrectiveInfo");

            migrationBuilder.DropColumn(
                name: "CorrectiveComponentId",
                table: "ComplaintCorrectiveInfo");

            migrationBuilder.DropColumn(
                name: "CorrectiveCustomerExplanation",
                table: "ComplaintCorrectiveInfo");

            migrationBuilder.DropColumn(
                name: "CorrectiveDiagnosisTimeTaken",
                table: "ComplaintCorrectiveInfo");

            migrationBuilder.DropColumn(
                name: "CorrectiveOtherCostKSH",
                table: "ComplaintCorrectiveInfo");

            migrationBuilder.DropColumn(
                name: "CorrectivePartsCostKSH",
                table: "ComplaintCorrectiveInfo");

            migrationBuilder.DropColumn(
                name: "CorrectiveRectifyTimeTaken",
                table: "ComplaintCorrectiveInfo");

            migrationBuilder.RenameColumn(
                name: "VehicleOnSite",
                table: "ComplaintLogDetail",
                newName: "RegistrationNumber");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "ComplaintLogDetail",
                newName: "BrandId");

            migrationBuilder.RenameColumn(
                name: "LogPreventiveAction",
                table: "ComplaintLogDetail",
                newName: "Model");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintLogDetail_VehicleId",
                table: "ComplaintLogDetail",
                newName: "IX_ComplaintLogDetail_BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_Brands_BrandId",
                table: "ComplaintLogDetail",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
