using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class Adjustmentsss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_sub_categories_product_categories_product_category_id",
                table: "product_sub_categories");

            migrationBuilder.DropIndex(
                name: "ix_product_sub_categories_product_category_id",
                table: "product_sub_categories");

            migrationBuilder.CreateTable(
                name: "product_category_product_sub_category",
                columns: table => new
                {
                    product_category_id = table.Column<int>(type: "int", nullable: false),
                    product_sub_category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_category_product_sub_category", x => new { x.product_category_id, x.product_sub_category_id });
                    table.ForeignKey(
                        name: "fk_product_category_product_sub_category_product_categories_pro",
                        column: x => x.product_category_id,
                        principalTable: "product_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_category_product_sub_category_product_sub_categories",
                        column: x => x.product_sub_category_id,
                        principalTable: "product_sub_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_product_category_product_sub_category_product_sub_category_id",
                table: "product_category_product_sub_category",
                column: "product_sub_category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_category_product_sub_category");

            migrationBuilder.CreateIndex(
                name: "ix_product_sub_categories_product_category_id",
                table: "product_sub_categories",
                column: "product_category_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_sub_categories_product_categories_product_category_id",
                table: "product_sub_categories",
                column: "product_category_id",
                principalTable: "product_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
