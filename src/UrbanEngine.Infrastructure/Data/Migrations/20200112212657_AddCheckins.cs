using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UrbanEngine.Infrastructure.Data.Migrations
{
    public partial class AddCheckins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckIn",
                schema: "ue",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CheckedInAt = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    EventId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckIn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckIn_Event_EventId",
                        column: x => x.EventId,
                        principalSchema: "ue",
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Venue",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2020, 1, 12, 15, 26, 57, 611, DateTimeKind.Local).AddTicks(1022));

            migrationBuilder.CreateIndex(
                name: "IX_CheckIn_EventId",
                schema: "ue",
                table: "CheckIn",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckIn",
                schema: "ue");

            migrationBuilder.UpdateData(
                schema: "ue",
                table: "Venue",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreated",
                value: new DateTime(2019, 12, 23, 14, 18, 28, 432, DateTimeKind.Local).AddTicks(5423));
        }
    }
}
