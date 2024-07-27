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
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
        {
            var genres = await genresService.GetAllGenres();
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
        public async Task<ActionResult<GenreDto>> GetGenreById(Guid genreId)
        {
            var genre = await genresService.GetGenre(genreId);

            var genreDto = new GenreDto
            {
                Id = genreId,
                Name = genre.Name
            };
            
            return Ok(genreDto);
        }
        
        [HttpPost]
        public async Task<ActionResult<GenreDto>> InsertGenre(GenreDto genreDto)
        {
            var genre = new Genre
            {
                Name = genreDto.Name
            };
            
            await genresService.InsertGenre(genre);

            return Ok();
        }
        
        [HttpPut]
        public async Task<ActionResult<GenreDto>> UpdateGenre(GenreDto genreDto)
        {
            var genre = await genresService.GetGenre(genreDto.Id);
            await genresService.UpdateGenre(genre);
            
            return Ok();
        }
        
        [HttpDelete("{genreId}")]
        public async Task<ActionResult<GenreDto>> DeleteGenre(Guid genreId)
        {
            var genre = await genresService.GetGenre(genreId);
            await genresService.DeleteGenre(genre);
            
            return Ok();
        }
        
    }
    
}
