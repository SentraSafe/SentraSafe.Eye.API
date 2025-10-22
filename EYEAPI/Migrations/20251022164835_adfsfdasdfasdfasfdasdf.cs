using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EYEAPI.Migrations
{
    /// <inheritdoc />
    public partial class adfsfdasdfasdfasfdasdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Machines_MetaDataId",
                table: "Machines");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_MetaDataId",
                table: "Machines",
                column: "MetaDataId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineMetaData_MachineId",
                table: "MachineMetaData",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineMetaData_Machines_MachineId",
                table: "MachineMetaData",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineMetaData_Machines_MachineId",
                table: "MachineMetaData");

            migrationBuilder.DropIndex(
                name: "IX_Machines_MetaDataId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_MachineMetaData_MachineId",
                table: "MachineMetaData");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_MetaDataId",
                table: "Machines",
                column: "MetaDataId",
                unique: true,
                filter: "[MetaDataId] IS NOT NULL");
        }
    }
}
