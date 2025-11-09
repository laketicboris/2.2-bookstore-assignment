using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddVolumeFieldsToIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VolumeApiDetailUrl",
                table: "Issues",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolumeId",
                table: "Issues",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VolumeName",
                table: "Issues",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolumeApiDetailUrl",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "VolumeId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "VolumeName",
                table: "Issues");
        }
    }
}
