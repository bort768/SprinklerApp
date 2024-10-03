using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GateApiSpirinklerApp.Migrations
{
    /// <inheritdoc />
    public partial class AddNewStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryOfTankWaterLevels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TankId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WaterLevel = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryOfTankWaterLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryOfTankWaterLevels_Tanks_TankId",
                        column: x => x.TankId,
                        principalTable: "Tanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sprinklers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprinklers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryOfIrrigations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    TankId = table.Column<int>(type: "int", nullable: false),
                    TankId1 = table.Column<long>(type: "bigint", nullable: false),
                    SprinklerId = table.Column<int>(type: "int", nullable: false),
                    SprinklerId1 = table.Column<long>(type: "bigint", nullable: false),
                    AmountOfWaterUsed = table.Column<double>(type: "float", nullable: false),
                    WaterLevelBefore = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryOfIrrigations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryOfIrrigations_Sprinklers_SprinklerId1",
                        column: x => x.SprinklerId1,
                        principalTable: "Sprinklers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryOfIrrigations_Tanks_TankId1",
                        column: x => x.TankId1,
                        principalTable: "Tanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IrrigationSchedules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MinimumTankLevel = table.Column<double>(type: "float", nullable: false),
                    TankId = table.Column<int>(type: "int", nullable: false),
                    TankId1 = table.Column<long>(type: "bigint", nullable: false),
                    SprinklerId = table.Column<int>(type: "int", nullable: false),
                    SprinklerId1 = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IrrigationSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IrrigationSchedules_Sprinklers_SprinklerId1",
                        column: x => x.SprinklerId1,
                        principalTable: "Sprinklers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IrrigationSchedules_Tanks_TankId1",
                        column: x => x.TankId1,
                        principalTable: "Tanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOfIrrigations_SprinklerId1",
                table: "HistoryOfIrrigations",
                column: "SprinklerId1");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOfIrrigations_TankId1",
                table: "HistoryOfIrrigations",
                column: "TankId1");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOfTankWaterLevels_TankId",
                table: "HistoryOfTankWaterLevels",
                column: "TankId");

            migrationBuilder.CreateIndex(
                name: "IX_IrrigationSchedules_SprinklerId1",
                table: "IrrigationSchedules",
                column: "SprinklerId1");

            migrationBuilder.CreateIndex(
                name: "IX_IrrigationSchedules_TankId1",
                table: "IrrigationSchedules",
                column: "TankId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryOfIrrigations");

            migrationBuilder.DropTable(
                name: "HistoryOfTankWaterLevels");

            migrationBuilder.DropTable(
                name: "IrrigationSchedules");

            migrationBuilder.DropTable(
                name: "Sprinklers");
        }
    }
}
