using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GateApiSpirinklerApp.Migrations
{
    /// <inheritdoc />
    public partial class TankModelChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tanks",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tanks");
        }
    }
}
