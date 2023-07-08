using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStore.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleProductsSumValueandQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleProducts_Products_ProductId",
                table: "SaleProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleProducts_Sales_SaleId",
                table: "SaleProducts");

            migrationBuilder.DropIndex(
                name: "IX_SaleProducts_ProductId",
                table: "SaleProducts");

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "SaleProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SumValue",
                table: "SaleProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "ParentCategoryId",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ProductSaleProduct",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    SaleProductsSaleId = table.Column<int>(type: "int", nullable: false),
                    SaleProductsProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSaleProduct", x => new { x.ProductsId, x.SaleProductsSaleId, x.SaleProductsProductId });
                    table.ForeignKey(
                        name: "FK_ProductSaleProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSaleProduct_SaleProducts_SaleProductsSaleId_SaleProductsProductId",
                        columns: x => new { x.SaleProductsSaleId, x.SaleProductsProductId },
                        principalTable: "SaleProducts",
                        principalColumns: new[] { "SaleId", "ProductId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleSaleProduct",
                columns: table => new
                {
                    SalesId = table.Column<int>(type: "int", nullable: false),
                    SaleProductsSaleId = table.Column<int>(type: "int", nullable: false),
                    SaleProductsProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleSaleProduct", x => new { x.SalesId, x.SaleProductsSaleId, x.SaleProductsProductId });
                    table.ForeignKey(
                        name: "FK_SaleSaleProduct_SaleProducts_SaleProductsSaleId_SaleProductsProductId",
                        columns: x => new { x.SaleProductsSaleId, x.SaleProductsProductId },
                        principalTable: "SaleProducts",
                        principalColumns: new[] { "SaleId", "ProductId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleSaleProduct_Sales_SalesId",
                        column: x => x.SalesId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSaleProduct_SaleProductsSaleId_SaleProductsProductId",
                table: "ProductSaleProduct",
                columns: new[] { "SaleProductsSaleId", "SaleProductsProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_SaleSaleProduct_SaleProductsSaleId_SaleProductsProductId",
                table: "SaleSaleProduct",
                columns: new[] { "SaleProductsSaleId", "SaleProductsProductId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSaleProduct");

            migrationBuilder.DropTable(
                name: "SaleSaleProduct");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SaleProducts");

            migrationBuilder.DropColumn(
                name: "SumValue",
                table: "SaleProducts");

            migrationBuilder.AlterColumn<int>(
                name: "ParentCategoryId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleProducts_ProductId",
                table: "SaleProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProducts_Products_ProductId",
                table: "SaleProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProducts_Sales_SaleId",
                table: "SaleProducts",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
