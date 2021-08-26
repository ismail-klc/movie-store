using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [Authorize(Role = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _movieService.GetMovies();
            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovieDto movie)
        {
            await _movieService.AddMovie(movie);

            return StatusCode(201);
        }

    }
}
