using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _10_PrincipalKey.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                    table.UniqueConstraint("AK_Cars_LicensePlate", x => x.LicensePlate);
                });

            migrationBuilder.CreateTable(
                name: "RecordOfSale",
                columns: table => new
                {
                    RecordOfSaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateSold = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarLicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordOfSale", x => x.RecordOfSaleId);
                    table.ForeignKey(
                        name: "FK_RecordOfSale_Cars_CarLicensePlate",
                        column: x => x.CarLicensePlate,
                        principalTable: "Cars",
                        principalColumn: "LicensePlate",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordOfSale_CarLicensePlate",
                table: "RecordOfSale",
                column: "CarLicensePlate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordOfSale");

            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
