using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    meat_type_id = table.Column<int>(type: "int", nullable: false),
                    product_category_id = table.Column<int>(type: "int", nullable: false),
                    uom_id = table.Column<int>(type: "int", nullable: false),
                    added_by = table.Column<string>(type: "longtext", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    item_code = table.Column<string>(type: "longtext", nullable: true),
                    item_description = table.Column<string>(type: "longtext", nullable: true),
                    modified_by = table.Column<string>(type: "longtext", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_items_meat_types_meat_type_id",
                        column: x => x.meat_type_id,
                        principalTable: "meat_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_items_product_categories_product_category_id",
                        column: x => x.product_category_id,
                        principalTable: "product_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_items_uoms_uom_id",
                        column: x => x.uom_id,
                        principalTable: "uoms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_items_meat_type_id",
                table: "items",
                column: "meat_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_items_product_category_id",
                table: "items",
                column: "product_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_items_uom_id",
                table: "items",
                column: "uom_id");
        }
    }
}
