using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class SalesOrder_Addresses_CustomerAddress_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesOrderAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerAddressId = table.Column<int>(type: "int", nullable: true),
                    SalesOrderId = table.Column<int>(type: "int", nullable: true),
                    IsShippingAddress = table.Column<bool>(type: "bit", nullable: false),
                    IsBillingAddress = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderAddresses_Addresses_CustomerAddressId",
                        column: x => x.CustomerAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesOrderAddresses_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderAddresses_CustomerAddressId",
                table: "SalesOrderAddresses",
                column: "CustomerAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderAddresses_SalesOrderId",
                table: "SalesOrderAddresses",
                column: "SalesOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesOrderAddresses");
        }
    }
}
