using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dictionary.Domain.Migrations
{
    /// <inheritdoc />
    public partial class many_to_many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypeEducationLevel_DocumentTypes_DocumentTypesId",
                table: "DocumentTypeEducationLevel");

            migrationBuilder.RenameColumn(
                name: "NextEducationLevelsId",
                table: "DocumentTypeEducationLevel",
                newName: "NextEducationLevelId");

            migrationBuilder.RenameColumn(
                name: "DocumentTypesId",
                table: "DocumentTypeEducationLevel",
                newName: "DocumentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentTypeEducationLevel_NextEducationLevelsId",
                table: "DocumentTypeEducationLevel",
                newName: "IX_DocumentTypeEducationLevel_NextEducationLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypeEducationLevel_DocumentTypes_DocumentTypeId",
                table: "DocumentTypeEducationLevel",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypeEducationLevel_DocumentTypes_DocumentTypeId",
                table: "DocumentTypeEducationLevel");

            migrationBuilder.RenameColumn(
                name: "NextEducationLevelId",
                table: "DocumentTypeEducationLevel",
                newName: "NextEducationLevelsId");

            migrationBuilder.RenameColumn(
                name: "DocumentTypeId",
                table: "DocumentTypeEducationLevel",
                newName: "DocumentTypesId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentTypeEducationLevel_NextEducationLevelId",
                table: "DocumentTypeEducationLevel",
                newName: "IX_DocumentTypeEducationLevel_NextEducationLevelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypeEducationLevel_DocumentTypes_DocumentTypesId",
                table: "DocumentTypeEducationLevel",
                column: "DocumentTypesId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
