using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.Data.Migrations
{
    public partial class OwnerRemoveGeometry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Geometry_Lat",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "Geometry_Lng",
                table: "Owner");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Geometry_Lat",
                table: "Owner",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Geometry_Lng",
                table: "Owner",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Owner",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Geometry_Lat", "Geometry_Lng" },
                values: new object[] { 40.1345871, -74.9800064 });
        }
    }
}
