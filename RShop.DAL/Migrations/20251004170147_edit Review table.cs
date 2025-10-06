using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editReviewtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RevieqDate",
                table: "Reviews",
                newName: "ReviewDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewDate",
                table: "Reviews",
                newName: "RevieqDate");
        }
    }
}
