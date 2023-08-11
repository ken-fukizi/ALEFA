using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI.Data.Migrations
{
    public partial class symptoms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Options = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrackingState = table.Column<int>(type: "int", nullable: false),
                    EntityIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Symptoms");
        }
    }
}
