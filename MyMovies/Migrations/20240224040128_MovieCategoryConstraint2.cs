using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovies.Migrations
{
    /// <inheritdoc />
    public partial class MovieCategoryConstraint2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Category_CategoryId",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Movie",
                newName: "CategoryId1");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_CategoryId",
                table: "Movie",
                newName: "IX_Movie_CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Category_CategoryId1",
                table: "Movie",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Category_CategoryId1",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "CategoryId1",
                table: "Movie",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_CategoryId1",
                table: "Movie",
                newName: "IX_Movie_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Category_CategoryId",
                table: "Movie",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
