using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVRenter_Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ChargePower",
                table: "Models",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChargingTime",
                table: "Models",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ChargePower",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "ChargingTime",
                table: "Models");
        }
    }
}
