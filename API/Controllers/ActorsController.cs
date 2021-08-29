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
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var actors = await _actorService.GetActors();
            return Ok(actors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var actor = await _actorService.GetActorById(id);
            return Ok(actor);
        }

        [Authorize(Role = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateActor(CreateActorDto actor)
        {
            await _actorService.AddActor(actor);

            return StatusCode(201);
        }

        [Authorize(Role = "Admin")]
        [HttpPost("movie")]
        public async Task<IActionResult> AddMovieToActor([FromQuery(Name = "actor")] int actorId,
            [FromQuery(Name = "movie")] int movieId)
        {
            await _actorService.AddMovieToActor(actorId, movieId);
            return Ok();
        }

    }
}
