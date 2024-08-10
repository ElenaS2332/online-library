using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Library.Repository.Migrations
{
    /// <inheritdoc />
    public partial class MakeReadingListAndSubscriptionRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ReadingLists_ReadingListId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Subscriptions_UserSubscriptionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ReadingListId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserSubscriptionId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserSubscriptionId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReadingListId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ReadingListId",
                table: "AspNetUsers",
                column: "ReadingListId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserSubscriptionId",
                table: "AspNetUsers",
                column: "UserSubscriptionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ReadingLists_ReadingListId",
                table: "AspNetUsers",
                column: "ReadingListId",
                principalTable: "ReadingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Subscriptions_UserSubscriptionId",
                table: "AspNetUsers",
                column: "UserSubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ReadingLists_ReadingListId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Subscriptions_UserSubscriptionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ReadingListId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserSubscriptionId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserSubscriptionId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReadingListId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ReadingLists_ReadingListId",
                table: "AspNetUsers",
                column: "ReadingListId",
                principalTable: "ReadingLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Subscriptions_UserSubscriptionId",
                table: "AspNetUsers",
                column: "UserSubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");
        }
    }
}
