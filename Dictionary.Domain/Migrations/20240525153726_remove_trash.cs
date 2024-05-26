using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dictionary.Domain.Migrations
{
    /// <inheritdoc />
    public partial class remove_trash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TEST",
                table: "Faculties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TEST",
                table: "Faculties",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
