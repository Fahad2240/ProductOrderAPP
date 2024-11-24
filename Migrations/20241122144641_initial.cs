using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNET.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblOrders",
                columns: table => new
                {
                    intOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intProductId = table.Column<int>(type: "int", nullable: false),
                    strCustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    dtOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrders", x => x.intOrderId);
                });

            migrationBuilder.CreateTable(
                name: "tblProducts",
                columns: table => new
                {
                    intProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    numStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProducts", x => x.intProductId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblOrders");

            migrationBuilder.DropTable(
                name: "tblProducts");
        }
    }
}
