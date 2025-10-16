using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EYEAPI.Migrations
{
    /// <inheritdoc />
    public partial class ewlrhjfgjkdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "Alarms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_MachineId",
                table: "Alarms",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alarms_Machines_MachineId",
                table: "Alarms",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alarms_Machines_MachineId",
                table: "Alarms");

            migrationBuilder.DropIndex(
                name: "IX_Alarms_MachineId",
                table: "Alarms");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "Alarms");
        }
    }
}
