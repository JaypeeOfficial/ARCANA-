using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustUserRoleRemoveTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_user_roles_user_role_id",
                table: "users");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropIndex(
                name: "ix_users_user_role_id",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "added_by",
                table: "locations",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "modified_by",
                table: "locations",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "added_by",
                table: "departments",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "modified_by",
                table: "departments",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "added_by",
                table: "companies",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "modified_by",
                table: "companies",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "added_by",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "added_by",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "added_by",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "companies");

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    modiefied_by = table.Column<string>(type: "longtext", nullable: true),
                    permissions = table.Column<string>(type: "longtext", nullable: true),
                    role_name = table.Column<string>(type: "longtext", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_users_user_role_id",
                table: "users",
                column: "user_role_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_users_user_roles_user_role_id",
                table: "users",
                column: "user_role_id",
                principalTable: "user_roles",
                principalColumn: "id");
        }
    }
}
