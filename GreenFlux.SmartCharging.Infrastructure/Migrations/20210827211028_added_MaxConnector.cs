using Microsoft.EntityFrameworkCore.Migrations;

namespace GreenFlux.SmartCharging.Infrastructure.Migrations
{
    public partial class added_MaxConnector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxConnectors",
                table: "ChargeStations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxConnectors",
                table: "ChargeStations");
        }
    }
}
