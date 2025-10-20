using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EYEAPI.Migrations
{
    /// <inheritdoc />
    public partial class asdfgh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MachineType",
                table: "Alarms",
                newName: "ValueType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValueType",
                table: "Alarms",
                newName: "MachineType");
        }
    }
}
