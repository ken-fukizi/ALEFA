using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI.Data.Migrations
{
    public partial class clinicaldatamodelsrenameclinicaldata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicalDataModels",
                table: "ClinicalDataModels");

            migrationBuilder.RenameTable(
                name: "ClinicalDataModels",
                newName: "ClinicalData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicalData",
                table: "ClinicalData",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicalData",
                table: "ClinicalData");

            migrationBuilder.RenameTable(
                name: "ClinicalData",
                newName: "ClinicalDataModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicalDataModels",
                table: "ClinicalDataModels",
                column: "Id");
        }
    }
}
