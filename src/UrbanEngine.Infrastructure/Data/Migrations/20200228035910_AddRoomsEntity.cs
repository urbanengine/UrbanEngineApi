using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UrbanEngine.Infrastructure.Data.Migrations
{
    public partial class AddRoomsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RoomId",
                schema: "ue",
                table: "Event",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Room",
                schema: "ue",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Capacity = table.Column<int>(nullable: true),
                    Resources = table.Column<string>(maxLength: 500, nullable: true),
                    VenueId = table.Column<long>(nullable: true),
                    HasTVOrProjector = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Room_Venue_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "ue",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "CheckIn",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CheckedInAt", "DateCreated" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Unspecified).AddTicks(5784), new TimeSpan(0, -5, 0, 0, 0)), new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Local).AddTicks(5097) });

            migrationBuilder.InsertData(
                schema: "ue",
                table: "Room",
                columns: new[] { "Id", "Capacity", "DateCreated", "Description", "HasTVOrProjector", "Name", "Resources", "VenueId" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2020, 2, 27, 22, 59, 9, 983, DateTimeKind.Local).AddTicks(1999), "Cafe Conference Room", true, "Cafe Conference Room", null, 1L },
                    { 2L, null, new DateTime(2020, 2, 27, 22, 59, 9, 983, DateTimeKind.Local).AddTicks(6174), "Front Conference Room", true, "Front Conference Room", null, 1L },
                    { 3L, null, new DateTime(2020, 2, 27, 22, 59, 9, 983, DateTimeKind.Local).AddTicks(6246), "Corner Conference Room", true, "Corner Conference Room", null, 1L },
                    { 4L, null, new DateTime(2020, 2, 27, 22, 59, 9, 983, DateTimeKind.Local).AddTicks(6251), "Library", false, "Library", null, 1L },
                    { 5L, null, new DateTime(2020, 2, 27, 22, 59, 9, 983, DateTimeKind.Local).AddTicks(6253), "Training Room", true, "Training Room", null, 1L }
                });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Venue",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2020, 2, 27, 22, 59, 9, 974, DateTimeKind.Local).AddTicks(9153));

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Event",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "EndDate", "RoomId", "StartDate" },
                values: new object[] { new DateTime(2020, 2, 27, 22, 59, 9, 981, DateTimeKind.Local).AddTicks(6540), new DateTimeOffset(new DateTime(2020, 2, 27, 22, 59, 9, 981, DateTimeKind.Unspecified).AddTicks(2571), new TimeSpan(0, -5, 0, 0, 0)), 4L, new DateTimeOffset(new DateTime(2020, 2, 27, 22, 59, 9, 981, DateTimeKind.Unspecified).AddTicks(2480), new TimeSpan(0, -5, 0, 0, 0)) });

            migrationBuilder.InsertData(
                schema: "ue",
                table: "Event",
                columns: new[] { "Id", "DateCreated", "Description", "EndDate", "EventType", "Name", "OrganizerId", "RoomId", "StartDate", "VenueId" },
                values: new object[,]
                {
                    { 2L, new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Local).AddTicks(2159), null, new DateTimeOffset(new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Unspecified).AddTicks(2130), new TimeSpan(0, -5, 0, 0, 0)), 1, "Designer's Corner", null, 2L, new DateTimeOffset(new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Unspecified).AddTicks(2116), new TimeSpan(0, -5, 0, 0, 0)), 1L },
                    { 3L, new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Local).AddTicks(2250), null, new DateTimeOffset(new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Unspecified).AddTicks(2246), new TimeSpan(0, -5, 0, 0, 0)), 1, "Huntsville AI", null, 5L, new DateTimeOffset(new DateTime(2020, 2, 27, 22, 59, 9, 982, DateTimeKind.Unspecified).AddTicks(2243), new TimeSpan(0, -5, 0, 0, 0)), 1L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_RoomId",
                schema: "ue",
                table: "Event",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_VenueId",
                schema: "ue",
                table: "Room",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Room_RoomId",
                schema: "ue",
                table: "Event",
                column: "RoomId",
                principalSchema: "ue",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Room_RoomId",
                schema: "ue",
                table: "Event");

            migrationBuilder.DropTable(
                name: "Room",
                schema: "ue");

            migrationBuilder.DropIndex(
                name: "IX_Event_RoomId",
                schema: "ue",
                table: "Event");

            migrationBuilder.DeleteData(
                schema: "ue",
                table: "Event",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "ue",
                table: "Event",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DropColumn(
                name: "RoomId",
                schema: "ue",
                table: "Event");

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "CheckIn",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CheckedInAt", "DateCreated" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 1, 17, 12, 27, 32, 91, DateTimeKind.Unspecified).AddTicks(2432), new TimeSpan(0, -6, 0, 0, 0)), new DateTime(2020, 1, 17, 12, 27, 32, 91, DateTimeKind.Local).AddTicks(1665) });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Event",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2020, 1, 17, 12, 27, 32, 90, DateTimeKind.Local).AddTicks(1720), new DateTimeOffset(new DateTime(2020, 1, 17, 12, 27, 32, 89, DateTimeKind.Unspecified).AddTicks(7316), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 1, 17, 12, 27, 32, 89, DateTimeKind.Unspecified).AddTicks(7146), new TimeSpan(0, -6, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Venue",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2020, 1, 17, 12, 27, 32, 73, DateTimeKind.Local).AddTicks(1324));
        }
    }
}
