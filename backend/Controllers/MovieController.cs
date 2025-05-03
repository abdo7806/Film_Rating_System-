using Film_Rating_System.Models.Repositories;
using Film_Rating_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Film_Rating_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        public readonly IRepository<Movie> _MovieRepository;
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public MovieController(IRepository<Movie> MovieRepository, AppDbContext context,
            IFileService fileService)
        {
            this._MovieRepository = MovieRepository;
            _context = context;
            _fileService = fileService;
        }

        [HttpGet("{id}", Name = "GetMovieById")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<Movie> GetMovieById(int id)
        {

            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            var Movie = _MovieRepository.GetById(id);

            if (Movie == null)
            {
                return NotFound($"Movie with ID {id} not found.");
            }

            return Ok(Movie);
        }

        // GET: api/<MoviesController>
        [HttpGet("All", Name = "GetAllMovie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Movie>> GetAllMovies() // Define a method to get all Movies.
        {

            var data = _MovieRepository.GetAll();

            if (data.Count == 0)
            {
                return NotFound("No Movies Found!");
            }
            return Ok(data); // Returns the list of Movies.
        }


        // ✅ Add new movie (Admin only)
       // [Authorize(Roles = "Admin")]
        [HttpPost(Name = "AddMovie")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddMovie([FromForm] CreateMovieDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string? imageUrl = null;

            if (dto.Image != null)
                imageUrl = await _fileService.SaveImageAsync(dto.Image);

            var movie = new Movie
            {
                Title = dto.Title,
                Description = dto.Description,
                Genre = dto.Genre,
                ReleaseDate = dto.ReleaseDate,
                ImageUrl = imageUrl
            };



            _MovieRepository.Add(movie);
            

            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }




        // PUT api/<MoviesController>/5
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}", Name = "UpdateMovie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> UpdateMovie(int id, [FromForm] CreateMovieDto dto)
        {
            var movie = _MovieRepository.GetById(id);
            if (movie == null)
                return NotFound($"Movie with ID {id} not found.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (dto.Image != null)
            {
                if (!string.IsNullOrEmpty(movie.ImageUrl))
                    _fileService.DeleteImage(movie.ImageUrl);

                movie.ImageUrl =  await _fileService.SaveImageAsync(dto.Image);
            }


            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.Genre = dto.Genre;
            movie.ReleaseDate = dto.ReleaseDate;


            _MovieRepository.Edit(id, movie);
            
            return Ok(movie);


        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}", Name = "DeleteMovie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteMovie(int id)
        {
            var movie = _MovieRepository.GetById(id);
            if (!string.IsNullOrEmpty(movie.ImageUrl))
                _fileService.DeleteImage(movie.ImageUrl);

            int MovieID = _MovieRepository.Delete(id);
            if (MovieID == -1)
            {
                return NotFound($"Movie with ID {id} not found.");
            }

            return Ok($"Movie with ID {id} has been deleted.");
        }


    }
}
