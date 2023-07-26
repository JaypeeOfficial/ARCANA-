using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustMeatTypeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // add new column
            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "meat_types",
                nullable: true);
         
            // IF 'type' column has data, you would need to copy it to 'updated_at' column
         
            // drop old column
            migrationBuilder.DropColumn(
                name: "type",
                table: "meat_types");
         
            migrationBuilder.AddColumn<string>(
                name: "added_by",
                table: "meat_types",
                type: "varchar(255)",
                nullable: true);
         
            migrationBuilder.AddColumn<string>(
                name: "modified_by",
                table: "meat_types",
                type: "varchar(255)",
                nullable: true);
        }
         
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "added_by",
                table: "meat_types");
         
            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "meat_types");
         
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "meat_types",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
         
            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "meat_types");
        }
    }
}