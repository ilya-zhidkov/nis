using Nis.Core.Models.Enums;
using Nis.Core.Models.MedicalScales;

namespace Nis.Core.Persistence.Seeders;

public class MedicalScalesSeeder : BaseSeeder
{
    public override void Seed(DataContext context, IServiceProvider services = null)
    {
        if (context.MedicalScales.Any())
            return;

        #region Risk of fall seeders

        context.MedicalScales.AddRange(
            new MedicalScale
            {
                Name = "Pohyb",
                ScaleCategory = MedicalScaleCategory.RiskOfFall,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "Neomezený", Score = 0 },
                    new() { Name = "Použivání pomůcek", Score = 1 },
                    new() { Name = "Potřebuje pomoc k pohybu", Score = 1 },
                    new() { Name = "Neschopen přesunu", Score = 1 }
                }
            },
            new MedicalScale
            {
                Name = "Vyprazdňování",
                ScaleCategory = MedicalScaleCategory.RiskOfFall,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "Nevyžaduje pomoc", Score = 0 },
                    new() { Name = "Historie nokturie/inkotinence", Score = 1 },
                    new() { Name = "Vyžaduje pomoc", Score = 1 }
                }
            },
            new MedicalScale
            {
                Name = "Medikace",
                ScaleCategory = MedicalScaleCategory.RiskOfFall,
                Activities = new List<MedicalScaleActivity>
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
            new MedicalScale
            {
                Name = "Smyslové poruchy",
                ScaleCategory = MedicalScaleCategory.RiskOfFall,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "Žádné", Score = 0 },
                    new() { Name = "Vizuální, sluchové, smyslový deficit", Score = 1 }
                }
            },
            new MedicalScale
            {
                Name = "Mentální stav",
                ScaleCategory = MedicalScaleCategory.RiskOfFall,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "Orientován", Score = 0 },
                    new() { Name = "Občasná/noční dezorientace", Score = 1 },
                    new() { Name = "Historie dezorientace/demence", Score = 1 }
                }
            },
            new MedicalScale
            {
                Name = "Věk",
                ScaleCategory = MedicalScaleCategory.RiskOfFall,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "18-75", Score = 0 },
                    new() { Name = "75 a výše", Score = 1 }
                }
            },

            #endregion

            #region Activity daily living (ADL)

            new MedicalScale
            {
                Name = "Najedení, napití",
                ScaleCategory = MedicalScaleCategory.ADL,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "Samostatně bez pomoci", Score = 10 },
                    new() { Name = "s pomocí", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },
            new MedicalScale
            {
                Name = "Oblékání",
                ScaleCategory = MedicalScaleCategory.ADL,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "Samostatně bez pomoci", Score = 10 },
                    new() { Name = "s pomocí", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },
            new MedicalScale
            {
                Name = "Koupání",
                ScaleCategory = MedicalScaleCategory.ADL,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "Samostatně bez pomoci", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },
            new MedicalScale
            {
                Name = "Osobní hygiena",
                ScaleCategory = MedicalScaleCategory.ADL,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "Samostatně bez pomoci", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },
            new MedicalScale
            {
                Name = "Kontinence moči",
                ScaleCategory = MedicalScaleCategory.ADL,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "plně kontinentní", Score = 10 },
                    new() { Name = "občas inkontinentní", Score = 5 },
                    new() { Name = "trvale inkontinentní", Score = 0 },
                }
            },
            new MedicalScale
            {
                Name = "kontinence stolice",
                ScaleCategory = MedicalScaleCategory.ADL,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "plně kontinentní", Score = 10 },
                    new() { Name = "občas inkontinentní", Score = 5 },
                    new() { Name = "trvale inkontinentní", Score = 0 },
                }
            },
            new MedicalScale
            {
                Name = "Použití WC",
                ScaleCategory = MedicalScaleCategory.ADL,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "samostatně bez pomoci", Score = 10 },
                    new() { Name = "s pomocí", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },
            new MedicalScale
            {
                Name = "Přesun lůžko – židle",
                ScaleCategory = MedicalScaleCategory.ADL,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "samostatně bez pomoci", Score = 15 },
                    new() { Name = "s malou pomocí", Score = 10 },
                    new() { Name = "vydrží sedět", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },
            new MedicalScale
            {
                Name = "Chůze po rovině",
                ScaleCategory = MedicalScaleCategory.ADL,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "samostatně nad 50 m", Score = 15 },
                    new() { Name = "s pomocí 50 m", Score = 10 },
                    new() { Name = "na vozíku 50 m", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },
            new MedicalScale
            {
                Name = "Chůze po schodech",
                ScaleCategory = MedicalScaleCategory.ADL,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "samostatně bez pomoci", Score = 10 },
                    new() { Name = "s pomocí", Score = 5 },
                    new() { Name = "neprovede", Score = 0 },
                }
            },

            #endregion

            #region Nutritional status assessment Screening

            new MedicalScale
            {
                Name = "Jíte méně v posledních 3 měsících?",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentScreening,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "ano, výrazně méně", Score = 0 },
                    new() { Name = "ano, trochu méně", Score = 1 },
                    new() { Name = "ne, jím pořád stejně", Score = 2 },
                }
            },
            new MedicalScale
            {
                Name = "Zhubnul jste v posledních měsících? O kolik kilogramů?",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentScreening,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "více než o 3 kg", Score = 0 },
                    new() { Name = "nevím", Score = 1 },
                    new() { Name = "úbytek mezi 1-3 kg", Score = 2 },
                    new() { Name = "žádný úbytek na váze", Score = 3 }
                }
            },
            new MedicalScale
            {
                Name = "Stav hybnosti",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentScreening,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "upoutaný na lůžko nebo invalidní vozík", Score = 0 },
                    new() { Name = "schopen vstát, ale většinu dne tráví na lůžku či vozíku", Score = 1 },
                    new() { Name = "samostatně se pohybuje", Score = 2 },
                }
            },
            new MedicalScale
            {
                Name = "Prodělal jste v posledních 3 měsících nějaké akutní onemocnění nebo výrazný stres?",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentScreening,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "ano", Score = 0 },
                    new() { Name = "ne", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Neuropsychologický stav pacienta",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentScreening,
                Activities = new List<MedicalScaleActivity>
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
            new MedicalScale
            {
                Name = "BMI – Body Mass Index",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentScreening,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "BMI méně než 19", Score = 0 },
                    new() { Name = "BMI 19 až méně než 21", Score = 1 },
                    new() { Name = "BMI 21 až méně než 23", Score = 2 },
                    new() { Name = "BMI 23 či vyšší", Score = 3 },
                }
            },

            #endregion

            #region Nutritional status assessment Screening Addination Examination

            new MedicalScale
            {
                Name = "Žije samostatně v domácím prostředí (není nikde dlouhodobě umístěn či hospitalizován)",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentAdditionalExamination,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "ne", Score = 0 },
                    new() { Name = "ano", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Užívá více než tři druhy léků denně (dlouhodobá medikace)",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentAdditionalExamination,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "ano", Score = 0 },
                    new() { Name = "ne", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Dekubity či jiné výrazné kožní defekty",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentAdditionalExamination,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "ano", Score = 0 },
                    new() { Name = "ne", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Kolik plnohodnotných jídel sní pacient za den?",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentAdditionalExamination,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "jedno", Score = 0 },
                    new() { Name = "dvě", Score = 1 },
                    new() { Name = "tři", Score = 2 },
                }
            },
            new MedicalScale
            {
                Name = "Zhodnoťte následující indikátory příjmu proteinů \n alespoň jedenkrát denně mléčný pokrm (mléko, sýr jogurt) ano – ne \n alespoň dvakrát v týdnu vejce nebo luštěniny ano - ne \n maso, ryba nebo drůbež každý den ano – ne",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentAdditionalExamination,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "do jedné pozitivní odpovědi", Score = 0 },
                    new() { Name = "při dvou pozitivních odpovědích", Score = 0.5f },
                    new() { Name = "při třech pozitivních odpovědích", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Jí pacient alespoň dvě porce čerstvé zeleniny nebo ovoce za týden?",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentAdditionalExamination,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "ne", Score = 0 },
                    new() { Name = "ano", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Kolik tekutin pacient vypije? (voda, džus, káva, čaj, mléko...)",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentAdditionalExamination,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "méně než tři šálky", Score = 0 },
                    new() { Name = "tři až pět šálků", Score = 0.5f },
                    new() { Name = "více než 5 šálků", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Jak pacient jí:",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentAdditionalExamination,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "musí být krmen, sám se nenají", Score = 0 },
                    new() { Name = "jí sám, ale s potížemi", Score = 1 },
                    new() { Name = "bez problémů sám", Score = 2 },
                }
            },
            new MedicalScale
            {
                Name = "Jak sám posuzuje svůj nutriční stav",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentAdditionalExamination,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "domnívá se, že je podvyživený", Score = 0 },
                    new() { Name = "neví", Score = 1 },
                    new() { Name = "domnívá se, že podvyživený není a potíže s výživou nemá", Score = 2 },
                }
            },
            new MedicalScale
            {
                Name = "Jak posuzuje pacient svůj zdravotní stav, když jej srovnává s většinou lidí svého věku?",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentAdditionalExamination,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "horší než většina vrstevníků", Score = 0 },
                    new() { Name = "neví", Score = 0.5f },
                    new() { Name = "asi tak stejný jako většina vrstevníků", Score = 1 },
                    new() { Name = "lepší", Score = 2 },
                }
            },
            new MedicalScale
            {
                Name = "Střední obvod paže v centimetrech",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentAdditionalExamination,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "méně než 21 cm", Score = 0 },
                    new() { Name = "21-22 cm", Score = 0.5f },
                    new() { Name = "22 cm a více", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "obvod lýtka",
                ScaleCategory = MedicalScaleCategory.NutritionalStatusAssessmentAdditionalExamination,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "méně než 31 cm", Score = 0 },
                    new() { Name = "31 cm a více", Score = 1 }
                }
            },

            #endregion

            #region Risk Of Decubitus

            new MedicalScale
            {
                Name = "Schopnost spolupráce",
                ScaleCategory = MedicalScaleCategory.RiskOfDecubitus,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "úplná", Score = 4 },
                    new() { Name = "malá", Score = 3 },
                    new() { Name = "částečná", Score = 2 },
                    new() { Name = "žadná", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Věk",
                ScaleCategory = MedicalScaleCategory.RiskOfDecubitus,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "< 10", Score = 4 },
                    new() { Name = "< 30", Score = 3 },
                    new() { Name = "< 60", Score = 2 },
                    new() { Name = "> 60", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Stav pokožky",
                ScaleCategory = MedicalScaleCategory.RiskOfDecubitus,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "normální", Score = 4 },
                    new() { Name = "alergie", Score = 3 },
                    new() { Name = "vlhká", Score = 2 },
                    new() { Name = "suchá", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Každé další onemocnění",
                ScaleCategory = MedicalScaleCategory.RiskOfDecubitus,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "žádné", Score = 4 },
                    new() { Name = "DM, anemie", Score = 3 },
                    new() { Name = "kachexie,ucpávání tepen", Score = 2 },
                    new() { Name = "obezita,karcinom", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Fyzický stav",
                ScaleCategory = MedicalScaleCategory.RiskOfDecubitus,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "dobrý", Score = 4 },
                    new() { Name = "zhoršený", Score = 3 },
                    new() { Name = "špatný", Score = 2 },
                    new() { Name = "velmi špatný", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Stav vědomí",
                ScaleCategory = MedicalScaleCategory.RiskOfDecubitus,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "dobrý", Score = 4 },
                    new() { Name = "apatický", Score = 3 },
                    new() { Name = "zmatený", Score = 2 },
                    new() { Name = "bezvědomí", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Aktivita",
                ScaleCategory = MedicalScaleCategory.RiskOfDecubitus,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "chodí", Score = 4 },
                    new() { Name = "doprovod", Score = 3 },
                    new() { Name = "sedačka", Score = 2 },
                    new() { Name = "leží", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Pohyblivost",
                ScaleCategory = MedicalScaleCategory.RiskOfDecubitus,
                Activities = new List<MedicalScaleActivity>
                {
                    new() { Name = "úplná", Score = 4 },
                    new() { Name = "částečně omezená", Score = 3 },
                    new() { Name = "velmi omezená", Score = 2 },
                    new() { Name = "žádná", Score = 1 },
                }
            },
            new MedicalScale
            {
                Name = "Inkontinence",
                ScaleCategory = MedicalScaleCategory.RiskOfDecubitus,
                Activities = new List<MedicalScaleActivity>
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