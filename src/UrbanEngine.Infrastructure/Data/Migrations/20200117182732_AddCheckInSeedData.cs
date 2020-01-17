using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UrbanEngine.Infrastructure.Data.Migrations
{
    public partial class AddCheckInSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartDate",
                schema: "ue",
                table: "Event",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EndDate",
                schema: "ue",
                table: "Event",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.InsertData(
                schema: "ue",
                table: "Event",
                columns: new[] { "Id", "DateCreated", "Description", "EndDate", "EventType", "Name", "OrganizerId", "StartDate", "VenueId" },
                values: new object[] { 1L, new DateTime(2020, 1, 17, 12, 27, 32, 90, DateTimeKind.Local).AddTicks(1720), null, new DateTimeOffset(new DateTime(2020, 1, 17, 12, 27, 32, 89, DateTimeKind.Unspecified).AddTicks(7316), new TimeSpan(0, -6, 0, 0, 0)), 1, "show256", null, new DateTimeOffset(new DateTime(2020, 1, 17, 12, 27, 32, 89, DateTimeKind.Unspecified).AddTicks(7146), new TimeSpan(0, -6, 0, 0, 0)), 1L });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Venue",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2020, 1, 17, 12, 27, 32, 73, DateTimeKind.Local).AddTicks(1324));

            migrationBuilder.InsertData(
                schema: "ue",
                table: "CheckIn",
                columns: new[] { "Id", "CheckedInAt", "DateCreated", "EventId", "UserId" },
                values: new object[] { 1L, new DateTimeOffset(new DateTime(2020, 1, 17, 12, 27, 32, 91, DateTimeKind.Unspecified).AddTicks(2432), new TimeSpan(0, -6, 0, 0, 0)), new DateTime(2020, 1, 17, 12, 27, 32, 91, DateTimeKind.Local).AddTicks(1665), 1L, 0L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ue",
                table: "CheckIn",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "ue",
                table: "Event",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "ue",
                table: "Event",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "ue",
                table: "Event",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Venue",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2020, 1, 12, 15, 26, 57, 611, DateTimeKind.Local).AddTicks(1022));
        }
    }
}
