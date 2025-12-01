using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using Pizzeria_Toscana.Services.Interfaces;
using Pizzeria_Toscana.Models;
using iText.Layout.Borders;

namespace Pizzeria_Toscana.Services
{
    public class PdfService : IPdfService
    {
        private readonly IProdusService _produsService;
        private readonly IProdus_IngredientService _produsIngredientService;
        private readonly IIngredientService _ingredientService;
        private readonly ICategorieService _categorieService;
        public PdfService(IProdusService produsService, IProdus_IngredientService produsIngredientService, IIngredientService ingredientService, ICategorieService categorieService)
        {
            _produsService = produsService;
            _produsIngredientService = produsIngredientService;
            _ingredientService = ingredientService;
            _categorieService = categorieService;
        }

        public byte[] GenerateProductSpecificationPdf(string productId)
        {
            var produs = _produsService.GetProdusWithIngredientsByCod(productId);
            if (produs == null)
            { 
                throw new ArgumentException("Produsul nu a fost gasit.");
            }

            var ingrediente = _produsIngredientService.GetAllProdus_Ingredients()
                .Where(pi => pi.COD_Produs == productId)
                .Select(pi => new
                {
                    Denumire = _ingredientService.GetIngredientByCode(pi.COD_Ingredient)?.Denumire ?? "N/A",
                    Cantitate = !string.IsNullOrEmpty(pi.Cantitate_Ingredient) ? pi.Cantitate_Ingredient : "N/A"
                })
                .ToList();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryStream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                var titleFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                var normalFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                Color lightGreen = new DeviceRgb(144, 238, 144); 

                document.Add(new Paragraph("★ Fisa de Specificatii a Produsului ★")
                    .SetFont(titleFont)
                    .SetFontSize(28)
                    .SetFontColor(ColorConstants.BLACK)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetUnderline()
                    .SetMarginBottom(30));

                Table detailsTable = new Table(2).UseAllAvailableWidth();
                detailsTable.SetMarginBottom(20);
                detailsTable.SetBorder(new SolidBorder(ColorConstants.GREEN, 1));

                detailsTable.AddCell(CreateStyledCell("Denumire:", titleFont, true, lightGreen));
                detailsTable.AddCell(CreateStyledCell(produs.Denumire, normalFont, false, ColorConstants.WHITE));

                detailsTable.AddCell(CreateStyledCell("Descriere:", titleFont, true, lightGreen));
                detailsTable.AddCell(CreateStyledCell(produs.Descriere ?? "N/A", normalFont, false, ColorConstants.WHITE));

                detailsTable.AddCell(CreateStyledCell("Pret:", titleFont, true, lightGreen));
                detailsTable.AddCell(CreateStyledCell($"{produs.Pret} RON", normalFont, false, ColorConstants.WHITE));

                var category = _categorieService.GetCategorieById(produs.ID_Categorie);
                detailsTable.AddCell(CreateStyledCell("Categorie:", titleFont, true, lightGreen));
                detailsTable.AddCell(CreateStyledCell(category.Nume ?? "N/A", normalFont, false, ColorConstants.WHITE));

                detailsTable.AddCell(CreateStyledCell("Greutate aproximativa:", titleFont, true, lightGreen));
                detailsTable.AddCell(CreateStyledCell("500g", normalFont, false, ColorConstants.WHITE));

                detailsTable.AddCell(CreateStyledCell("Valoare energetica:", titleFont, true, lightGreen));
                detailsTable.AddCell(CreateStyledCell("1200 kcal", normalFont, false, ColorConstants.WHITE));

                document.Add(detailsTable);
                document.Add(new Paragraph("Ingrediente")
                    .SetFont(titleFont)
                    .SetFontSize(18)
                    .SetMarginTop(20)
                    .SetFontColor(ColorConstants.BLACK)); 

                if (ingrediente.Any())
                {
                    Table ingredientTable = new Table(2).UseAllAvailableWidth();
                    ingredientTable.SetMarginBottom(20);
                    ingredientTable.SetBorder(new SolidBorder(ColorConstants.GREEN, 1));

                    ingredientTable.AddHeaderCell(CreateStyledCell("Ingredient", titleFont, true, lightGreen));
                    ingredientTable.AddHeaderCell(CreateStyledCell("Cantitate", titleFont, true, lightGreen));

                    foreach (var ing in ingrediente)
                    {
                        ingredientTable.AddCell(CreateStyledCell(ing.Denumire, normalFont, false, ColorConstants.WHITE));
                        ingredientTable.AddCell(CreateStyledCell(ing.Cantitate, normalFont, false, ColorConstants.WHITE));
                    }

                    document.Add(ingredientTable);
                }
                else
                {
                    document.Add(new Paragraph("Nu exista ingrediente pentru acest produs.")
                        .SetFont(normalFont)
                        .SetFontSize(12));
                }

                document.Add(new Paragraph("Informatii aditionale:")
                    .SetFont(titleFont)
                    .SetFontSize(18)
                    .SetMarginTop(20)
                    .SetFontColor(ColorConstants.BLACK)); 

                document.Add(new Paragraph("Acest preparat este facut din ingrediente proaspete si de cea mai buna calitate. Se recomanda consumul in termen de 24 de ore de la preparare pentru o experienta gustativa optima.")
                    .SetFont(normalFont)
                    .SetFontSize(12)
                    .SetMarginBottom(10));

                document.Add(new Paragraph("Pentru o experienta de neuitat, incercati sa reincalziti produsul la 180°C timp de 5-7 minute in cuptor, evitand utilizarea cuptorului cu microunde pentru a pastra textura produsului.")
                    .SetFont(normalFont)
                    .SetFontSize(12));

                document.Close();
                byte[] pdfBytes = memoryStream.ToArray();
                return pdfBytes;
            }
        }

        private Cell CreateStyledCell(string content, PdfFont font, bool isHeader = false, Color backgroundColor = null)
        {
            Cell cell = new Cell()
                .Add(new Paragraph(content)
                .SetFont(font)
                .SetFontSize(12)
                .SetFontColor(ColorConstants.BLACK)); 

            if (isHeader)
            {
                cell.SetBackgroundColor(backgroundColor ?? ColorConstants.LIGHT_GRAY);
            }
            else
            {
                cell.SetBackgroundColor(backgroundColor ?? ColorConstants.WHITE);
            }

            return cell;
        }


    }
}