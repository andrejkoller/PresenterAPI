using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PresenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserRelationFromPresentation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presentations_Users_OwnerId",
                table: "Presentations");

            migrationBuilder.DropIndex(
                name: "IX_Presentations_OwnerId",
                table: "Presentations");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Presentations_OwnerId",
                table: "Presentations",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presentations_Users_OwnerId",
                table: "Presentations",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
