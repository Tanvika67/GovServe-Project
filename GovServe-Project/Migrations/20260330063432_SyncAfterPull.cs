using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovServe_Project.Migrations
{
    /// <inheritdoc />
    public partial class SyncAfterPull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Case_User_UsersUserId",
                table: "Case");

            migrationBuilder.DropIndex(
                name: "IX_Case_UsersUserId",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "Case");

            migrationBuilder.CreateIndex(
                name: "IX_Case_AssignedOfficerId",
                table: "Case",
                column: "AssignedOfficerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Case_User_AssignedOfficerId",
                table: "Case",
                column: "AssignedOfficerId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Case_User_AssignedOfficerId",
                table: "Case");

            migrationBuilder.DropIndex(
                name: "IX_Case_AssignedOfficerId",
                table: "Case");

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "Case",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Case_UsersUserId",
                table: "Case",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Case_User_UsersUserId",
                table: "Case",
                column: "UsersUserId",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}
