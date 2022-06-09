using Nis.Api.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Drawing;
using Nis.Core.Models.Enums;
using QuestPDF.Infrastructure;
using Nis.Core.Models.MedicalScales;

namespace Nis.Api.Helpers.Pdf;

public class ExamDocument : IDocument
{
    private readonly Exam _exam;
    /// <summary>
    /// It starts with 5, because first 4 rows are always given (Anamnesis, Department, Diagnosis and Diet)
    /// And those values are always on the first page
    /// </summary>
    private uint _rowsTotal = 5; //TODO

    public ExamDocument(Exam exam) => _exam = exam;

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
    }

    private void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(24).SemiBold().FontColor(Colors.Black);

        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().ShowOnce().Text($"Student: {_exam.Student.FirstName} {_exam.Student.LastName}")
                    .Style(titleStyle);

                column.Item().ShowOnce().Text(text =>
                {
                    var passedInCzechLocale = _exam.Passed ? "ano" : "ne";
                    text.Span("Prospěl: ").SemiBold();
                    text.Span($"{passedInCzechLocale}");
                });
                column.Item().ShowOnce().Text(text =>
                {
                    var dateOfTheExam = DateTime.Now.ToShortDateString();
                    text.Span($"Test ze dne: {dateOfTheExam}").SemiBold();
                });
            });
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(col =>
        {
            col.Item().Table(table =>
            {
                table.ColumnsDefinition(collumns =>
                {
                    collumns.ConstantColumn(100);
                    collumns.RelativeColumn();
                });

                table.Cell().Row(1).Column(1).Element(BlockTableKeyStyle).Text("Anamnéza");
                table.Cell().Row(1).Column(2).Element(BlockTableValueStyle).Text($"{_exam.Anamnesis}");
                table.Cell().Row(2).Column(1).Element(BlockTableKeyStyle).Text("Oddělení");
                table.Cell().Row(2).Column(2).Element(BlockTableValueStyle).Text($"{_exam.Department}");
                table.Cell().Row(3).Column(1).Element(BlockTableKeyStyle).Text("Diagnóza");
                table.Cell().Row(3).Column(2).Element(BlockTableValueStyle).Text($"{_exam.Diagnosis}");
                table.Cell().Row(4).Column(1).Element(BlockTableKeyStyle).Text("Dieta");
                table.Cell().Row(4).Column(2).Element(BlockTableValueStyle).Text($"{_exam.Diet}");
            });

            if (!_exam.Passed)
                return;

            var medicalScales = _exam.Scales.ToList();

            ComposeTitleOfThePage(col, headerText: "Barthelův test základních všedních činností ADL");

            col.Item().Table(x =>
            {
                ComposeTable(x,
                    selectedMedicalScales: medicalScales.FindAll(scale =>
                        scale.ScaleType == ScaleType.Activity));
            });

            ComposeTitleOfThePage(col, headerText: "Posouzení rizika vzniku dekubitů");

            col.Item().Table(x =>
            {
                ComposeTable(x,
                    selectedMedicalScales: medicalScales.FindAll(scale =>
                        scale.ScaleType == ScaleType.Decubitus));
            });

            ComposeTitleOfThePage(col, headerText: "Zjištění rizika pádu pacienta");

            col.Item().Table(x =>
            {
                ComposeTable(x,
                    selectedMedicalScales: medicalScales.FindAll(scale =>
                        scale.ScaleType == ScaleType.RiskOfFall));
            });


            ComposeTitleOfThePage(col, headerText: "Hodnocení nutričního stavu");

            col.Item().Table(x =>
            {
                ComposeTable(x,
                    selectedMedicalScales: medicalScales.FindAll(scale =>
                        scale.ScaleType == ScaleType.Malnutrition));
            });
        });
    }

    private void ComposeTitleOfThePage(ColumnDescriptor columnDescriptor, string headerText)
    {
        columnDescriptor.Item().AlignCenter().Text($"{headerText}").SemiBold().FontSize(20);
        columnDescriptor.Spacing(10);
    }

    private void ComposeTable(TableDescriptor table, List<Scale> selectedMedicalScales,
        int customWidthOfTheActivityNameColumn = 150)
    {
        if (!selectedMedicalScales.Any()) return;

        table.ColumnsDefinition(collumns =>
        {
            collumns.ConstantColumn(customWidthOfTheActivityNameColumn);
            collumns.RelativeColumn();
            collumns.ConstantColumn(80);
        });

        table.Header(header =>
        {
            header.Cell().Element(CellStyle).AlignCenter().Text("Činnost");
            header.Cell().Element(CellStyle).AlignCenter().Text("Provedení činnosti");
            header.Cell().Element(CellStyle).AlignCenter().Text("bodové skóre");

            static IContainer CellStyle(IContainer container)
            {
                return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1)
                    .BorderColor(Colors.Black);
            }
        });
        foreach (var activity in selectedMedicalScales)
        {
            table.Cell().Row(_rowsTotal).Column(1).Element(BlockTableKeyStyle)
                .Text(activity.Name).SemiBold();

            var activityNames = activity.Activities
                .Aggregate("", (current, activity) => current + $"- {activity.Name}\n");

            var activityScores = activity.Activities
                .Aggregate("", (current, activity) => current + $"{activity.Score}\n");

            table.Cell().Row(_rowsTotal).Column(2).Element(BlockTableValueStyle).Text($"{activityNames}");
            table.Cell().Row(_rowsTotal).Column(3).Element(BlockTableValueStyle).Text($"{activityScores}");
            _rowsTotal++;
        }

        var total = selectedMedicalScales.SelectMany(medicalScale => medicalScale.Activities)
            .Sum(medicalScaleActivity => medicalScaleActivity.Score);

        table.Cell().Row(_rowsTotal).ColumnSpan(3).AlignRight().Text($"Součet bodů: {total}");
    }

    private IContainer BlockTableKeyStyle(IContainer container)
    {
        return container
            .Border(1)
            .Background(Colors.Grey.Lighten3)
            .ShowOnce()
            .Padding(5)
            .AlignCenter()
            .AlignMiddle();
    }

    private IContainer BlockTableValueStyle(IContainer container)
    {
        return container
            .Border(1)
            .Background(Colors.Grey.Lighten5)
            .ShowOnce()
            .PaddingHorizontal(10)
            .AlignTop();
    }
}
