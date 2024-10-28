using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainApp.Migrations
{
    /// <inheritdoc />
    public partial class commit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Bookings",
                newName: "StartDateTime");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Bookings",
                newName: "EndDateTime");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_ProviderId_ServiceInfoId_StartTime_EndTime",
                table: "Bookings",
                newName: "IX_Bookings_ProviderId_ServiceInfoId_StartDateTime_EndDateTime");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ServiceInfos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Providers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "ServiceInfos");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Providers");

            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "Bookings",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndDateTime",
                table: "Bookings",
                newName: "EndTime");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_ProviderId_ServiceInfoId_StartDateTime_EndDateTime",
                table: "Bookings",
                newName: "IX_Bookings_ProviderId_ServiceInfoId_StartTime_EndTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "Bookings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
