using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nis.Core.Persistence.Migrations
{
    public partial class add_medicalScale_and_medicalScaleActivity_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "medical_scales",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    scale_category = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_medical_scales", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "medical_scale_activities",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    score = table.Column<float>(type: "REAL", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    medical_scale_id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_medical_scale_activities", x => x.id);
                    table.ForeignKey(
                        name: "fk_medical_scale_activities_medical_scales_medical_scale_id",
                        column: x => x.medical_scale_id,
                        principalTable: "medical_scales",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_medical_scale_activities_medical_scale_id",
                table: "medical_scale_activities",
                column: "medical_scale_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "medical_scale_activities");

            migrationBuilder.DropTable(
                name: "medical_scales");
        }
    }
}
