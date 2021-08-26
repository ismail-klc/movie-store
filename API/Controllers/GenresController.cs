using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreService.GetGenres();
            return Ok(genres);
        }

        [Authorize(Role = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateGenre(CreateGenreDto genre)
        {
            await _genreService.AddGenre(genre);

            return StatusCode(201);
        }

    }
}
