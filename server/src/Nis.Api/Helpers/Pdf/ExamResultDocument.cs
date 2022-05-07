using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Drawing;
using QuestPDF.Infrastructure;
using Nis.Api.Models.Requests;

namespace Nis.Api.Helpers.Pdf;

public class ExamResultDocument : IDocument
{
    private readonly ExamResult _examResult;

    public ExamResultDocument(ExamResult examResult) => _examResult = examResult;

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
                column.Item().Text($"Student: {_examResult.Student.FirstName} {_examResult.Student.LastName}")
                    .Style(titleStyle);

                column.Item().Text(text =>
                {
                    var passedInCzechLocale = _examResult.Passed ? "ano" : "ne";
                    text.Span("Prospěl: ").SemiBold();
                    text.Span($"{passedInCzechLocale}");
                });
            });
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(collumns =>
            {
                collumns.ConstantColumn(150);
                collumns.RelativeColumn();
            });

            table.Cell().Row(1).Column(1).Element(BlockTableKeyStyle).Text("Anamnéza");
            table.Cell().Row(1).Column(2).Element(BlockTableValueStyle).Text($"{_examResult.Anamnesis}");
            table.Cell().Row(2).Column(1).Element(BlockTableKeyStyle).Text("Oddělení");
            table.Cell().Row(2).Column(2).Element(BlockTableValueStyle).Text($"{_examResult.Department}");
            table.Cell().Row(3).Column(1).Element(BlockTableKeyStyle).Text("Diagnóza");
            table.Cell().Row(3).Column(2).Element(BlockTableValueStyle).Text($"{_examResult.Diagnose}");
            table.Cell().Row(4).Column(1).Element(BlockTableKeyStyle).Text("Dieta");
            table.Cell().Row(4).Column(2).Element(BlockTableValueStyle).Text($"{_examResult.Diet}");
        });
    }

    private IContainer BlockTableKeyStyle(IContainer container)
    {
        return container
            .Border(1)
            .Background(Colors.Grey.Lighten3)
            .ShowOnce()
            .MinWidth(50)
            .MaxWidth(150)
            .MinHeight(50)
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
            .MinWidth(50)
            .MinHeight(50)
            .AlignMiddle()
            .AlignCenter();
    }
}
