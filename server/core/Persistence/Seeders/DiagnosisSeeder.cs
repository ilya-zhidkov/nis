using Nis.Core.Models;

namespace Nis.Core.Persistence.Seeders;

public class DiagnosisSeeder : BaseSeeder
{
    public override void Seed(DataContext context, IServiceProvider services = null)
    {
        if (context.Diagnoses.Any())
            return;

        context.Diagnoses.AddRange(
            // Infekční oddělení
            new Diagnosis
            {
                Name = "Infekce způsobené salmonelami",
                DepartmentId = 1
            },
             new Diagnosis
             {
                 Name = "Shigelóza",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Bakteriální intoxikace – otravy, přenesené potravou",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Střevní infekce viry a jinými určenými mikroorganismy",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Gastroenteritida a kolitida infekčního a NS původu",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Tuberkulóza",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Tetanus",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Listerióza",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Meningokoková infekce",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Streptokoková sepse",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Pohlavním stykem přenášené nemoci",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Encefalitida",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Meningitida",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Infekce virem Herpes Simplex",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Virová hepatitida",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Infekční mononukleóza",
                 DepartmentId = 1
             }, new Diagnosis
             {
                 Name = "Svrab – scabies",
                 DepartmentId = 1
             }
             // Oddělení hematologie
             , new Diagnosis
             {
                 Name = "Leidenská mutace",
                 DepartmentId = 2
             }
             , new Diagnosis
             {
                 Name = "Leukemie",
                 DepartmentId = 2
             }
             , new Diagnosis
             {
                 Name = "Diseminovaná intravaskulární koagulace ",
                 DepartmentId = 2
             }
             , new Diagnosis
             {
                 Name = "Agranulocytóza",
                 DepartmentId = 2
             }
             , new Diagnosis
             {
                 Name = "Anemie",
                 DepartmentId = 2
             }
             , new Diagnosis
             {
                 Name = "Koagulopathie",
                 DepartmentId = 2
             }
             //Oddělení endokrinologie a diabetologie
             , new Diagnosis
             {
                 Name = "Hypertyreóza",
                 DepartmentId = 3
             }
             , new Diagnosis
             {
                 Name = "Hypotyreóza",
                 DepartmentId = 3
             }
             , new Diagnosis
             {
                 Name = "Diabetes mellitus",
                 DepartmentId = 3
             }
             , new Diagnosis
             {
                 Name = "Hypoparatyreóza",
                 DepartmentId = 3
             }
             , new Diagnosis
             {
                 Name = "Hyperparatyreóza a jiné nemoci příštítných tělísek ",
                 DepartmentId = 3
             }
             , new Diagnosis
             {
                 Name = "Hyperfunkce hypofýzy – glandulae pituitariae – podvěsku mozkového",
                 DepartmentId = 3
             }
             , new Diagnosis
             {
                 Name = "Hypofunkce a jiné poruchy hypofýzy",
                 DepartmentId = 3
             }
             , new Diagnosis
             {
                 Name = "Cushingův syndrom",
                 DepartmentId = 3
             }
             , new Diagnosis
             {
                 Name = "Hyperaldosteronismus",
                 DepartmentId = 3
             }
             // Oddělení lékařské genetiky
             , new Diagnosis
             {
                 Name = "Cystická fibróza",
                 DepartmentId = 4
             }
             // Neurologické oddělení
             , new Diagnosis
             {
                 Name = "Alzheimerova nemoc",
                 DepartmentId = 5
             }
             , new Diagnosis
             {
                 Name = "Demence",
                 DepartmentId = 5
             }
             , new Diagnosis
             {
                 Name = "Schizofrenie",
                 DepartmentId = 5
             }
             , new Diagnosis
             {
                 Name = "Poruchy chování",
                 DepartmentId = 5
             }
             , new Diagnosis
             {
                 Name = "Roztroušená skleróza – sclerosis multiplex",
                 DepartmentId = 5
             }
             , new Diagnosis
             {
                 Name = "Migrena",
                 DepartmentId = 5
             }
             , new Diagnosis
             {
                 Name = "Epilepsie",
                 DepartmentId = 5
             }
             , new Diagnosis
             {
                 Name = "Sclerosis multiplex",
                 DepartmentId = 5
             }
             , new Diagnosis
             {
                 Name = "Parkinsonismus",
                 DepartmentId = 5
             }
             , new Diagnosis
             {
                 Name = "Hydrocefalus",
                 DepartmentId = 5
             }
             , new Diagnosis
             {
                 Name = "Encefalopatie",
                 DepartmentId = 5
             }
             , new Diagnosis
             {
                 Name = "Mozková obrna",
                 DepartmentId = 5
             }
             , new Diagnosis
             {
                 Name = "Cévní mozková příhoda",
                 DepartmentId = 5
             }
             // Kardiologie
             , new Diagnosis
             {
                 Name = "Hypertenze",
                 DepartmentId = 6
             }
             , new Diagnosis
             {
                 Name = "Angina pectoris",
                 DepartmentId = 6
             }
             , new Diagnosis
             {
                 Name = "Infarkt myokardu",
                 DepartmentId = 6
             }
             , new Diagnosis
             {
                 Name = "Ischemická choroba srdeční",
                 DepartmentId = 6
             }
             , new Diagnosis
             {
                 Name = "Ateroskleróza",
                 DepartmentId = 6
             }
             , new Diagnosis
             {
                 Name = "Srdeční selhání",
                 DepartmentId = 6
             }

             , new Diagnosis
             {
                 Name = "Ischemická choroba dolních končetin",
                 DepartmentId = 6
             }
             // Plicní oddělení
             , new Diagnosis
             {
                 Name = "Pneumonie",
                 DepartmentId = 7
             }
             , new Diagnosis
             {
                 Name = "Plicní embolie",
                 DepartmentId = 7
             }
             , new Diagnosis
             {
                 Name = "Asthma bronchiale",
                 DepartmentId = 7
             }
             , new Diagnosis
             {
                 Name = "Chronická obštrukční plicní nemoc",
                 DepartmentId = 7
             }
             , new Diagnosis
             {
                 Name = "Respirační insuficience",
                 DepartmentId = 7
             }
             // Gastroenterologie
             , new Diagnosis
             {
                 Name = "Gastritida",
                 DepartmentId = 8
             }
             , new Diagnosis
             {
                 Name = "Vředová choroba gastroduodena",
                 DepartmentId = 8
             }
             , new Diagnosis
             {
                 Name = "Pankreatitida",
                 DepartmentId = 8
             }
             , new Diagnosis
             {
                 Name = "Obezita",
                 DepartmentId = 8
             }
             , new Diagnosis
             {
                 Name = "Malnutrice",
                 DepartmentId = 8
             }
             , new Diagnosis
             {
                 Name = "Ulcerózní kolitida",
                 DepartmentId = 8
             }
             , new Diagnosis
             {
                 Name = "Crohnova nemoc",
                 DepartmentId = 8
             }
             , new Diagnosis
             {
                 Name = "Enteritida",
                 DepartmentId = 8
             }
             // Ortopedie
             , new Diagnosis
             {
                 Name = "Artróza",
                 DepartmentId = 9
             }
             , new Diagnosis
             {
                 Name = "Revmatoidní artritida",
                 DepartmentId = 9
             }
             , new Diagnosis
             {
                 Name = "Kolagenózy",
                 DepartmentId = 9
             }
             , new Diagnosis
             {
                 Name = "Osteoporóza",
                 DepartmentId = 9
             }
             // Onkologie                
             , new Diagnosis
             {
                 Name = "Novotvary",
                 DepartmentId = 10
             }
             // Nefrologie a urologie
             , new Diagnosis
             {
                 Name = "Nefrologie a urologie",
                 DepartmentId = 11
             }
             , new Diagnosis
             {
                 Name = "Cystitida",
                 DepartmentId = 11
             }
             , new Diagnosis
             {
                 Name = "Renální selhání",
                 DepartmentId = 11
             }
             , new Diagnosis
             {
                 Name = "Glomerulonefritída",
                 DepartmentId = 11
             }
            );
    }
}
