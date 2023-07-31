using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class ReAdjustDatabaseEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_companies_company_id",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "fk_users_departments_department_id",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "fk_users_locations_location_id",
                table: "users");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropColumn(
                name: "addded_by",
                table: "uoms");

            migrationBuilder.AlterColumn<int>(
                name: "location_id",
                table: "users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "department_id",
                table: "users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "company_id",
                table: "users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "added_by",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "added_by",
                table: "uoms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "added_by",
                table: "term_days",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "added_by",
                table: "product_sub_categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "added_by",
                table: "product_categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "modified_by",
                table: "product_categories",
                type: "longtext",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "added_by",
                table: "meat_types",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "added_by",
                table: "locations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "added_by",
                table: "items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "added_by",
                table: "discounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "added_by",
                table: "departments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "added_by",
                table: "companies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "prospecting_clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    owners_name = table.Column<string>(type: "longtext", nullable: true),
                    owners_address = table.Column<string>(type: "longtext", nullable: true),
                    business_name = table.Column<string>(type: "longtext", nullable: true),
                    business_address = table.Column<string>(type: "longtext", nullable: true),
                    phone_number = table.Column<string>(type: "longtext", nullable: true),
                    registration_status = table.Column<string>(type: "longtext", nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    email_address = table.Column<string>(type: "longtext", nullable: true),
                    authorized_representative = table.Column<string>(type: "longtext", nullable: true),
                    authorized_representative_position = table.Column<string>(type: "longtext", nullable: true),
                    title_authorized_signatory = table.Column<string>(type: "longtext", nullable: true),
                    authorization_letter = table.Column<string>(type: "longtext", nullable: true),
                    owner_valid_id = table.Column<string>(type: "longtext", nullable: true),
                    representative_valid_id = table.Column<string>(type: "longtext", nullable: true),
                    dti_permit_photo = table.Column<string>(type: "longtext", nullable: true),
                    barangay_or_other_permit_photo = table.Column<string>(type: "longtext", nullable: true),
                    store_photo = table.Column<string>(type: "longtext", nullable: true),
                    store_category = table.Column<string>(type: "longtext", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: true),
                    modified_by = table.Column<string>(type: "longtext", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prospecting_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_prospecting_clients_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prospecting_clients_users_approved_by",
                        column: x => x.approved_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_prospecting_clients_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_role_name = table.Column<string>(type: "longtext", nullable: true),
                    permissions = table.Column<string>(type: "longtext", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<string>(type: "longtext", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_roles_users_added_by",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_users_added_by",
                table: "users",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_users_user_role_id",
                table: "users",
                column: "user_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_uoms_added_by",
                table: "uoms",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_term_days_added_by",
                table: "term_days",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_product_sub_categories_added_by",
                table: "product_sub_categories",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_product_categories_added_by",
                table: "product_categories",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_meat_types_added_by",
                table: "meat_types",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_locations_added_by",
                table: "locations",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_items_added_by",
                table: "items",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_discounts_added_by",
                table: "discounts",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_departments_added_by",
                table: "departments",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_companies_added_by",
                table: "companies",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_prospecting_clients_added_by",
                table: "prospecting_clients",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_prospecting_clients_approved_by",
                table: "prospecting_clients",
                column: "approved_by");

            migrationBuilder.CreateIndex(
                name: "ix_prospecting_clients_user_id",
                table: "prospecting_clients",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_added_by",
                table: "user_roles",
                column: "added_by");

            migrationBuilder.AddForeignKey(
                name: "fk_companies_users_added_by",
                table: "companies",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_departments_users_added_by",
                table: "departments",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_discounts_users_added_by",
                table: "discounts",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_items_users_added_by",
                table: "items",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_locations_users_added_by",
                table: "locations",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_meat_types_users_added_by",
                table: "meat_types",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_product_categories_users_added_by",
                table: "product_categories",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_product_sub_categories_users_added_by",
                table: "product_sub_categories",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_term_days_users_added_by",
                table: "term_days",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_uoms_users_added_by",
                table: "uoms",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_companies_company_id",
                table: "users",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_departments_department_id",
                table: "users",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_locations_location_id",
                table: "users",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_user_roles_user_role_id",
                table: "users",
                column: "user_role_id",
                principalTable: "user_roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_users_added_by",
                table: "users",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_companies_users_added_by",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "fk_departments_users_added_by",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "fk_discounts_users_added_by",
                table: "discounts");

            migrationBuilder.DropForeignKey(
                name: "fk_items_users_added_by",
                table: "items");

            migrationBuilder.DropForeignKey(
                name: "fk_locations_users_added_by",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "fk_meat_types_users_added_by",
                table: "meat_types");

            migrationBuilder.DropForeignKey(
                name: "fk_product_categories_users_added_by",
                table: "product_categories");

            migrationBuilder.DropForeignKey(
                name: "fk_product_sub_categories_users_added_by",
                table: "product_sub_categories");

            migrationBuilder.DropForeignKey(
                name: "fk_term_days_users_added_by",
                table: "term_days");

            migrationBuilder.DropForeignKey(
                name: "fk_uoms_users_added_by",
                table: "uoms");

            migrationBuilder.DropForeignKey(
                name: "fk_users_companies_company_id",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "fk_users_departments_department_id",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "fk_users_locations_location_id",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "fk_users_user_roles_user_role_id",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "fk_users_users_added_by",
                table: "users");

            migrationBuilder.DropTable(
                name: "prospecting_clients");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropIndex(
                name: "ix_users_added_by",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_user_role_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_uoms_added_by",
                table: "uoms");

            migrationBuilder.DropIndex(
                name: "ix_term_days_added_by",
                table: "term_days");

            migrationBuilder.DropIndex(
                name: "ix_product_sub_categories_added_by",
                table: "product_sub_categories");

            migrationBuilder.DropIndex(
                name: "ix_product_categories_added_by",
                table: "product_categories");

            migrationBuilder.DropIndex(
                name: "ix_meat_types_added_by",
                table: "meat_types");

            migrationBuilder.DropIndex(
                name: "ix_locations_added_by",
                table: "locations");

            migrationBuilder.DropIndex(
                name: "ix_items_added_by",
                table: "items");

            migrationBuilder.DropIndex(
                name: "ix_discounts_added_by",
                table: "discounts");

            migrationBuilder.DropIndex(
                name: "ix_departments_added_by",
                table: "departments");

            migrationBuilder.DropIndex(
                name: "ix_companies_added_by",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "added_by",
                table: "users");

            migrationBuilder.DropColumn(
                name: "added_by",
                table: "uoms");

            migrationBuilder.DropColumn(
                name: "added_by",
                table: "product_sub_categories");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "product_categories");

            migrationBuilder.AlterColumn<int>(
                name: "location_id",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "department_id",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "company_id",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "addded_by",
                table: "uoms",
                type: "longtext",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "added_by",
                table: "term_days",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "added_by",
                table: "product_categories",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "added_by",
                table: "meat_types",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "added_by",
                table: "locations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "added_by",
                table: "items",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "added_by",
                table: "discounts",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "added_by",
                table: "departments",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "added_by",
                table: "companies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    fullname = table.Column<string>(type: "longtext", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customers", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "fk_users_companies_company_id",
                table: "users",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_departments_department_id",
                table: "users",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_locations_location_id",
                table: "users",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
