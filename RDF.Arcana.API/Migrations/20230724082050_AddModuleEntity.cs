using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddModuleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reason",
                table: "main_menus");

            migrationBuilder.CreateTable(
                name: "modules",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    main_menu_id = table.Column<int>(type: "int", nullable: false),
                    sub_menu_name = table.Column<string>(type: "longtext", nullable: true),
                    module_path = table.Column<string>(type: "longtext", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    added_by = table.Column<string>(type: "longtext", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    modified_by = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_modules", x => x.id);
                    table.ForeignKey(
                        name: "fk_modules_main_menus_main_menu_id",
                        column: x => x.main_menu_id,
                        principalTable: "main_menus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_modules_main_menu_id",
                table: "modules",
                column: "main_menu_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "modules");

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "main_menus",
                type: "longtext",
                nullable: true);
        }
    }
}
