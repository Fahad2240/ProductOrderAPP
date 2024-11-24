using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNET.Migrations
{
    public partial class sett : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblOrders_tblProducts_intProductId1",
                table: "tblOrders");

            migrationBuilder.RenameColumn(
                name: "intProductId1",
                table: "tblOrders",
                newName: "intProductId");

            migrationBuilder.RenameIndex(
                name: "IX_tblOrders_intProductId1",
                table: "tblOrders",
                newName: "IX_tblOrders_intProductId");

            migrationBuilder.AddColumn<int>(
                name: "TblProductsintProductId",
                table: "tblOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblOrders_TblProductsintProductId",
                table: "tblOrders",
                column: "TblProductsintProductId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblOrders_tblProducts_intProductId",
                table: "tblOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_tblOrders_tblProducts_TblProductsintProductId",
                table: "tblOrders");

            migrationBuilder.DropIndex(
                name: "IX_tblOrders_TblProductsintProductId",
                table: "tblOrders");

            migrationBuilder.DropColumn(
                name: "TblProductsintProductId",
                table: "tblOrders");

            migrationBuilder.RenameColumn(
                name: "intProductId",
                table: "tblOrders",
                newName: "intProductId1");

            migrationBuilder.RenameIndex(
                name: "IX_tblOrders_intProductId",
                table: "tblOrders",
                newName: "IX_tblOrders_intProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrders_tblProducts_intProductId1",
                table: "tblOrders",
                column: "intProductId1",
                principalTable: "tblProducts",
                principalColumn: "intProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
