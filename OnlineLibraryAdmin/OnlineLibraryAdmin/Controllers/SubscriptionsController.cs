using System.Globalization;
using System.Text;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Mvc;
using GemBox.Document;
using Newtonsoft.Json;
using OnlineLibraryAdmin.Models;

namespace OnlineLibraryAdmin.Controllers;

public class SubscriptionsController : Controller
{
    public SubscriptionsController() {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
    }
    
    public IActionResult Index()
    { 
        var client = new HttpClient();
        var url = "https://online-libraryweb20240831204444.azurewebsites.net/api/Admin/GetAllSubscriptions";
        var response = client.GetAsync(url).Result;

        var data = response.Content.ReadAsAsync<List<Subscription>>().Result;
        return View(data);
    }

    public IActionResult Details(Guid id)
    {
        var client = new HttpClient();
        string url = "https://online-libraryweb20240831204444.azurewebsites.net/api/Admin/GetDetailsForSubscription/" + id;
        
        var response = client.GetAsync(url).Result;

        var data = response.Content.ReadAsAsync<Subscription>().Result;
        return View(data);
    }

    public FileContentResult CreateInvoice(Guid id)
    {
        var client = new HttpClient();
        var url = "https://online-libraryweb20240831204444.azurewebsites.net/api/Admin/GetDetailsForSubscription/" + id;
        var response = client.GetAsync(url).Result;

        var data = response.Content.ReadAsAsync<Subscription>().Result;

        var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
        var document = DocumentModel.Load(templatePath);
        document.Content.Replace("{{SubscriptionNumber}}", data.Id.ToString());
        
        var fullname = data.User is null
            ? ""
            : data.User.FirstName + " " + data.User.LastName;

        
        document.Content.Replace("{{UserFullName}}", fullname);
        document.Content.Replace("{{SubscriptionStartDate}}", data.StartDate.ToString(CultureInfo.CurrentCulture));
        document.Content.Replace("{{SubscriptionEndDate}}", data.EndDate.ToString(CultureInfo.CurrentCulture));

        StringBuilder sb = new StringBuilder();
        
        var stream = new MemoryStream();
        document.Save(stream, new PdfSaveOptions());
        return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportedInvoice.pdf");
    }

    [HttpGet]
    public FileContentResult ExportSubscriptions()
    {
        string fileName = "AllSubscriptions.xlsx";
        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        using(var workBook = new XLWorkbook())
        {
            IXLWorksheet worksheet = workBook.Worksheets.Add("Subscriptions");

            worksheet.Cell(1, 1).Value = "Subscription ID";
            worksheet.Cell(1, 2).Value = "Customer Fullname";
            worksheet.Cell(1, 3).Value = "Subscription Start Date";
            worksheet.Cell(1, 4).Value = "Subscription End Date";

            var client = new HttpClient();
            var url = "https://online-libraryweb20240831204444.azurewebsites.net/api/Admin/GetAllSubscriptions";
            var response = client.GetAsync(url).Result;

            var data = response.Content.ReadAsAsync<List<Subscription>>().Result;

            for(int i=0; i< data.Count(); i++)
            {
                var subscription = data[i];
                    worksheet.Cell(i + 2, 1).Value = subscription.Id.ToString();

                    var fullname = subscription.User is null
                        ? ""
                        : subscription.User.FirstName + " " + subscription.User.LastName;
                    
                    worksheet.Cell(i + 2, 2).Value = fullname;
                    worksheet.Cell(i + 2, 3).Value = subscription.StartDate;
                    worksheet.Cell(i + 2, 4).Value = subscription.EndDate;
            }

            using(var stream = new MemoryStream())
            {
                workBook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, contentType, fileName);
            }
        }
    }
}