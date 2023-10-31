using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialFilm.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Removed_Post_Navigation_Property_Of_Comment_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_FilmDetails_FilmId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_FilmId",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "FilmId",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FilmId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_FilmId",
                table: "Posts",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_FilmDetails_FilmId",
                table: "Posts",
                column: "FilmId",
                principalTable: "FilmDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
