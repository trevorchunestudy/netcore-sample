using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Geometry_Lat = table.Column<double>(nullable: false),
                    Geometry_Lng = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OwnerId = table.Column<long>(nullable: false),
                    Automobile_VehicleId = table.Column<string>(maxLength: 100, nullable: true),
                    Automobile_Make = table.Column<string>(maxLength: 25, nullable: true),
                    Automobile_Model = table.Column<string>(maxLength: 25, nullable: true),
                    Automobile_Year = table.Column<int>(nullable: false),
                    Automobile_Transmission = table.Column<string>(maxLength: 10, nullable: true),
                    Automobile_FuelType = table.Column<string>(maxLength: 10, nullable: true),
                    Automobile_BodyStyle = table.Column<string>(maxLength: 12, nullable: true),
                    Automobile_DriveTrain = table.Column<string>(maxLength: 10, nullable: true),
                    Automobile_Vin = table.Column<string>(maxLength: 17, nullable: true),
                    Automobile_Availablity = table.Column<string>(maxLength: 12, nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Mileage_Value = table.Column<int>(nullable: false),
                    Mileage_Unit = table.Column<string>(maxLength: 2, nullable: true),
                    Url = table.Column<string>(maxLength: 2083, nullable: true),
                    ImageUrl = table.Column<string>(maxLength: 2083, nullable: true),
                    ImageTag = table.Column<string>(maxLength: 2083, nullable: true),
                    Condition = table.Column<string>(maxLength: 10, nullable: true),
                    Price = table.Column<string>(maxLength: 25, nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ExteriorColor = table.Column<string>(maxLength: 25, nullable: true),
                    SalePrice = table.Column<string>(maxLength: 25, nullable: true),
                    StateOfVehicle = table.Column<string>(maxLength: 4, nullable: true),
                    Geometry_Lat = table.Column<double>(nullable: false),
                    Geometry_Lng = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicle_Owner_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Owner",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name", "Geometry_Lat", "Geometry_Lng" },
                values: new object[] { 1L, new DateTime(2018, 8, 21, 17, 0, 0, 0, DateTimeKind.Utc), false, "Joe", 40.1345871, -74.9800064 });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_OwnerId",
                table: "Vehicle",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "Owner");
        }
    }
}
