using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EYEAPI.Migrations
{
    /// <inheritdoc />
    public partial class asdvsdcCACACS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MetaDataId",
                table: "Machines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MachineMetaData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    TotalStorage = table.Column<int>(type: "int", nullable: false),
                    TotalMemory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineMetaData", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Machines_MetaDataId",
                table: "Machines",
                column: "MetaDataId",
                unique: true,
                filter: "[MetaDataId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_MachineMetaData_MetaDataId",
                table: "Machines",
                column: "MetaDataId",
                principalTable: "MachineMetaData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_MachineMetaData_MetaDataId",
                table: "Machines");

            migrationBuilder.DropTable(
                name: "MachineMetaData");

            migrationBuilder.DropIndex(
                name: "IX_Machines_MetaDataId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "MetaDataId",
                table: "Machines");
        }
    }
}
