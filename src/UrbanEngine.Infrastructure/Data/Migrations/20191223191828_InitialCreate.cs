using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UrbanEngine.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ue");

            migrationBuilder.CreateTable(
                name: "Venue",
                schema: "ue",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Address2 = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 75, nullable: true),
                    State = table.Column<string>(maxLength: 75, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 30, nullable: true),
                    Country = table.Column<string>(maxLength: 75, nullable: true),
                    Region = table.Column<int>(nullable: true),
                    IsAvailable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                schema: "ue",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    EventType = table.Column<int>(nullable: false),
                    OrganizerId = table.Column<string>(nullable: true),
                    VenueId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Venue_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "ue",
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "ue",
                table: "Venue",
                columns: new[] { "Id", "Address", "Address2", "City", "Country", "DateCreated", "IsAvailable", "Name", "PostalCode", "Region", "State" },
                values: new object[] { 1L, "3001 9th Avenue Southwest", null, "Huntsville", "United States", new DateTime(2019, 12, 23, 14, 18, 28, 432, DateTimeKind.Local).AddTicks(5423), false, "Huntsville West", "35805", 1, "AL" });

            migrationBuilder.CreateIndex(
                name: "IX_Event_VenueId",
                schema: "ue",
                table: "Event",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Venue_Name",
                schema: "ue",
                table: "Venue",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event",
                schema: "ue");

            migrationBuilder.DropTable(
                name: "Venue",
                schema: "ue");
        }
    }
}
