using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceTask.Migrations
{
    /// <inheritdoc />
    public partial class FixedProductDbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Products_Product_ProductID",
                table: "Order_Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryCID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Product_ProductID",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryCID",
                table: "Products",
                newName: "IX_Products_CategoryCID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "PID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Products_Products_ProductID",
                table: "Order_Products",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "PID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryCID",
                table: "Products",
                column: "CategoryCID",
                principalTable: "Categories",
                principalColumn: "CID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductID",
                table: "Reviews",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "PID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Products_Products_ProductID",
                table: "Order_Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryCID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductID",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryCID",
                table: "Product",
                newName: "IX_Product_CategoryCID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "PID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Products_Product_ProductID",
                table: "Order_Products",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "PID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryCID",
                table: "Product",
                column: "CategoryCID",
                principalTable: "Categories",
                principalColumn: "CID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Product_ProductID",
                table: "Reviews",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "PID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
