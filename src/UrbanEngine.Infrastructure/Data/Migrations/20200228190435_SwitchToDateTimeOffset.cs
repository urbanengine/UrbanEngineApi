using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UrbanEngine.Infrastructure.Data.Migrations
{
    public partial class SwitchToDateTimeOffset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateCreated",
                schema: "ue",
                table: "Venue",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateCreated",
                schema: "ue",
                table: "Room",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateCreated",
                schema: "ue",
                table: "Event",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateCreated",
                schema: "ue",
                table: "CheckIn",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "CheckIn",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CheckedInAt", "DateCreated" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 410, DateTimeKind.Unspecified).AddTicks(6734), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 410, DateTimeKind.Unspecified).AddTicks(6036), new TimeSpan(0, -5, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Event",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "EndDate", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 409, DateTimeKind.Unspecified).AddTicks(7633), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 409, DateTimeKind.Unspecified).AddTicks(5302), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 409, DateTimeKind.Unspecified).AddTicks(5216), new TimeSpan(0, -5, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Event",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateCreated", "EndDate", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 410, DateTimeKind.Unspecified).AddTicks(3083), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 410, DateTimeKind.Unspecified).AddTicks(3045), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 410, DateTimeKind.Unspecified).AddTicks(3004), new TimeSpan(0, -5, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Event",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "DateCreated", "EndDate", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 410, DateTimeKind.Unspecified).AddTicks(3165), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 410, DateTimeKind.Unspecified).AddTicks(3160), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 410, DateTimeKind.Unspecified).AddTicks(3155), new TimeSpan(0, -5, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 411, DateTimeKind.Unspecified).AddTicks(4005), new TimeSpan(0, -5, 0, 0, 0)));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 411, DateTimeKind.Unspecified).AddTicks(9216), new TimeSpan(0, -5, 0, 0, 0)));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 411, DateTimeKind.Unspecified).AddTicks(9312), new TimeSpan(0, -5, 0, 0, 0)));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 411, DateTimeKind.Unspecified).AddTicks(9319), new TimeSpan(0, -5, 0, 0, 0)));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 411, DateTimeKind.Unspecified).AddTicks(9325), new TimeSpan(0, -5, 0, 0, 0)));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Venue",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 2, 28, 14, 4, 34, 399, DateTimeKind.Unspecified).AddTicks(9789), new TimeSpan(0, -5, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "ue",
                table: "Venue",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "ue",
                table: "Room",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "ue",
                table: "Event",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "ue",
                table: "CheckIn",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "CheckIn",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CheckedInAt", "DateCreated" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 2, 28, 13, 26, 57, 432, DateTimeKind.Unspecified).AddTicks(991), new TimeSpan(0, -5, 0, 0, 0)), new DateTime(2020, 2, 28, 13, 26, 57, 432, DateTimeKind.Local).AddTicks(315) });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Event",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2020, 2, 28, 13, 26, 57, 431, DateTimeKind.Local).AddTicks(1674), new DateTimeOffset(new DateTime(2020, 2, 28, 13, 26, 57, 430, DateTimeKind.Unspecified).AddTicks(7528), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 2, 28, 13, 26, 57, 430, DateTimeKind.Unspecified).AddTicks(7404), new TimeSpan(0, -5, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Event",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateCreated", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2020, 2, 28, 13, 26, 57, 431, DateTimeKind.Local).AddTicks(6694), new DateTimeOffset(new DateTime(2020, 2, 28, 13, 26, 57, 431, DateTimeKind.Unspecified).AddTicks(6658), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 2, 28, 13, 26, 57, 431, DateTimeKind.Unspecified).AddTicks(6642), new TimeSpan(0, -5, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Event",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "DateCreated", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2020, 2, 28, 13, 26, 57, 431, DateTimeKind.Local).AddTicks(6788), new DateTimeOffset(new DateTime(2020, 2, 28, 13, 26, 57, 431, DateTimeKind.Unspecified).AddTicks(6785), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 2, 28, 13, 26, 57, 431, DateTimeKind.Unspecified).AddTicks(6782), new TimeSpan(0, -5, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2020, 2, 28, 13, 26, 57, 432, DateTimeKind.Local).AddTicks(7515));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2020, 2, 28, 13, 26, 57, 433, DateTimeKind.Local).AddTicks(2161));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2020, 2, 28, 13, 26, 57, 433, DateTimeKind.Local).AddTicks(2229));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2020, 2, 28, 13, 26, 57, 433, DateTimeKind.Local).AddTicks(2233));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2020, 2, 28, 13, 26, 57, 433, DateTimeKind.Local).AddTicks(2236));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Venue",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2020, 2, 28, 13, 26, 57, 422, DateTimeKind.Local).AddTicks(4092));
        }
    }
}
