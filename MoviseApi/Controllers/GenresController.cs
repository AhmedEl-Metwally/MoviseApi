using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviseApi.Dtos;
using Microsoft.EntityFrameworkCore;
using MoviseApi.Services;

namespace MoviseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genresService;

        public GenresController( IGenresService genresService)
        {
            
            this._genresService = genresService;
        }


        [HttpGet]
        public async Task<IActionResult> GetallAsync()
        {
            var genres = await _genresService.GetAll();
            return Ok(genres);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreataGenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };
            await _genresService.Add(genre);
            return Ok(genre);
        }


        [HttpPut("id")]
        public async Task<IActionResult>UpdateAsync(byte id, [FromBody] CreataGenreDto dto)
        {
            var genre = await _genresService.GetById(id);
            if (genre == null)
            return NotFound($"No genre was found with ID ; {id}");
            genre.Name =dto.Name;
            _genresService.Update(genre);
            return Ok(genre);

        }


        [HttpDelete("id")]
        public async Task<IActionResult> DateAsync(byte id)
        {
            var genre = await _genresService.GetById(id);
            if (genre == null)
            return NotFound($"No genre was found with ID ; {id}");

            _genresService.Delete(genre);
            return Ok(genre);
        }
    

    }
}
