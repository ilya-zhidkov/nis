using Nis.Core.Models.Enums;
using Nis.Core.Models.MedicalScales;

namespace Nis.Core.Persistence.Seeders;

public class MedicalScalesSeeder : BaseSeeder
{
    public override void Seed(DataContext context, IServiceProvider services = null)
    {
        if (context.Scales.Any())
            return;

        #region Risk of fall seeders

        context.Scales.AddRange(
            new Scale
            {
                Name = "Pohyb",
                ScaleType = ScaleType.RiskOfFall,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "Neomezený", Score = 0 },
                    new() { Name = "Použivání pomůcek", Score = 1 },
                    new() { Name = "Potřebuje pomoc k pohybu", Score = 1 },
                    new() { Name = "Neschopen přesunu", Score = 1 }
                }
            },
            new Scale
            {
                Name = "Vyprazdňování",
                ScaleType = ScaleType.RiskOfFall,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "Nevyžaduje pomoc", Score = 0 },
                    new() { Name = "Historie nokturie/inkotinence", Score = 1 },
                    new() { Name = "Vyžaduje pomoc", Score = 1 }
                }
            },
            new Scale
            {
                Name = "Medikace",
                ScaleType = ScaleType.RiskOfFall,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "Neužívá rizikové léky", Score = 0 },
                    new()
                    {
                        Name =
                            "Užívá léky ze skupiny: \n - diuretik, \n - antikonvulziv,\n - antiparkinsonik,\n - antihypertenziv,\n - psychotropní léky nebo benzodiazepiny",
                        Score = 1
                    }
                }
            },
            new Scale
            {
                Name = "Smyslové poruchy",
                ScaleType = ScaleType.RiskOfFall,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "Žádné", Score = 0 },
                    new() { Name = "Vizuální, sluchové, smyslový deficit", Score = 1 }
                }
            },
            new Scale
            {
                Name = "Mentální stav",
                ScaleType = ScaleType.RiskOfFall,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "Orientován", Score = 0 },
                    new() { Name = "Občasná/noční dezorientace", Score = 1 },
                    new() { Name = "Historie dezorientace/demence", Score = 1 }
                }
            },
            new Scale
            {
                Name = "Věk",
                ScaleType = ScaleType.RiskOfFall,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "18-75", Score = 0 },
                    new() { Name = "75 a výše", Score = 1 }
                }
            },

            #endregion

            #region Activity daily living (Activity)

            new Scale
            {
                Name = "Najedení, napití",
                ScaleType = ScaleType.Activity,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "Samostatně bez pomoci", Score = 10 },
                    new() { Name = "s pomocí", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },
            new Scale
            {
                Name = "Oblékání",
                ScaleType = ScaleType.Activity,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "Samostatně bez pomoci", Score = 10 },
                    new() { Name = "s pomocí", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },
            new Scale
            {
                Name = "Koupání",
                ScaleType = ScaleType.Activity,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "Samostatně bez pomoci", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },
            new Scale
            {
                Name = "Osobní hygiena",
                ScaleType = ScaleType.Activity,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "Samostatně bez pomoci", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },
            new Scale
            {
                Name = "Kontinence moči",
                ScaleType = ScaleType.Activity,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "plně kontinentní", Score = 10 },
                    new() { Name = "občas inkontinentní", Score = 5 },
                    new() { Name = "trvale inkontinentní", Score = 0 },
                }
            },
            new Scale
            {
                Name = "kontinence stolice",
                ScaleType = ScaleType.Activity,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "plně kontinentní", Score = 10 },
                    new() { Name = "občas inkontinentní", Score = 5 },
                    new() { Name = "trvale inkontinentní", Score = 0 },
                }
            },
            new Scale
            {
                Name = "Použití WC",
                ScaleType = ScaleType.Activity,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "samostatně bez pomoci", Score = 10 },
                    new() { Name = "s pomocí", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },
            new Scale
            {
                Name = "Přesun lůžko – židle",
                ScaleType = ScaleType.Activity,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "samostatně bez pomoci", Score = 15 },
                    new() { Name = "s malou pomocí", Score = 10 },
                    new() { Name = "vydrží sedět", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },
            new Scale
            {
                Name = "Chůze po rovině",
                ScaleType = ScaleType.Activity,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "samostatně nad 50 m", Score = 15 },
                    new() { Name = "s pomocí 50 m", Score = 10 },
                    new() { Name = "na vozíku 50 m", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },
            new Scale
            {
                Name = "Chůze po schodech",
                ScaleType = ScaleType.Activity,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "samostatně bez pomoci", Score = 10 },
                    new() { Name = "s pomocí", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },

            #endregion

            #region Nutritional status assessment Screening

            new Scale
            {
                Name = "Jíte méně v posledních 3 měsících?",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "ano, výrazně méně", Score = 0 },
                    new() { Name = "ano, trochu méně", Score = 1 },
                    new() { Name = "ne, jím pořád stejně", Score = 2 },
                }
            },
            new Scale
            {
                Name = "Zhubnul jste v posledních měsících? O kolik kilogramů?",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "více než o 3 kg", Score = 0 },
                    new() { Name = "nevím", Score = 1 },
                    new() { Name = "úbytek mezi 1-3 kg", Score = 2 },
                    new() { Name = "žádný úbytek na váze", Score = 3 }
                }
            },
            new Scale
            {
                Name = "Stav hybnosti",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "upoutaný na lůžko nebo invalidní vozík", Score = 0 },
                    new() { Name = "schopen vstát, ale většinu dne tráví na lůžku či vozíku", Score = 1 },
                    new() { Name = "samostatně se pohybuje", Score = 2 },
                }
            },
            new Scale
            {
                Name = "Prodělal jste v posledních 3 měsících nějaké akutní onemocnění nebo výrazný stres?",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "ano", Score = 0 },
                    new() { Name = "ne", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Neuropsychologický stav pacienta",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "deprese nebo těžká demence", Score = 0 },
                    new()
                    {
                        Name =
                            @"mírná a střední demence (pacient je schopen komunikovat, může být dezorientovaný, ale není agresivní či neklidný, v noci převážně spí)",
                        Score = 1
                    },
                    new() { Name = "bez těchto problémů", Score = 2 },
                }
            },
            new Scale
            {
                Name = "BMI – Body Mass Index",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "BMI méně než 19", Score = 0 },
                    new() { Name = "BMI 19 až méně než 21", Score = 1 },
                    new() { Name = "BMI 21 až méně než 23", Score = 2 },
                    new() { Name = "BMI 23 či vyšší", Score = 3 },
                }
            },

            #endregion

            #region Nutritional status assessment Screening Addination Examination

            new Scale
            {
                Name = "Žije samostatně v domácím prostředí (není nikde dlouhodobě umístěn či hospitalizován)",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "ne", Score = 0 },
                    new() { Name = "ano", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Užívá více než tři druhy léků denně (dlouhodobá medikace)",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "ano", Score = 0 },
                    new() { Name = "ne", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Dekubity či jiné výrazné kožní defekty",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "ano", Score = 0 },
                    new() { Name = "ne", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Kolik plnohodnotných jídel sní pacient za den?",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "jedno", Score = 0 },
                    new() { Name = "dvě", Score = 1 },
                    new() { Name = "tři", Score = 2 },
                }
            },
            new Scale
            {
                Name = "Zhodnoťte následující indikátory příjmu proteinů \n alespoň jedenkrát denně mléčný pokrm (mléko, sýr jogurt) ano – ne \n alespoň dvakrát v týdnu vejce nebo luštěniny ano - ne \n maso, ryba nebo drůbež každý den ano – ne",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "do jedné pozitivní odpovědi", Score = 0 },
                    new() { Name = "při dvou pozitivních odpovědích", Score = 0.5f },
                    new() { Name = "při třech pozitivních odpovědích", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Jí pacient alespoň dvě porce čerstvé zeleniny nebo ovoce za týden?",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "ne", Score = 0 },
                    new() { Name = "ano", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Kolik tekutin pacient vypije? (voda, džus, káva, čaj, mléko...)",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "méně než tři šálky", Score = 0 },
                    new() { Name = "tři až pět šálků", Score = 0.5f },
                    new() { Name = "více než 5 šálků", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Jak pacient jí:",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "musí být krmen, sám se nenají", Score = 0 },
                    new() { Name = "jí sám, ale s potížemi", Score = 1 },
                    new() { Name = "bez problémů sám", Score = 2 },
                }
            },
            new Scale
            {
                Name = "Jak sám posuzuje svůj nutriční stav",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "domnívá se, že je podvyživený", Score = 0 },
                    new() { Name = "neví", Score = 1 },
                    new() { Name = "domnívá se, že podvyživený není a potíže s výživou nemá", Score = 2 },
                }
            },
            new Scale
            {
                Name = "Jak posuzuje pacient svůj zdravotní stav, když jej srovnává s většinou lidí svého věku?",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "horší než většina vrstevníků", Score = 0 },
                    new() { Name = "neví", Score = 0.5f },
                    new() { Name = "asi tak stejný jako většina vrstevníků", Score = 1 },
                    new() { Name = "lepší", Score = 2 },
                }
            },
            new Scale
            {
                Name = "Střední obvod paže v centimetrech",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "méně než 21 cm", Score = 0 },
                    new() { Name = "21-22 cm", Score = 0.5f },
                    new() { Name = "22 cm a více", Score = 1 },
                }
            },
            new Scale
            {
                Name = "obvod lýtka",
                ScaleType = ScaleType.Malnutrition,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "méně než 31 cm", Score = 0 },
                    new() { Name = "31 cm a více", Score = 1 }
                }
            },

            #endregion

            #region Risk Of Decubitus

            new Scale
            {
                Name = "Schopnost spolupráce",
                ScaleType = ScaleType.Decubitus,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "úplná", Score = 4 },
                    new() { Name = "malá", Score = 3 },
                    new() { Name = "částečná", Score = 2 },
                    new() { Name = "žadná", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Věk",
                ScaleType = ScaleType.Decubitus,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "< 10", Score = 4 },
                    new() { Name = "< 30", Score = 3 },
                    new() { Name = "< 60", Score = 2 },
                    new() { Name = "> 60", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Stav pokožky",
                ScaleType = ScaleType.Decubitus,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "normální", Score = 4 },
                    new() { Name = "alergie", Score = 3 },
                    new() { Name = "vlhká", Score = 2 },
                    new() { Name = "suchá", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Každé další onemocnění",
                ScaleType = ScaleType.Decubitus,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "žádné", Score = 4 },
                    new() { Name = "DM, anemie", Score = 3 },
                    new() { Name = "kachexie,ucpávání tepen", Score = 2 },
                    new() { Name = "obezita,karcinom", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Fyzický stav",
                ScaleType = ScaleType.Decubitus,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "dobrý", Score = 4 },
                    new() { Name = "zhoršený", Score = 3 },
                    new() { Name = "špatný", Score = 2 },
                    new() { Name = "velmi špatný", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Stav vědomí",
                ScaleType = ScaleType.Decubitus,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "dobrý", Score = 4 },
                    new() { Name = "apatický", Score = 3 },
                    new() { Name = "zmatený", Score = 2 },
                    new() { Name = "bezvědomí", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Aktivita",
                ScaleType = ScaleType.Decubitus,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "chodí", Score = 4 },
                    new() { Name = "doprovod", Score = 3 },
                    new() { Name = "sedačka", Score = 2 },
                    new() { Name = "leží", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Pohyblivost",
                ScaleType = ScaleType.Decubitus,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "úplná", Score = 4 },
                    new() { Name = "částečně omezená", Score = 3 },
                    new() { Name = "velmi omezená", Score = 2 },
                    new() { Name = "žádná", Score = 1 },
                }
            },
            new Scale
            {
                Name = "Inkontinence",
                ScaleType = ScaleType.Decubitus,
                Activities = new List<ScaleActivity>
                {
                    new() { Name = "není", Score = 4 },
                    new() { Name = "občas", Score = 3 },
                    new() { Name = "převážně moč", Score = 2 },
                    new() { Name = "moč + stolice", Score = 1 },
                }
            }

            #endregion

        );
    }
}
