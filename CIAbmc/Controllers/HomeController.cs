using System.Diagnostics;
using CIAbmc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DocXToPdfConverter;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DocXToPdfConverter.DocXToPdfHandlers;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.IO;
using System.Xml;
using OpenXmlPowerTools;
using Newtonsoft.Json;
using HtmlAgilityPack;
using DocumentFormat.OpenXml.Spreadsheet;

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
            return View();
        }

        public ActionResult PDFex()
        {
            string workingDirectory = Environment.CurrentDirectory;

            var filePath = workingDirectory + @"\wwwroot\html\PDF.html";
            StreamReader reader = new StreamReader(filePath);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            FileContentResult file = File(fileBytes, "text/html");
            return file;
            //var fileExists = System.IO.File.Exists(filePath);
        }

        [HttpPost]
        public IActionResult ConvertHtmlToPDF(Szablon model, string action)
        {
            string executableLocation = Environment.CurrentDirectory;
            string pdfPath = executableLocation + @"\wwwroot\html\PDF.pdf";
            string pdfName = "PDF.pdf";
            string indexPath = executableLocation + @"\wwwroot\html\PDFTemplate.html";
            string locationOfLibreOfficeSoffice = @"C:\Users\piotr\OneDrive\Pulpit\C#\New folder\LibreOfficePortable\App\libreoffice\program\soffice.exe";
            string imgPath = executableLocation + @"\\wwwroot\\img\\outputpdf.png";

            if (action == "Download")
            {
                var placeholders = new Placeholders
                {
                    NewLineTag = "<br/>",
                    TextPlaceholderStartTag = "##",
                    TextPlaceholderEndTag = "##",
                    TablePlaceholderStartTag = "==",
                    TablePlaceholderEndTag = "==",
                    ImagePlaceholderStartTag = "++",
                    ImagePlaceholderEndTag = "++",

                    TextPlaceholders = new Dictionary<string, string>
                    {
                        {"opracowane_dla", model.Opracowane_dla},
                        {"opracowane_przez", model.Opracowane_przez},
                        {"data", model.Data.ToString()},
                        {"wersja", model.Wersja.ToString()},
                        {"kluczowi_partnerzy", model.Kluczowi_partnerzy},
                        {"kluczowe_aktywności", model.Kluczowe_aktywności},
                        {"propozycja_wartości", model.Propozycja_wartości},
                        {"relacja_z_klientami", model.Relacja_z_klientami},
                        {"segmenty_klientów", model.Segmenty_klientów},
                        {"kluczowe_zasoby", model.Kluczowe_zasoby},
                        {"kanaly_dotarcia", model.Kanaly_dotarcia},
                        {"struktura_kosztow", model.Struktura_kosztow},
                        {"strumienie_przychodów", model.Strumienie_przychodów}
                    }
                };

                var qrImage = StreamHandler.GetFileAsMemoryStream(Path.Combine(executableLocation, imgPath));
                var qrImageElement = new ImageElement { Dpi = 300, memStream = qrImage };

                placeholders.ImagePlaceholders = new Dictionary<string, ImageElement>
                {
                    {"QRCode", qrImageElement }
                };

                var reportGenerator = new ReportGenerator(locationOfLibreOfficeSoffice);
                reportGenerator.Convert(indexPath, pdfPath, placeholders);

                var fs = System.IO.File.OpenRead(pdfPath);
                return File(fs, "application/pdf", pdfName);
            }

            else if (action == "Edit")
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine(@"cd C:\\Users\\piotr\\Source\\Repos\\SuperSaryk\\CIAbmc\\CIAbmc\\wwwroot\\html\\");
                cmd.StandardInput.WriteLine(@"./pdftohtml PDF.pdf converted_pdf.html");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                Console.WriteLine(cmd.StandardOutput.ReadToEnd());

                var convertedHtmlPath = "C:\\Users\\piotr\\Source\\Repos\\SuperSaryk\\CIAbmc\\CIAbmc\\wwwroot\\html\\converted_pdfs.html";
                var htmlContent = System.IO.File.ReadAllText(convertedHtmlPath);
                var formattedHtml = HtmlFormatter.FormatHtml(htmlContent);
                var fileBytes = Encoding.UTF8.GetBytes(formattedHtml);
                return File(fileBytes, "text/html", "FormattedConvertedPDF.html");
            }

            return RedirectToAction("Index"); // Redirect to some default action if needed
        }


        public IActionResult PDF()
        {
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public static class HtmlFormatter
        {
            public static string FormatHtml(string input)
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(input);
                using (var sw = new StringWriter())
                {
                    htmlDoc.OptionOutputAsXml = true;
                    htmlDoc.Save(sw);
                    return sw.ToString();
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
