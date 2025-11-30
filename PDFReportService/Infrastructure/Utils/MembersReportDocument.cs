using Entitites;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utils
{
    public class MembersReportDocument : IDocument
    {
        private readonly List<MemberDto> _members;

        public MembersReportDocument(List<MemberDto> members)
        {
            _members = members;
        }


        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));
                    page.Header()
                        .Text("Üyeler Raporu")
                        .SemiBold().FontSize(18).FontColor(Colors.Blue.Darken2);
                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(10);
                            column.Item().Text("Tüm Üyeler").SemiBold().FontSize(14);
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(20);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                table.Header(header =>
                                {
                                    header.Cell().Border(1).Background(Colors.Grey.Lighten3).Padding(5).Text("ID").SemiBold();
                                    header.Cell().Border(1).Background(Colors.Grey.Lighten3).Padding(5).Text("Adı").SemiBold();
                                    header.Cell().Border(1).Background(Colors.Grey.Lighten3).Padding(5).Text("E-posta").SemiBold();
                                    header.Cell().Border(1).Background(Colors.Grey.Lighten3).Padding(5).Text("Telefon").SemiBold();
                                });
                                foreach (var member in _members)
                                {
                                    table.Cell().Border(1).Padding(5).Text(member.MemberId.ToString());
                                    table.Cell().Border(1).Padding(5).Text(member.FullName);
                                    table.Cell().Border(1).Padding(5).Text(member.Email);
                                    table.Cell().Border(1).Padding(5).Text(member.Phone);
                                }
                            });
                        });
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Sayfa ");
                            x.CurrentPageNumber();
                            x.Span(" / ");
                            x.TotalPages();
                        });
                });
        }
    }
}
