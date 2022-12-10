using Nis.Core.Models;
using System.Text.Json;

namespace Nis.Core.Persistence.Seeders;

public class ExamSeeder : BaseSeeder
{
    public override void Seed(DataContext context, IServiceProvider services = null)
    {
        if (context.Exams.Any())
            return;

        var path = Path.Combine(AppContext.BaseDirectory, "Persistence/Seeders/Data/Exam.json");
        var exam = JsonSerializer.Deserialize<Exam>(File.ReadAllText(path))!;

        context.Exams.Add(
            new Exam
            {
                Anamnesis = exam.Anamnesis,
                DietId = exam.DietId,
                DiagnosisId = exam.DiagnosisId,
                DepartmentId = exam.DepartmentId,
            }
        );
    }
}

