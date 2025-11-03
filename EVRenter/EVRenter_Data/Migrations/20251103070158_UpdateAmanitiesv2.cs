using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVRenter_Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAmanitiesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmenityName",
                table: "Amenities",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Amenities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Amenities");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Amenities",
                newName: "AmenityName");
        }
    }
}
