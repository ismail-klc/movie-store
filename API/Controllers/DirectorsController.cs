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
    public class DirectorsController : ControllerBase
    {
        private readonly IDirectorService _directorService;

        public DirectorsController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var directors = await _directorService.GetDirectors();
            return Ok(directors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var director = await _directorService.GetDirectorById(id);
            return Ok(director);
        }

        [Authorize(Role = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDirector(CreateDirectorDto director)
        {
            await _directorService.AddDirector(director);

            return StatusCode(201);
        }

        [Authorize(Role = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateActor(UpdateDirectorDto dto)
        {
            await _directorService.UpdateDirector(dto);

            return Ok();
        }

    }
}
