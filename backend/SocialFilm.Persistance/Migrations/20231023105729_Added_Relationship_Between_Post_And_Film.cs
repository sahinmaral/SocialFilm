using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialFilm.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Added_Relationship_Between_Post_And_Film : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilmId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_FilmDetails_FilmId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_FilmId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "FilmId",
                table: "Posts");
        }
    }
}
