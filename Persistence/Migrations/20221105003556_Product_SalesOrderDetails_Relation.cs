using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Product_SalesOrderDetails_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDetails_Products_ProductId",
                table: "SalesOrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "SalesOrderDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDetails_Products_ProductId",
                table: "SalesOrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDetails_Products_ProductId",
                table: "SalesOrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "SalesOrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDetails_Products_ProductId",
                table: "SalesOrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
