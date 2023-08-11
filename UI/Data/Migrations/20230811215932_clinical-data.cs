using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI.Data.Migrations
{
    public partial class clinicaldata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClinicalDataModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    HgCount = table.Column<int>(type: "int", nullable: false),
                    LastVisitDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastPrescriptions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrackingState = table.Column<int>(type: "int", nullable: false),
                    EntityIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalDataModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicalDataModels");
        }
    }
}
