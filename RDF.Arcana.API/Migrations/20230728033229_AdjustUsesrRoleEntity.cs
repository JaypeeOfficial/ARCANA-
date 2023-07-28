using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AdjustUsesrRoleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role_name",
                table: "user_roles",
                newName: "user_role_name");

            migrationBuilder.RenameColumn(
                name: "modiefied_by",
                table: "user_roles",
                newName: "modified_by");

            migrationBuilder.AddColumn<string>(
                name: "added_by",
                table: "user_roles",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "added_by",
                table: "user_roles");

            migrationBuilder.RenameColumn(
                name: "user_role_name",
                table: "user_roles",
                newName: "role_name");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "user_roles",
                newName: "modiefied_by");
        }
    }
}
