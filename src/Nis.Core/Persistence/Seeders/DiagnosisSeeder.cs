using System;
using System.Linq;
using Nis.Core.Models.Diagnosis;

namespace Nis.Core.Persistence.Seeders
{
    public class DiagnosisSeeder : BaseSeeder
    {
        public override void Seed(DataContext context, IServiceProvider services = null)
        {
            if (context.Diagnoses.Any())
                return;

            context.Diagnoses.AddRange(
                // Infekční oddělení
                new Diagnose
                {
                    Name = "Infekce způsobené salmonelami",
                    DepartmentId = 1
                },
                 new Diagnose
                 {
                     Name = "Shigelóza",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Bakteriální intoxikace – otravy, přenesené potravou",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Střevní infekce viry a jinými určenými mikroorganismy",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Gastroenteritida a kolitida infekčního a NS původu",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Tuberkulóza",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Tetanus",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Listerióza",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Meningokoková infekce",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Streptokoková sepse",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Pohlavním stykem přenášené nemoci",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Encefalitida",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Meningitida",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Infekce virem Herpes Simplex",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Virová hepatitida",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Infekční mononukleóza",
                     DepartmentId = 1
                 }, new Diagnose
                 {
                     Name = "Svrab – scabies",
                     DepartmentId = 1
                 }
                 // Oddělení hematologie
                 , new Diagnose
                 {
                     Name = "Leidenská mutace",
                     DepartmentId = 2
                 }
                 , new Diagnose
                 {
                     Name = "Leukemie",
                     DepartmentId = 2
                 }
                 , new Diagnose
                 {
                     Name = "Diseminovaná intravaskulární koagulace ",
                     DepartmentId = 2
                 }
                 , new Diagnose
                 {
                     Name = "Agranulocytóza",
                     DepartmentId = 2
                 }
                 , new Diagnose
                 {
                     Name = "Anemie",
                     DepartmentId = 2
                 }
                 , new Diagnose
                 {
                     Name = "Koagulopathie",
                     DepartmentId = 2
                 }
                 //Oddělení endokrinologie a diabetologie
                 , new Diagnose
                 {
                     Name = "Hypertyreóza",
                     DepartmentId = 3
                 }
                 , new Diagnose
                 {
                     Name = "Hypotyreóza",
                     DepartmentId = 3
                 }
                 , new Diagnose
                 {
                     Name = "Diabetes mellitus",
                     DepartmentId = 3
                 }
                 , new Diagnose
                 {
                     Name = "Hypoparatyreóza",
                     DepartmentId = 3
                 }
                 , new Diagnose
                 {
                     Name = "Hyperparatyreóza a jiné nemoci příštítných tělísek ",
                     DepartmentId = 3
                 }
                 , new Diagnose
                 {
                     Name = "Hyperfunkce hypofýzy – glandulae pituitariae – podvěsku mozkového",
                     DepartmentId = 3
                 }
                 , new Diagnose
                 {
                     Name = "Hypofunkce a jiné poruchy hypofýzy",
                     DepartmentId = 3
                 }
                 , new Diagnose
                 {
                     Name = "Cushingův syndrom",
                     DepartmentId = 3
                 }
                 , new Diagnose
                 {
                     Name = "Hyperaldosteronismus",
                     DepartmentId = 3
                 }
                 // Oddělení lékařské genetiky
                 , new Diagnose
                 {
                     Name = "Cystická fibróza",
                     DepartmentId = 4
                 }
                 // Neurologické oddělení
                 , new Diagnose
                 {
                     Name = "Alzheimerova nemoc",
                     DepartmentId = 5
                 }
                 , new Diagnose
                 {
                     Name = "Demence",
                     DepartmentId = 5
                 }
                 , new Diagnose
                 {
                     Name = "Schizofrenie",
                     DepartmentId = 5
                 }
                 , new Diagnose
                 {
                     Name = "Poruchy chování",
                     DepartmentId = 5
                 }
                 , new Diagnose
                 {
                     Name = "Roztroušená skleróza – sclerosis multiplex",
                     DepartmentId = 5
                 }
                 , new Diagnose
                 {
                     Name = "Migrena",
                     DepartmentId = 5
                 }
                 , new Diagnose
                 {
                     Name = "Epilepsie",
                     DepartmentId = 5
                 }
                 , new Diagnose
                 {
                     Name = "Sclerosis multiplex",
                     DepartmentId = 5
                 }
                 , new Diagnose
                 {
                     Name = "Parkinsonismus",
                     DepartmentId = 5
                 }
                 , new Diagnose
                 {
                     Name = "Hydrocefalus",
                     DepartmentId = 5
                 }
                 , new Diagnose
                 {
                     Name = "Encefalopatie",
                     DepartmentId = 5
                 }
                 , new Diagnose
                 {
                     Name = "Mozková obrna",
                     DepartmentId = 5
                 }
                 , new Diagnose
                 {
                     Name = "Cévní mozková příhoda",
                     DepartmentId = 5
                 }
                 // Kardiologie
                 , new Diagnose
                 {
                     Name = "Hypertenze",
                     DepartmentId = 6
                 }
                 , new Diagnose
                 {
                     Name = "Angina pectoris",
                     DepartmentId = 6
                 }
                 , new Diagnose
                 {
                     Name = "Infarkt myokardu",
                     DepartmentId = 6
                 }
                 , new Diagnose
                 {
                     Name = "Ischemická choroba srdeční",
                     DepartmentId = 6
                 }
                 , new Diagnose
                 {
                     Name = "Ateroskleróza",
                     DepartmentId = 6
                 }
                 , new Diagnose
                 {
                     Name = "Srdeční selhání",
                     DepartmentId = 6
                 }

                 , new Diagnose
                 {
                     Name = "Ischemická choroba dolních končetin",
                     DepartmentId = 6
                 }
                 // Plicní oddělení
                 , new Diagnose
                 {
                     Name = "Pneumonie",
                     DepartmentId = 7
                 }
                 , new Diagnose
                 {
                     Name = "Plicní embolie",
                     DepartmentId = 7
                 }
                 , new Diagnose
                 {
                     Name = "Asthma bronchiale",
                     DepartmentId = 7
                 }
                 , new Diagnose
                 {
                     Name = "Chronická obštrukční plicní nemoc",
                     DepartmentId = 7
                 }
                 , new Diagnose
                 {
                     Name = "Respirační insuficience",
                     DepartmentId = 7
                 }
                 // Gastroenterologie
                 , new Diagnose
                 {
                     Name = "Gastritida",
                     DepartmentId = 8
                 }
                 , new Diagnose
                 {
                     Name = "Vředová choroba gastroduodena",
                     DepartmentId = 8
                 }
                 , new Diagnose
                 {
                     Name = "Pankreatitida",
                     DepartmentId = 8
                 }
                 , new Diagnose
                 {
                     Name = "Obezita",
                     DepartmentId = 8
                 }
                 , new Diagnose
                 {
                     Name = "Malnutrice",
                     DepartmentId = 8
                 }
                 , new Diagnose
                 {
                     Name = "Ulcerózní kolitida",
                     DepartmentId = 8
                 }
                 , new Diagnose
                 {
                     Name = "Crohnova nemoc",
                     DepartmentId = 8
                 }
                 , new Diagnose
                 {
                     Name = "Enteritida",
                     DepartmentId = 8
                 }
                 // Ortopedie
                 , new Diagnose
                 {
                     Name = "Artróza",
                     DepartmentId = 9
                 }
                 , new Diagnose
                 {
                     Name = "Revmatoidní artritida",
                     DepartmentId = 9
                 }
                 , new Diagnose
                 {
                     Name = "Kolagenózy",
                     DepartmentId = 9
                 }
                 , new Diagnose
                 {
                     Name = "Osteoporóza",
                     DepartmentId = 9
                 }
                 // Onkologie                
                 , new Diagnose
                 {
                     Name = "Novotvary",
                     DepartmentId = 10
                 }
                 // Nefrologie a urologie
                 , new Diagnose
                 {
                     Name = "Nefrologie a urologie",
                     DepartmentId = 11
                 }
                 , new Diagnose
                 {
                     Name = "Cystitida",
                     DepartmentId = 11
                 }
                 , new Diagnose
                 {
                     Name = "Renální selhání",
                     DepartmentId = 11
                 }
                 , new Diagnose
                 {
                     Name = "Glomerulonefritída",
                     DepartmentId = 11
                 }
                );
        }
    }
}
