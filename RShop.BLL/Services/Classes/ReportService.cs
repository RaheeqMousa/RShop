using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.Repositories.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel;

namespace RShop.BLL.Services.Classes
{
    public class ReportService
    {
        private readonly IProductRepository _productRepository;
        public ReportService(IProductRepository productRepository) {
            _productRepository= productRepository;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] GenerateProductReports()
        {
            var products = _productRepository.getAllProductsWithImage();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text("RShop - Products!")
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);

                            foreach (var item in products)
                            {
                                x.Item().Text($"Id: {item.Id}, Name: {item.Name}");
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            });

            return document.GeneratePdf();
        }

    }
}
