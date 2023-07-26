using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_categories_product_sub_categories_product_sub_catego",
                table: "product_categories");

            migrationBuilder.DropForeignKey(
                name: "fk_users_roles_role_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_product_categories_product_sub_category_id",
                table: "product_categories");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "user_roles");

            migrationBuilder.DropColumn(
                name: "product_sub_category_id",
                table: "product_categories");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "user_role_id",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "product_category_id",
                table: "product_sub_categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_users_user_role_id",
                table: "users",
                column: "user_role_id",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "fk_users_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_user_roles_user_role_id",
                table: "users",
                column: "user_role_id",
                principalTable: "user_roles",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_sub_categories_product_categories_product_category_id",
                table: "product_sub_categories");

            migrationBuilder.DropForeignKey(
                name: "fk_users_roles_role_id",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "fk_users_user_roles_user_role_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_user_role_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_product_sub_categories_product_category_id",
                table: "product_sub_categories");

            migrationBuilder.DropColumn(
                name: "user_role_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "product_category_id",
                table: "product_sub_categories");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "user_roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "product_sub_category_id",
                table: "product_categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_product_categories_product_sub_category_id",
                table: "product_categories",
                column: "product_sub_category_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_product_categories_product_sub_categories_product_sub_catego",
                table: "product_categories",
                column: "product_sub_category_id",
                principalTable: "product_sub_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
