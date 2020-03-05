using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UrbanEngine.Infrastructure.Data.Migrations
{
    public partial class AddRoomsEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Venue_VenueId",
                schema: "ue",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_VenueId",
                schema: "ue",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "VenueId",
                schema: "ue",
                table: "Event");

            migrationBuilder.AddColumn<long>(
                name: "EventVenueEntityId",
                schema: "ue",
                table: "Event",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventVenueEntityId",
                schema: "ue",
                table: "Event",
                column: "EventVenueEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Venue_EventVenueEntityId",
                schema: "ue",
                table: "Event",
                column: "EventVenueEntityId",
                principalSchema: "ue",
                principalTable: "Venue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Venue_EventVenueEntityId",
                schema: "ue",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_EventVenueEntityId",
                schema: "ue",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EventVenueEntityId",
                schema: "ue",
                table: "Event");

            migrationBuilder.AddColumn<long>(
                name: "VenueId",
                schema: "ue",
                table: "Event",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "CheckIn",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CheckedInAt", "DateCreated" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Unspecified).AddTicks(5784), new TimeSpan(0, -5, 0, 0, 0)), new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Local).AddTicks(5097) });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Event",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "EndDate", "StartDate", "VenueId" },
                values: new object[] { new DateTime(2020, 2, 27, 22, 59, 9, 981, DateTimeKind.Local).AddTicks(6540), new DateTimeOffset(new DateTime(2020, 2, 27, 22, 59, 9, 981, DateTimeKind.Unspecified).AddTicks(2571), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 2, 27, 22, 59, 9, 981, DateTimeKind.Unspecified).AddTicks(2480), new TimeSpan(0, -5, 0, 0, 0)), 1L });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Event",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateCreated", "EndDate", "StartDate", "VenueId" },
                values: new object[] { new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Local).AddTicks(2159), new DateTimeOffset(new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Unspecified).AddTicks(2130), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Unspecified).AddTicks(2116), new TimeSpan(0, -5, 0, 0, 0)), 1L });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Event",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "DateCreated", "EndDate", "StartDate", "VenueId" },
                values: new object[] { new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Local).AddTicks(2250), new DateTimeOffset(new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Unspecified).AddTicks(2246), new TimeSpan(0, -5, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Unspecified).AddTicks(2243), new TimeSpan(0, -5, 0, 0, 0)), 1L });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2020, 2, 27, 22, 59, 9, 983, DateTimeKind.Local).AddTicks(1999));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreated",
                value: new DateTime(2020, 2, 27, 22, 59, 9, 983, DateTimeKind.Local).AddTicks(6174));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreated",
                value: new DateTime(2020, 2, 27, 22, 59, 9, 983, DateTimeKind.Local).AddTicks(6246));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreated",
                value: new DateTime(2020, 2, 27, 22, 59, 9, 983, DateTimeKind.Local).AddTicks(6251));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Room",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreated",
                value: new DateTime(2020, 2, 27, 22, 59, 9, 983, DateTimeKind.Local).AddTicks(6253));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Venue",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2020, 2, 27, 22, 59, 9, 974, DateTimeKind.Local).AddTicks(9153));

            migrationBuilder.CreateIndex(
                name: "IX_Event_VenueId",
                schema: "ue",
                table: "Event",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Venue_VenueId",
                schema: "ue",
                table: "Event",
                column: "VenueId",
                principalSchema: "ue",
                principalTable: "Venue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
