using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _07_ForeignKey_02.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    State = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => new { x.State, x.LicensePlate });
                });

            migrationBuilder.CreateTable(
                name: "RecordOfSale",
                columns: table => new
                {
                    RecordOfSaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateSold = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarState = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CarLicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordOfSale", x => x.RecordOfSaleId);
                    table.ForeignKey(
                        name: "FK_RecordOfSale_Cars_CarState_CarLicensePlate",
                        columns: x => new { x.CarState, x.CarLicensePlate },
                        principalTable: "Cars",
                        principalColumns: new[] { "State", "LicensePlate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordOfSale_CarState_CarLicensePlate",
                table: "RecordOfSale",
                columns: new[] { "CarState", "CarLicensePlate" });
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
