using System.Net;
using System.Text;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineLibraryAdmin.Models;

namespace OnlineLibraryAdmin.Controllers;

public class AuthorsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Success()
    {
        return View();
    }
    
    public async Task<IActionResult> ImportAuthors(IFormFile file)
    {
        try
        {
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            List<Author> authors = GetAllAuthorsFromFile(file.FileName);

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;

            HttpClient client = new HttpClient(handler);
            
            string url = "https://online-libraryweb20240831204444.azurewebsites.net/api/Admin/ImportAuthors";
            HttpContent content = new StringContent(JsonConvert.SerializeObject(authors), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Success", "Authors");
            }
            
            return RedirectToAction("Index", "Authors");
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return View("Error");
        }
    }
    
    private List<Author> GetAllAuthorsFromFile(string fileName)
    {
        List<Author> authors = new List<Author>();
        string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                while (reader.Read())
                {
                    var name = reader.GetValue(0)?.ToString();
                    var surname = reader.GetValue(1)?.ToString();
                    var dateOfBirthValue = reader.GetValue(2)?.ToString();
                    DateTime? dateOfBirth = null;

                    if (DateTime.TryParse(dateOfBirthValue, out DateTime parsedDate))
                    {
                        dateOfBirth = parsedDate;
                    }

                    if (name != null && surname != null && dateOfBirth.HasValue)
                    {
                        authors.Add(new Author
                        {
                            Name = name,
                            Surname = surname,
                            DateOfBirth = dateOfBirth.Value
                        });
                    }
                }
            }
        }

        return authors;
    }
}