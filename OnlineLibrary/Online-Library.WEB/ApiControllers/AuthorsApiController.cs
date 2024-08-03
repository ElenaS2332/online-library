using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Library.Domain.Dtos;
using Online_Library.Domain.Entities;
using Online_Library.Service.Interfaces;

namespace Online_Library.WEB.ApiControllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsApiController(IAuthorsService authorsService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors = await authorsService.GetAllAuthorsAsync();
            var authorsDtos = new List<AuthorDto>();
            foreach (var author in authors)
            {
                authorsDtos.Add(new AuthorDto
                {
                    Name = author.Name,
                    Surname = author.Surname,
                    DateOfBirth = author.DateOfBirth
                });
            }
            return Ok(authorsDtos);
        }
        
        [HttpGet("{authorId}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorById(Guid authorId)
        {
            var author = await authorsService.GetAuthorAsync(authorId);

            if (author is null)
            {
                return NotFound();
            }
            
            var authorsDto = new AuthorDto
            {
                Name = author.Name,
                Surname = author.Surname,
                DateOfBirth = author.DateOfBirth
            };
            
            return Ok(authorsDto);
        }
        
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> InsertAuthor(AuthorDto authorDto)
        {
            var author = new Author
            {
                Name = authorDto.Name,
                Surname = authorDto.Surname,
                DateOfBirth = authorDto.DateOfBirth
            };
            
            await authorsService.InsertAuthorAsync(author);

            return Ok();
        }
        
        [HttpPut("{authorId}")]
        public async Task<ActionResult<AuthorDto>> UpdateAuthor(Guid authorId, AuthorDto authorDto)
        {
            var author = await authorsService.GetAuthorAsync(authorId);

            if (author is null)
            {
                return NotFound();
            }

            author.Name = authorDto.Name;
            author.Surname = authorDto.Surname;
            author.DateOfBirth = authorDto.DateOfBirth;
            
            await authorsService.UpdateAuthorAsync(author);
            
            return Ok();
        }
        
        [HttpDelete("{authorId}")]
        public async Task<ActionResult<AuthorDto>> DeleteAuthor(Guid authorId)
        {
            var author = await authorsService.GetAuthorAsync(authorId);
            
            if (author is null)
            {
                return NotFound();
            }
            
            await authorsService.DeleteAuthorAsync(author);
            
            return Ok();
        }

    }
}
