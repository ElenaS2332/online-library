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
    
    public IActionResult ImportAuthors(IFormFile file)
    {
        string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

        using (FileStream fileStream = System.IO.File.Create(pathToUpload))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }

        List<Author> authors = GetAllAuthorsFromFile(file.FileName);
        HttpClient client = new HttpClient();
        string url = "http://localhost:5042/api/Admin/ImportAuthors";
        
        HttpContent content = new StringContent(JsonConvert.SerializeObject(authors), Encoding.UTF8, "application/json");

        HttpResponseMessage response = client.PostAsync(url, content).Result;

        var result = response.Content.ReadAsAsync<bool>().Result;

        return RedirectToAction("Index", "Authors");

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
                    authors.Add(new Models.Author
                    {
                        Name = reader.GetValue(0).ToString(),
                        Surname = reader.GetValue(1).ToString(),
                        DateOfBirth = DateTime.Parse(reader.GetValue(2).ToString())
                    });
                }

            }
        }

        return authors;
    }
}