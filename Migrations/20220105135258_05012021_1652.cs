using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupCCP.Migrations
{
    public partial class _05012021_1652 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WIPToResolvedEscallate",
                table: "Timelines",
                newName: "OverdueResolvedEscallate");

            migrationBuilder.RenameColumn(
                name: "WIPToResolved",
                table: "Timelines",
                newName: "OverdueResolvedReminderHrs");

            migrationBuilder.RenameColumn(
                name: "ResolvedToCloseEscallate",
                table: "Timelines",
                newName: "OverdueResolvedHrs");

            migrationBuilder.RenameColumn(
                name: "ResolvedToClose",
                table: "Timelines",
                newName: "OverdueResolvedClosedReminderHrs");

            migrationBuilder.RenameColumn(
                name: "AssignedToWIPEscallate",
                table: "Timelines",
                newName: "OverdueAssignedEscallate");

            migrationBuilder.RenameColumn(
                name: "AssignedToWIP",
                table: "Timelines",
                newName: "OverdueResolvedClosedHrs");

            migrationBuilder.AddColumn<float>(
                name: "OverdueAssignedHrs",
                table: "Timelines",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "OverdueAssignedReminderHrs",
                table: "Timelines",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "OverdueClosedEscallate",
                table: "Timelines",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OverdueAssignedHrs",
                table: "Timelines");

            migrationBuilder.DropColumn(
                name: "OverdueAssignedReminderHrs",
                table: "Timelines");

            migrationBuilder.DropColumn(
                name: "OverdueClosedEscallate",
                table: "Timelines");

            migrationBuilder.RenameColumn(
                name: "OverdueResolvedReminderHrs",
                table: "Timelines",
                newName: "WIPToResolved");

            migrationBuilder.RenameColumn(
                name: "OverdueResolvedHrs",
                table: "Timelines",
                newName: "ResolvedToCloseEscallate");

            migrationBuilder.RenameColumn(
                name: "OverdueResolvedEscallate",
                table: "Timelines",
                newName: "WIPToResolvedEscallate");

            migrationBuilder.RenameColumn(
                name: "OverdueResolvedClosedReminderHrs",
                table: "Timelines",
                newName: "ResolvedToClose");

            migrationBuilder.RenameColumn(
                name: "OverdueResolvedClosedHrs",
                table: "Timelines",
                newName: "AssignedToWIP");

            migrationBuilder.RenameColumn(
                name: "OverdueAssignedEscallate",
                table: "Timelines",
                newName: "AssignedToWIPEscallate");
        }
    }
}
