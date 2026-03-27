using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovServe_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddUserColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appeals_User_UsersUserId",
                table: "Appeals");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_User_UsersUserId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Case_User_AssignedOfficerId",
                table: "Case");

            migrationBuilder.DropForeignKey(
                name: "FK_Case_User_UsersUserId",
                table: "Case");

            migrationBuilder.DropIndex(
                name: "IX_Case_UsersUserId",
                table: "Case");

            migrationBuilder.DropIndex(
                name: "IX_Application_UsersUserId",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Appeals_UsersUserId",
                table: "Appeals");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "Appeals");

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

            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "Case",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "Case",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "Application",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "Appeals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Case_UsersUserId",
                table: "Case",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_UsersUserId",
                table: "Application",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appeals_UsersUserId",
                table: "Appeals",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appeals_User_UsersUserId",
                table: "Appeals",
                column: "UsersUserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_User_UsersUserId",
                table: "Application",
                column: "UsersUserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Case_User_AssignedOfficerId",
                table: "Case",
                column: "AssignedOfficerId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Case_User_UsersUserId",
                table: "Case",
                column: "UsersUserId",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}
