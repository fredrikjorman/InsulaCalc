
using InsulaCalc.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InsulaCalc.Services;

public class ProjectPdfService
{
    public byte[] GeneratePdf(Project project)
    {
        ArgumentNullException.ThrowIfNull(project);

        var materials = project.Materials ?? new List<ProjectMaterial>();

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(35);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header().Column(column =>
                {
                    column.Item().Text("INSULACALC")
                        .Bold()
                        .FontSize(22)
                        .FontColor(Colors.Blue.Darken3);

                    column.Item().Text("Technical Insulation Report")
                        .FontSize(11)
                        .FontColor(Colors.Grey.Darken1);

                    column.Item().PaddingTop(12).LineHorizontal(1)
                        .LineColor(Colors.Grey.Lighten2);
                });

                page.Content().PaddingVertical(20).Column(column =>
                {
                    column.Spacing(16);

                    column.Item().Text(project.Name)
                        .Bold()
                        .FontSize(18);

                    column.Item().Text(
                        $"Report generated: {DateTime.Now:yyyy-MM-dd}");

                    if (!string.IsNullOrWhiteSpace(project.Description))
                    {
                        column.Item().Text(project.Description);
                    }

                    column.Item().Text("Project Summary")
                        .Bold()
                        .FontSize(14);

                    column.Item().Text($"Materials: {materials.Count}");
                    column.Item().Text(
                        $"Total length: {materials.Sum(x => x.Length):0.##} m");
                    column.Item().Text(
                        $"Packages: {materials.Sum(x => x.Packages)}");
                    column.Item().Text(
                        $"Loose sections: {materials.Sum(x => x.LooseSections)}");
                    column.Item().Text(
                        $"Rolls: {materials.Sum(x => x.Rolls)}");

                    foreach (var group in materials
                        .GroupBy(x => x.MaterialType)
                        .OrderBy(x => x.Key))
                    {
                        column.Item().PaddingTop(12)
                            .Text(group.Key)
                            .Bold()
                            .FontSize(13)
                            .FontColor(Colors.Blue.Darken3);

                        foreach (var material in group)
                        {
                            column.Item().BorderBottom(1)
                                .BorderColor(Colors.Grey.Lighten2)
                                .PaddingVertical(8)
                                .Column(item =>
                                {
                                    item.Spacing(3);

                                    item.Item().Text(material.Description)
                                        .SemiBold();

                                    item.Item().Text(
                                        $"Length: {material.Length:0.##} m");

                                    if (material.Diameter > 0)
                                        item.Item().Text(
                                            $"Diameter: {material.Diameter} mm");

                                    if (material.Thickness > 0)
                                        item.Item().Text(
                                            $"Thickness: {material.Thickness} mm");

                                    if (material.Packages > 0)
                                        item.Item().Text(
                                            $"Packages: {material.Packages}");

                                    if (material.LooseSections > 0)
                                        item.Item().Text(
                                            $"Loose sections: {material.LooseSections}");

                                    if (material.Rolls > 0)
                                        item.Item().Text(
                                            $"Rolls: {material.Rolls}");
                                });
                        }
                    }
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("InsulaCalc | Page ");
                    text.CurrentPageNumber();
                    text.Span(" of ");
                    text.TotalPages();
                });
            });
        });

        return document.GeneratePdf();
    }
}
