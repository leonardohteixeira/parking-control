using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    Duration = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    BilledTime = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AmountDue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkings", x => x.ParkingsId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parkings");
        }
    }
}
