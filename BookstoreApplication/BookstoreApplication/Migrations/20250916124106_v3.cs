using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAwardBridge_Author_AuthorId",
                table: "AuthorAwardBridge");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAwardBridge_Award_AwardId",
                table: "AuthorAwardBridge");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_AuthorId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Publisher_PublisherId",
                table: "Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book",
                table: "Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Award",
                table: "Award");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Author",
                table: "Author");

            migrationBuilder.RenameTable(
                name: "Publisher",
                newName: "Publishers");

            migrationBuilder.RenameTable(
                name: "Book",
                newName: "Books");

            migrationBuilder.RenameTable(
                name: "Award",
                newName: "Awards");

            migrationBuilder.RenameTable(
                name: "Author",
                newName: "Authors");

            migrationBuilder.RenameIndex(
                name: "IX_Book_PublisherId",
                table: "Books",
                newName: "IX_Books_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_AuthorId",
                table: "Books",
                newName: "IX_Books_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publishers",
                table: "Publishers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Awards",
                table: "Awards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "Birthday", "FullName" },
                values: new object[,]
                {
                    { 1, "British crime novelist known for detective stories", new DateTime(1890, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Agatha Christie" },
                    { 2, "American author of horror and supernatural fiction", new DateTime(1947, 9, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Stephen King" },
                    { 3, "British author best known for the Harry Potter series", new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Utc), "J.K. Rowling" },
                    { 4, "English novelist and essayist", new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "George Orwell" },
                    { 5, "American novelist known for To Kill a Mockingbird", new DateTime(1926, 4, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Harper Lee" }
                });

            migrationBuilder.InsertData(
                table: "Awards",
                columns: new[] { "Id", "Description", "Name", "StartYear" },
                values: new object[,]
                {
                    { 1, "Award for best science fiction or fantasy works", "Hugo Award", 1953 },
                    { 2, "Award for outstanding work in mystery genre", "Edgar Award", 1946 },
                    { 3, "Award for distinguished fiction", "Pulitzer Prize", 1918 },
                    { 4, "Award by Science Fiction Writers of America", "Nebula Award", 1965 }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Address", "Name", "Website" },
                values: new object[,]
                {
                    { 1, "1745 Broadway, New York, NY", "Penguin Random House", "https://www.penguinrandomhouse.com" },
                    { 2, "195 Broadway, New York, NY", "HarperCollins", "https://www.harpercollins.com" },
                    { 3, "1230 Avenue of the Americas, NY", "Simon & Schuster", "https://www.simonandschuster.com" }
                });

            migrationBuilder.InsertData(
                table: "AuthorAwardBridge",
                columns: new[] { "AuthorId", "AwardId", "YearAwarded" },
                values: new object[,]
                {
                    { 1, 2, 1955 },
                    { 1, 3, 1956 },
                    { 1, 4, 1962 },
                    { 2, 1, 1982 },
                    { 2, 2, 2009 },
                    { 2, 3, 1996 },
                    { 2, 4, 2015 },
                    { 3, 1, 2001 },
                    { 3, 2, 2004 },
                    { 3, 3, 2017 },
                    { 3, 4, 1971 },
                    { 4, 1, 1984 },
                    { 4, 2, 1949 },
                    { 5, 3, 1961 },
                    { 5, 4, 1961 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "ISBN", "PageCount", "PublishedDate", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "978-0-00-711930-6", 256, new DateTime(1934, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Murder on the Orient Express" },
                    { 2, 1, "978-0-00-711931-3", 272, new DateTime(1936, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), 2, "The ABC Murders" },
                    { 3, 1, "978-0-00-711932-0", 288, new DateTime(1937, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Death on the Nile" },
                    { 4, 2, "978-0-385-12167-5", 447, new DateTime(1977, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), 1, "The Shining" },
                    { 5, 2, "978-0-670-81302-4", 1138, new DateTime(1986, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, "It" },
                    { 6, 2, "978-0-385-08695-0", 199, new DateTime(1974, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Carrie" },
                    { 7, 3, "978-0-7475-3269-9", 223, new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Harry Potter and the Philosopher's Stone" },
                    { 8, 3, "978-0-7475-3849-3", 251, new DateTime(1998, 7, 2, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Harry Potter and the Chamber of Secrets" },
                    { 9, 3, "978-0-7475-4215-5", 317, new DateTime(1999, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Harry Potter and the Prisoner of Azkaban" },
                    { 10, 4, "978-0-452-28423-4", 328, new DateTime(1949, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc), 3, "1984" },
                    { 11, 4, "978-0-452-28424-1", 112, new DateTime(1945, 8, 17, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Animal Farm" },
                    { 12, 5, "978-0-06-112008-4", 376, new DateTime(1960, 7, 11, 0, 0, 0, 0, DateTimeKind.Utc), 2, "To Kill a Mockingbird" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAwardBridge_Authors_AuthorId",
                table: "AuthorAwardBridge",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAwardBridge_Awards_AwardId",
                table: "AuthorAwardBridge",
                column: "AwardId",
                principalTable: "Awards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAwardBridge_Authors_AuthorId",
                table: "AuthorAwardBridge");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAwardBridge_Awards_AwardId",
                table: "AuthorAwardBridge");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publishers",
                table: "Publishers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Awards",
                table: "Awards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "Publishers",
                newName: "Publisher");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Book");

            migrationBuilder.RenameTable(
                name: "Awards",
                newName: "Award");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Author");

            migrationBuilder.RenameIndex(
                name: "IX_Books_PublisherId",
                table: "Book",
                newName: "IX_Book_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorId",
                table: "Book",
                newName: "IX_Book_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book",
                table: "Book",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Award",
                table: "Award",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Author",
                table: "Author",
                column: "Id");

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
                name: "FK_Book_Author_AuthorId",
                table: "Book",
                column: "AuthorId",
                principalTable: "Author",
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
    }
}
