using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _07022022_1050 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ComplaintProductComponent",
                columns: new[] { "ProductID", "ProductComponent" },
                values: new object[,]
                {
                    { 1, "Unknown" },
                    { 2, "Engine" },
                    { 3, "Clutch and Transmission" },
                    { 4, "Chasis" },
                    { 5, "Steering and Tyre" },
                    { 6, "Brake" },
                    { 7, "Body" },
                    { 8, "Body Interior" },
                    { 9, "Door/Window/Sunroof" },
                    { 10, "Electrical" },
                    { 11, "Hybrid ElectVehicle" },
                    { 12, "Special Vehicle" },
                    { 13, "Accessories" },
                    { 14, "Other" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ComplaintProductComponent",
                keyColumn: "ProductID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ComplaintProductComponent",
                keyColumn: "ProductID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ComplaintProductComponent",
                keyColumn: "ProductID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ComplaintProductComponent",
                keyColumn: "ProductID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ComplaintProductComponent",
                keyColumn: "ProductID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ComplaintProductComponent",
                keyColumn: "ProductID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ComplaintProductComponent",
                keyColumn: "ProductID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ComplaintProductComponent",
                keyColumn: "ProductID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ComplaintProductComponent",
                keyColumn: "ProductID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ComplaintProductComponent",
                keyColumn: "ProductID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ComplaintProductComponent",
                keyColumn: "ProductID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ComplaintProductComponent",
                keyColumn: "ProductID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ComplaintProductComponent",
                keyColumn: "ProductID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ComplaintProductComponent",
                keyColumn: "ProductID",
                keyValue: 14);
        }
    }
}
