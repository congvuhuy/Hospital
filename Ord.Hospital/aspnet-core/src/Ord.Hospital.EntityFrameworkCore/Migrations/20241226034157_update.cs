using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ord.Hospital.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProcevinCode",
                table: "Patient",
                newName: "ProvinceCode");

            migrationBuilder.RenameColumn(
                name: "HopitalID",
                table: "Patient",
                newName: "HospitalID");

            migrationBuilder.RenameColumn(
                name: "CommnuneCode",
                table: "Patient",
                newName: "CommuneCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProvinceCode",
                table: "Patient",
                newName: "ProcevinCode");

            migrationBuilder.RenameColumn(
                name: "HospitalID",
                table: "Patient",
                newName: "HopitalID");

            migrationBuilder.RenameColumn(
                name: "CommuneCode",
                table: "Patient",
                newName: "CommnuneCode");
        }
    }
}
