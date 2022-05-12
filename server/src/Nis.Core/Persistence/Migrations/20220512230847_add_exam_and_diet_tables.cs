using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nis.Core.Persistence.Migrations
{
    public partial class add_exam_and_diet_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "diets",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_diets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "exams",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    anamnesis = table.Column<string>(type: "TEXT", nullable: true),
                    diet_id = table.Column<int>(type: "INTEGER", nullable: false),
                    diagnosis_id = table.Column<int>(type: "INTEGER", nullable: false),
                    department_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exams", x => x.id);
                    table.ForeignKey(
                        name: "fk_exams_departments_department_id",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_exams_diagnoses_diagnosis_id",
                        column: x => x.diagnosis_id,
                        principalTable: "diagnoses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_exams_diets_diet_id",
                        column: x => x.diet_id,
                        principalTable: "diets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_exams_department_id",
                table: "exams",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_exams_diagnosis_id",
                table: "exams",
                column: "diagnosis_id");

            migrationBuilder.CreateIndex(
                name: "ix_exams_diet_id",
                table: "exams",
                column: "diet_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exams");

            migrationBuilder.DropTable(
                name: "diets");
        }
    }
}
