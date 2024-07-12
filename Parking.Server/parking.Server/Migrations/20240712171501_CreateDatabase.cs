using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking.Server.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Parkings",
                columns: table => new
                {
                    ParkingsId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Plate = table.Column<string>(type: "longtext", nullable: true),
                    ArrivalTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DepartureTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkings", x => x.ParkingsId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ParkingValues",
                columns: table => new
                {
                    ParkingValuesId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HourlyTolerance = table.Column<int>(type: "int", nullable: true),
                    HalfInTheFirstHour = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingValues", x => x.ParkingValuesId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parkings");

            migrationBuilder.DropTable(
                name: "ParkingValues");
        }
    }
}
