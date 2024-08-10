using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Library.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCountFromModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "ReadingLists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "ReadingLists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
