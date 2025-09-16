using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAward_Author_AuthorId",
                table: "AuthorAward");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAward_Award_AwardId",
                table: "AuthorAward");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAward_Award_AwardId1",
                table: "AuthorAward");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Publisher_PublisherId",
                table: "Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorAward",
                table: "AuthorAward");

            migrationBuilder.DropIndex(
                name: "IX_AuthorAward_AwardId1",
                table: "AuthorAward");

            migrationBuilder.DropColumn(
                name: "AwardId1",
                table: "AuthorAward");

            migrationBuilder.RenameTable(
                name: "AuthorAward",
                newName: "AuthorAwardBridge");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Author",
                newName: "Birthday");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorAward_AwardId",
                table: "AuthorAwardBridge",
                newName: "IX_AuthorAwardBridge_AwardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorAwardBridge",
                table: "AuthorAwardBridge",
                columns: new[] { "AuthorId", "AwardId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAwardBridge_Author_AuthorId",
                table: "AuthorAwardBridge",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAwardBridge_Award_AwardId",
                table: "AuthorAwardBridge",
                column: "AwardId",
                principalTable: "Award",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Publisher_PublisherId",
                table: "Book",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAwardBridge_Author_AuthorId",
                table: "AuthorAwardBridge");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAwardBridge_Award_AwardId",
                table: "AuthorAwardBridge");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Publisher_PublisherId",
                table: "Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorAwardBridge",
                table: "AuthorAwardBridge");

            migrationBuilder.RenameTable(
                name: "AuthorAwardBridge",
                newName: "AuthorAward");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "Author",
                newName: "DateOfBirth");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorAwardBridge_AwardId",
                table: "AuthorAward",
                newName: "IX_AuthorAward_AwardId");

            migrationBuilder.AddColumn<int>(
                name: "AwardId1",
                table: "AuthorAward",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorAward",
                table: "AuthorAward",
                columns: new[] { "AuthorId", "AwardId" });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorAward_AwardId1",
                table: "AuthorAward",
                column: "AwardId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAward_Author_AuthorId",
                table: "AuthorAward",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAward_Award_AwardId",
                table: "AuthorAward",
                column: "AwardId",
                principalTable: "Award",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAward_Award_AwardId1",
                table: "AuthorAward",
                column: "AwardId1",
                principalTable: "Award",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Publisher_PublisherId",
                table: "Book",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
