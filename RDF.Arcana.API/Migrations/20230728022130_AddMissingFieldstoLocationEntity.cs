using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingFieldstoLocationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
