using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNET.Migrations
{
    public partial class alavi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_tblOrders_intProductId1",
                table: "tblOrders",
                column: "intProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrders_tblProducts_intProductId1",
                table: "tblOrders",
                column: "intProductId1",
                principalTable: "tblProducts",
                principalColumn: "intProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblOrders_tblProducts_intProductId1",
                table: "tblOrders");

            migrationBuilder.DropIndex(
                name: "IX_tblOrders_intProductId1",
                table: "tblOrders");

            migrationBuilder.RenameColumn(
                name: "intProductId1",
                table: "tblOrders",
                newName: "intProductId");

            migrationBuilder.AddColumn<int>(
                name: "TblProductsintProductId",
                table: "tblOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblOrders_TblProductsintProductId",
                table: "tblOrders",
                column: "TblProductsintProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrders_tblProducts_TblProductsintProductId",
                table: "tblOrders",
                column: "TblProductsintProductId",
                principalTable: "tblProducts",
                principalColumn: "intProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
