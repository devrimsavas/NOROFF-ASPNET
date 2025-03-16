using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NOROFF_ASPNET.Data;
using NOROFF_ASPNET.Models;
using Microsoft.AspNetCore.Authorization;

namespace NOROFF_ASPNET.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]

    public class GenreController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public GenreController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //GET api/genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            if (_dataContext == null)
            {
                return NotFound();
            }
            var genres = await _dataContext.Genres.ToListAsync();
            return Ok(genres);
        }

        //Get api/genres/id 
        [HttpGet("{Id}")]
        public async Task<ActionResult<Genre>> GetGenre(int Id)
        {
            if (_dataContext.Genres == null)
            {
                return NotFound();
            }
            var genre = await _dataContext.Genres.FindAsync(Id);
            if (genre is null)
            {
                return NotFound();
            }
            return Ok(genre);

        }
        //POST api/genres 
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Genre>> AddGenre([FromBody] Genre genre)
        {
            var existedGenre = await _dataContext.Genres.FirstOrDefaultAsync(g => g.Name == genre.Name);
            if (existedGenre != null)
            {
                return BadRequest(new { Message = "this genre is already existed" });
            }
            _dataContext.Genres.Add(genre);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
        }

        //update 
        [HttpPut("{Id}")]
        [Authorize] // Now only authorized users can update genres
        public async Task<ActionResult> UpdateGenre(int Id, [FromBody] Genre genre)
        {
            if (Id != genre.Id)
            {
                return BadRequest(new { Message = "ID in the URL does not match ID in the body" });
            }

            // Check if the genre exists before updating
            var existingGenre = await _dataContext.Genres.FindAsync(Id);
            if (existingGenre == null)
            {
                return NotFound(new { Message = "Genre not found" });
            }

            // Update only changed properties
            existingGenre.Name = genre.Name;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { Message = "Concurrency error occurred while updating the genre" });
            }

            return NoContent(); //  Returns 204 No Content after a successful update
        }

        //delete

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<ActionResult<Genre>> DeleteGenre(int Id)
        {
            if (_dataContext.Genres == null)
            {
                return NotFound();
            }
            var genre = await _dataContext.Genres.FindAsync(Id);
            if (genre is null)
            {
                return NotFound();
            }
            _dataContext.Genres.Remove(genre);
            await _dataContext.SaveChangesAsync();
            return NoContent();

        }


    }
}