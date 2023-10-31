using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialFilm.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Added_Relationship_Between_SavedFilm_And_FilmDetail_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedFilms_FilmDetails_FilmDetailId",
                table: "SavedFilms");

            migrationBuilder.DropIndex(
                name: "IX_SavedFilms_FilmDetailId",
                table: "SavedFilms");

            migrationBuilder.DropColumn(
                name: "FilmDetailId",
                table: "SavedFilms");

            migrationBuilder.CreateIndex(
                name: "IX_SavedFilms_FilmId",
                table: "SavedFilms",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedFilms_FilmDetails_FilmId",
                table: "SavedFilms",
                column: "FilmId",
                principalTable: "FilmDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedFilms_FilmDetails_FilmId",
                table: "SavedFilms");

            migrationBuilder.DropIndex(
                name: "IX_SavedFilms_FilmId",
                table: "SavedFilms");

            migrationBuilder.AddColumn<string>(
                name: "FilmDetailId",
                table: "SavedFilms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SavedFilms_FilmDetailId",
                table: "SavedFilms",
                column: "FilmDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedFilms_FilmDetails_FilmDetailId",
                table: "SavedFilms",
                column: "FilmDetailId",
                principalTable: "FilmDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
