using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Library.Domain.Dtos;
using Online_Library.Domain.Entities;
using Online_Library.Service.Implementations;
using Online_Library.Service.Interfaces;

namespace Online_Library.WEB.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(
        IGenresService genresService, 
        IAuthorsService authorsService,
        ISubscriptionsService subscriptionsService) : ControllerBase
    {
        [HttpGet("[action]")]
        public List<Subscription> GetAllSubscriptions()
        {
            return subscriptionsService.GetAllSubscriptions().ToList();
        }
        
        [HttpGet("GetDetailsForSubscription/{id}")]
        public Subscription GetDetailsForSubscription(Guid id)
        {
            return subscriptionsService.GetDetailsForSubscription(id)!;
        }
        
        
        [HttpPost("[action]")]
        public async Task ImportGenres(List<GenreDto> model)
        {
            foreach (var item in model)
            {
                if (item.Name is null)
                {
                    continue;
                }
                
                var genreCheck = genresService.GetGenreByName(item.Name);

                if (genreCheck is null) {
                    var genre = new Genre
                    {
                        Id = Guid.NewGuid(),
                        Name = item.Name
                    };

                    await genresService.InsertGenreAsync(genre);
                }
            }
        }

        //[HttpPost("[action]")]
        //public async Task ImportAuthors([FromBody] List<AuthorDto> model)
        //{
        //    foreach (var item in model)
        //    {
        //        if (item.Name is null || item.Surname is null)
        //        {
        //            continue;
        //        }

        //        var author = new Author
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = item.Name,
        //            Surname = item.Surname,
        //            DateOfBirth = item.DateOfBirth
        //        };

        //        await authorsService.InsertAuthorAsync(author);
        //    }
        //}

        [HttpPost("ImportAuthors")]
        public async Task<IActionResult> ImportAuthors([FromBody] List<AuthorDto> model)
        {
            foreach (var item in model)
            {
                if (item.Name is null || item.Surname is null)
                {
                    continue;
                }

                var author = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = item.Name,
                    Surname = item.Surname,
                    DateOfBirth = item.DateOfBirth
                };

                await authorsService.InsertAuthorAsync(author);
            }
            return Ok(); // Ensure you return an appropriate status code
        }

    }
}
