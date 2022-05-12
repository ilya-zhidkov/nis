using System.Text.Json;
using Nis.Core.Models;

namespace Nis.Core.Persistence.Seeders;

public class ExamSeeder : BaseSeeder
{
    public override void Seed(DataContext context, IServiceProvider services = null)
    {
        if (context.Exams.Any())
            return;

        var filePath = Path.Combine(Environment.CurrentDirectory, "../../../../../server/src/Nis.Core/Persistence/Seeders/Data/Exam.json"); //TODO: change path and check for existence of exam.json
        var examSeedData = JsonSerializer.Deserialize<Exam>(File.ReadAllText(filePath));
        
        context.Exams.Add(
            new Exam
            {
                Anamnesis = examSeedData.Anamnesis,
                DietId = examSeedData.DietId,
                DiagnosisId = examSeedData.DiagnosisId,
                DepartmentId = examSeedData.DepartmentId,
            }
        );
    }
}
