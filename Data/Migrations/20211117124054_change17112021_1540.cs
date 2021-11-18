using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCCP.Data.Migrations
{
    public partial class change17112021_1540 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintLogStatus_StatusId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintReceiveMeans_MeansId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_Level_LevelId",
                table: "ComplaintLogDetail");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "ComplaintLogDetail",
                newName: "LogStatusId");

            migrationBuilder.RenameColumn(
                name: "MeansId",
                table: "ComplaintLogDetail",
                newName: "LogMeansId");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "ComplaintLogDetail",
                newName: "LogLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintLogDetail_StatusId",
                table: "ComplaintLogDetail",
                newName: "IX_ComplaintLogDetail_LogStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintLogDetail_MeansId",
                table: "ComplaintLogDetail",
                newName: "IX_ComplaintLogDetail_LogMeansId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintLogDetail_LevelId",
                table: "ComplaintLogDetail",
                newName: "IX_ComplaintLogDetail_LogLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintLogStatus_LogStatusId",
                table: "ComplaintLogDetail",
                column: "LogStatusId",
                principalTable: "ComplaintLogStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintReceiveMeans_LogMeansId",
                table: "ComplaintLogDetail",
                column: "LogMeansId",
                principalTable: "ComplaintReceiveMeans",
                principalColumn: "MeansId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_Level_LogLevelId",
                table: "ComplaintLogDetail",
                column: "LogLevelId",
                principalTable: "Level",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintLogStatus_LogStatusId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintReceiveMeans_LogMeansId",
                table: "ComplaintLogDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintLogDetail_Level_LogLevelId",
                table: "ComplaintLogDetail");

            migrationBuilder.RenameColumn(
                name: "LogStatusId",
                table: "ComplaintLogDetail",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "LogMeansId",
                table: "ComplaintLogDetail",
                newName: "MeansId");

            migrationBuilder.RenameColumn(
                name: "LogLevelId",
                table: "ComplaintLogDetail",
                newName: "LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintLogDetail_LogStatusId",
                table: "ComplaintLogDetail",
                newName: "IX_ComplaintLogDetail_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintLogDetail_LogMeansId",
                table: "ComplaintLogDetail",
                newName: "IX_ComplaintLogDetail_MeansId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintLogDetail_LogLevelId",
                table: "ComplaintLogDetail",
                newName: "IX_ComplaintLogDetail_LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintLogStatus_StatusId",
                table: "ComplaintLogDetail",
                column: "StatusId",
                principalTable: "ComplaintLogStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_ComplaintReceiveMeans_MeansId",
                table: "ComplaintLogDetail",
                column: "MeansId",
                principalTable: "ComplaintReceiveMeans",
                principalColumn: "MeansId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintLogDetail_Level_LevelId",
                table: "ComplaintLogDetail",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
