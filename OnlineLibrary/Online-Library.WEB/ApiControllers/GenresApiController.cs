using Microsoft.AspNetCore.Mvc;
using Online_Library.Domain.Dtos;
using Online_Library.Domain.Entities;
using Online_Library.Service.Interfaces;

namespace Online_Library.WEB.ApiControllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresApiController(IGenresService genresService) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<GenreDto>> GetGenres()
        {
            var genres = genresService.GetAllGenres();
            var genresDtos = new List<GenreDto>();
            foreach (var genre in genres)
            {
                genresDtos.Add(new GenreDto
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }
            return Ok(genresDtos);
        }
        
        [HttpGet("{genreId}")]
        public ActionResult<GenreDto> GetGenreById(Guid genreId)
        {
            var genre = genresService.GetGenre(genreId);

            var genreDto = new GenreDto
            {
                Id = genreId,
                Name = genre.Name
            };
            
            return Ok(genreDto);
        }
        
        [HttpPost]
        public ActionResult<GenreDto> InsertGenre(GenreDto genreDto)
        {
            var genre = new Genre
            {
                Name = genreDto.Name
            };
            
            genresService.InsertGenre(genre);

            return Ok();
        }
        
        [HttpPut]
        public ActionResult<GenreDto> UpdateGenre(GenreDto genreDto)
        {
            var genre = genresService.GetGenre(genreDto.Id);
            genresService.UpdateGenre(genre);
            
            return Ok();
        }
        
        [HttpDelete("{genreId}")]
        public ActionResult<GenreDto> DeleteGenre(Guid genreId)
        {
            var genre = genresService.GetGenre(genreId);
            genresService.DeleteGenre(genre);
            
            return Ok();
        }
        
    }
    
}
