using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustUserEntityandCustomerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_customers_companies_company_id",
                table: "customers");

            migrationBuilder.DropForeignKey(
                name: "fk_customers_departments_department_id",
                table: "customers");

            migrationBuilder.DropForeignKey(
                name: "fk_customers_locations_location_id",
                table: "customers");

            migrationBuilder.DropForeignKey(
                name: "fk_customers_roles_role_id",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "ix_customers_company_id",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "ix_customers_department_id",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "ix_customers_location_id",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "ix_customers_role_id",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "company_id",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "department_id",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "location_id",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "customers");

            migrationBuilder.AddColumn<int>(
                name: "company_id",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "department_id",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "location_id",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "role_id",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "customer_id",
                table: "locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "customer_id",
                table: "departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "customer_id",
                table: "companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_users_company_id",
                table: "users",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_department_id",
                table: "users",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_location_id",
                table: "users",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_locations_customer_id",
                table: "locations",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_departments_customer_id",
                table: "departments",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_companies_customer_id",
                table: "companies",
                column: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "fk_companies_customers_customer_id",
                table: "companies",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_departments_customers_customer_id",
                table: "departments",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_locations_customers_customer_id",
                table: "locations",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "fk_users_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_companies_customers_customer_id",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "fk_departments_customers_customer_id",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "fk_locations_customers_customer_id",
                table: "locations");

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
                name: "fk_users_roles_role_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_company_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_department_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_location_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_role_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_locations_customer_id",
                table: "locations");

            migrationBuilder.DropIndex(
                name: "ix_departments_customer_id",
                table: "departments");

            migrationBuilder.DropIndex(
                name: "ix_companies_customer_id",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "company_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "department_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "location_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "customer_id",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "customer_id",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "customer_id",
                table: "companies");

            migrationBuilder.AddColumn<int>(
                name: "company_id",
                table: "customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "department_id",
                table: "customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "location_id",
                table: "customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "role_id",
                table: "customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_customers_company_id",
                table: "customers",
                column: "company_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_customers_department_id",
                table: "customers",
                column: "department_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_customers_location_id",
                table: "customers",
                column: "location_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_customers_role_id",
                table: "customers",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_customers_companies_company_id",
                table: "customers",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_customers_departments_department_id",
                table: "customers",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_customers_locations_location_id",
                table: "customers",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_customers_roles_role_id",
                table: "customers",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
