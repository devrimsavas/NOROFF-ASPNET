
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NOROFF_ASPNET.Data;
using NOROFF_ASPNET.Models;
using Microsoft.AspNetCore.Authorization;

namespace NOROFF_ASPNET.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public MoviesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetMovies()
        {
            if (_dataContext == null)
            {
                return NotFound();
            }
            var movies = await _dataContext.Movies.ToListAsync();

            return Ok(movies);


        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Movie>> GetMovie(int Id)
        {
            if (_dataContext == null)
            {
                return NotFound(new { Message = "The movie you search is not found" });
            }

            var movie = await _dataContext.Movies.FindAsync(Id);
            if (movie == null)
            {
                return NotFound(new { Message = "Movie is not found" });
            }
            return Ok(movie);

        }
        //add a movie 
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Movie>> AddMovie([FromBody] Movie movie)
        {
            var existedMovie = await _dataContext.Movies.FirstOrDefaultAsync(m => m.Name == movie.Name);
            if (existedMovie != null)
            {
                return BadRequest(new { Message = $"this movie with name {movie.Name} already exists" });
            }

            await _dataContext.Movies.AddAsync(movie);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new { Id = movie.Id }, movie);

        }
        [HttpPost("addmultimovies")]
        public async Task<ActionResult> AddMovies([FromBody] List<Movie> movies)
        {
            if (movies == null || movies.Count == 0)
            {
                return BadRequest(new { Message = "No movies provided" });
            }

            // Check for duplicate movies before adding
            var existingMovies = await _dataContext.Movies
                .Where(m => movies.Select(movie => movie.Name).Contains(m.Name))
                .ToListAsync();

            if (existingMovies.Any())
            {
                return BadRequest(new { Message = "Some movies already exist", ExistingMovies = existingMovies });
            }

            await _dataContext.Movies.AddRangeAsync(movies);
            await _dataContext.SaveChangesAsync();

            return Ok(new { Message = $"{movies.Count} movies added successfully" });
        }

        [HttpPut("updatemovie/{id}")]
        public async Task<ActionResult<Movie>> UpdateMovie(int id, [FromBody] Movie updatedMovie)
        {
            var existedMovie=await _dataContext.Movies.FindAsync(id);
            if (existedMovie==null) 
            {
                return NotFound(new {Message="this movie does not exist"});

            }
            
            existedMovie.Genre=updatedMovie.Genre ?? existedMovie.Genre;
            existedMovie.TicketPrice=updatedMovie.TicketPrice ?? existedMovie.TicketPrice;

           
            await _dataContext.SaveChangesAsync();
            return Ok(new {Message="movie updated"});






        }

    }
}
