using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovServe_Project.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentName",
                table: "CitizenDocument");

            migrationBuilder.AddColumn<int>(
                name: "DocumentID",
                table: "CitizenDocument",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CitizenDocument_DocumentID",
                table: "CitizenDocument",
                column: "DocumentID");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenDocument_RequiredDocuments_DocumentID",
                table: "CitizenDocument",
                column: "DocumentID",
                principalTable: "RequiredDocuments",
                principalColumn: "DocumentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitizenDocument_RequiredDocuments_DocumentID",
                table: "CitizenDocument");

            migrationBuilder.DropIndex(
                name: "IX_CitizenDocument_DocumentID",
                table: "CitizenDocument");

            migrationBuilder.DropColumn(
                name: "DocumentID",
                table: "CitizenDocument");

            migrationBuilder.AddColumn<string>(
                name: "DocumentName",
                table: "CitizenDocument",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
