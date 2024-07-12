using System.Diagnostics;
using CIAbmc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DocumentFormat.OpenXml.Spreadsheet;
using DocXToPdfConverter;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DocXToPdfConverter.DocXToPdfHandlers;
using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using Aspose.Words;

namespace CIAbmc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        { 
            var doc = new Document(@"C:\Users\piotr\source\repos\SuperSaryk\CIAbmc\CIAbmc\wwwroot\html\PDF.pdf");
            doc.Save(@"C:\Users\piotr\source\repos\SuperSaryk\CIAbmc\CIAbmc\wwwroot\html\Output.html");

            return View();
        }

        public ActionResult PDFex()
        {
            var filePath = @"C:\Users\piotr\Source\Repos\SuperSaryk\CIAbmc\CIAbmc\wwwroot\html\PDF.html";
            StreamReader reader = new StreamReader(filePath);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            FileContentResult file = File(fileBytes, "text/html");
            return file;
            //var fileExists = System.IO.File.Exists(filePath);
        }

        [HttpPost]
        public FileResult ConvertHtml(Szablon model)
        {
            string executableLocation = @"C:\Users\piotr\source\repos\SuperSaryk\CIAbmc\CIAbmc\";
            string pdfPath = executableLocation + @"\wwwroot\html\PDF.pdf";
            string pdfName = "PDF.pdf";
            string indexPath = executableLocation + @"\wwwroot\html\PDFTemplate.html";
            string locationOfLibreOfficeSoffice = @"C:\Users\piotr\OneDrive\Pulpit\C#\New folder\LibreOfficePortable\App\libreoffice\program\soffice.exe";
            string imgPath = executableLocation + @"\\wwwroot\\img\\outputpdf.png";

            var placeholders = new Placeholders();
            placeholders.NewLineTag = "<br/>";
            placeholders.TextPlaceholderStartTag = "##";
            placeholders.TextPlaceholderEndTag = "##";
            placeholders.TablePlaceholderStartTag = "==";
            placeholders.TablePlaceholderEndTag = "==";
            placeholders.ImagePlaceholderStartTag = "++";
            placeholders.ImagePlaceholderEndTag = "++";

            var value1 = model.Opracowane_dla;
            var value2 = model.Opracowane_przez;
            string value3 = model.Data.ToString();
            string value4 = model.Wersja.ToString();
            var value5 = model.Kluczowi_partnerzy;
            var value6 = model.Kluczowe_aktywności;
            var value7 = model.Propozycja_wartości;
            var value8 = model.Relacja_z_klientami;
            var value9 = model.Segmenty_klientów;
            var value10 = model.Kluczowe_zasoby;
            var value11 = model.Kanaly_dotarcia;
            var value12 = model.Struktura_kosztow;
            var value13 = model.Strumienie_przychodów;

            placeholders.TextPlaceholders = new Dictionary<string, string>
            {
                {"opracowane_dla", value1},
                {"opracowane_przez", value2},
                {"data", value3},
                {"wersja", value4},
                {"kluczowi_partnerzy", value5},
                {"kluczowe_aktywności", value6},
                {"propozycja_wartości", value7},
                {"relacja_z_klientami", value8},
                {"segmenty_klientów", value9},
                {"kluczowe_zasoby", value10},
                {"kanaly_dotarcia", value11},
                {"struktura_kosztow", value12},
                {"strumienie_przychodów", value13},

            };

            var qrImage = StreamHandler.GetFileAsMemoryStream(Path.Combine(executableLocation, imgPath));
            var qrImageElement = new ImageElement() { Dpi = 300, memStream = qrImage };

            placeholders.ImagePlaceholders = new Dictionary<string, ImageElement>
            {
                {"QRCode", qrImageElement },
            };


            var test = new ReportGenerator(locationOfLibreOfficeSoffice);
            test.Convert(indexPath, pdfPath, placeholders);

            var fs = System.IO.File.OpenRead(indexPath);
            return PhysicalFile(indexPath, "application/pdf", pdfName);
        }

        public IActionResult PDF()
        {
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
