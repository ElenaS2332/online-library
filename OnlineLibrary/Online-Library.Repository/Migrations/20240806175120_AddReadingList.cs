using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Library.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddReadingList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserSubscriptionId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Subscriptions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReadingListId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReadingLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BooksInReadingLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReadingListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksInReadingLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooksInReadingLists_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksInReadingLists_ReadingLists_ReadingListId",
                        column: x => x.ReadingListId,
                        principalTable: "ReadingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ReadingListId",
                table: "AspNetUsers",
                column: "ReadingListId",
                unique: true,
                filter: "[ReadingListId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserSubscriptionId",
                table: "AspNetUsers",
                column: "UserSubscriptionId",
                unique: true,
                filter: "[UserSubscriptionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BooksInReadingLists_BookId",
                table: "BooksInReadingLists",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksInReadingLists_ReadingListId",
                table: "BooksInReadingLists",
                column: "ReadingListId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ReadingLists_ReadingListId",
                table: "AspNetUsers",
                column: "ReadingListId",
                principalTable: "ReadingLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ReadingLists_ReadingListId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BooksInReadingLists");

            migrationBuilder.DropTable(
                name: "ReadingLists");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ReadingListId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserSubscriptionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ReadingListId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserSubscriptionId",
                table: "AspNetUsers",
                column: "UserSubscriptionId");
        }
    }
}
