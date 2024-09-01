using System.Net;
using System.Text;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineLibraryAdmin.Models;

namespace OnlineLibraryAdmin.Controllers;

public class GenresController : Controller
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
    
    public async Task<IActionResult> ImportGenres(IFormFile file)
    {
        try
        {
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            List<Genre> genres = GetAllGenresFromFile(file.FileName);
            
            
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;

            HttpClient client = new HttpClient(handler);
            string url = "https://online-libraryweb20240831204444.azurewebsites.net/api/Admin/ImportGenres";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(genres), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            var result = response.Content.ReadAsAsync<bool>().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Success", "Genres");
            }

            return RedirectToAction("Index", "Genres");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View("Error");
        }

    }

    private List<Genre> GetAllGenresFromFile(string fileName)
    {
        List<Genre> genres = new List<Genre>();
        string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        using(var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                while (reader.Read())
                {
                    genres.Add(new Models.Genre
                    {
                        Name = reader.GetValue(0).ToString(),
                    });
                }

            }
        }
        return genres;

    }
}