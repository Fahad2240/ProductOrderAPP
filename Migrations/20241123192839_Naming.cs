using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNET.Migrations
{
    public partial class Naming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblOrders_tblProducts_intProductId",
                table: "tblOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_tblOrders_tblProducts_TblProductsintProductId",
                table: "tblOrders");

            migrationBuilder.RenameColumn(
                name: "intProductId",
                table: "tblProducts",
                newName: "IntProductId");

            migrationBuilder.RenameColumn(
                name: "intProductId",
                table: "tblOrders",
                newName: "IntProductId");

            migrationBuilder.RenameColumn(
                name: "TblProductsintProductId",
                table: "tblOrders",
                newName: "TblProductsIntProductId");

            migrationBuilder.RenameIndex(
                name: "IX_tblOrders_TblProductsintProductId",
                table: "tblOrders",
                newName: "IX_tblOrders_TblProductsIntProductId");

            migrationBuilder.RenameIndex(
                name: "IX_tblOrders_intProductId",
                table: "tblOrders",
                newName: "IX_tblOrders_IntProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrders_tblProducts_IntProductId",
                table: "tblOrders",
                column: "IntProductId",
                principalTable: "tblProducts",
                principalColumn: "IntProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrders_tblProducts_TblProductsIntProductId",
                table: "tblOrders",
                column: "TblProductsIntProductId",
                principalTable: "tblProducts",
                principalColumn: "IntProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblOrders_tblProducts_IntProductId",
                table: "tblOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_tblOrders_tblProducts_TblProductsIntProductId",
                table: "tblOrders");

            migrationBuilder.RenameColumn(
                name: "IntProductId",
                table: "tblProducts",
                newName: "intProductId");

            migrationBuilder.RenameColumn(
                name: "TblProductsIntProductId",
                table: "tblOrders",
                newName: "TblProductsintProductId");

            migrationBuilder.RenameColumn(
                name: "IntProductId",
                table: "tblOrders",
                newName: "intProductId");

            migrationBuilder.RenameIndex(
                name: "IX_tblOrders_TblProductsIntProductId",
                table: "tblOrders",
                newName: "IX_tblOrders_TblProductsintProductId");

            migrationBuilder.RenameIndex(
                name: "IX_tblOrders_IntProductId",
                table: "tblOrders",
                newName: "IX_tblOrders_intProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrders_tblProducts_intProductId",
                table: "tblOrders",
                column: "intProductId",
                principalTable: "tblProducts",
                principalColumn: "intProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrders_tblProducts_TblProductsintProductId",
                table: "tblOrders",
                column: "TblProductsintProductId",
                principalTable: "tblProducts",
                principalColumn: "intProductId");
        }
    }
}
