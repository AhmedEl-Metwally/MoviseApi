using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviseApi.Models;
using MoviseApi.Services;

namespace MoviseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;
        private readonly IMapper _mapper;   


        private List<string> _allowedExtenstion = new () { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 10485760;


        public MoviesController(IMoviesService moviesService, IMapper mapper, IGenresService genresService)
        {
            _moviesService = moviesService;
            _genresService = genresService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsyns()
        {
            var Movies = await _moviesService.GetAll();

            var data =_mapper .Map <IEnumerable<MovieDetailsDto>> (Movies); 

            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _moviesService.GetById(id);
            if (movie == null)
                return NotFound();
            var dto =_mapper.Map<MovieDetailsDto> (movie); 

            return Ok(dto);  
        }


        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            var Movies = await _moviesService.GetAll(genreId);
            var dto = _mapper.Map<IEnumerable<MovieDetailsDto>>(Movies);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> createasync([FromForm] MovieDto dto)
        {
            if (dto == null)
                return BadRequest("Poster is required!");

            if (!_allowedExtenstion.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("only .jpg and .png images are allowed");


            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 10MB!");

            var isValidGenre = await _genresService.IsvalidGenre(dto.GenreId);

            if (!isValidGenre)
                return BadRequest("Invalid genre ID!");  
                 


            using var datastream = new MemoryStream();
            await dto.Poster.CopyToAsync(datastream);


            var Movie = _mapper.Map<Movie> (dto);
            Movie.Poster = datastream.ToArray();

              _moviesService.Add(Movie);


            return Ok(Movie);


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdataAsyne(int id, [FromForm] MovieDto dto)
        {
          var movie = await _moviesService.GetById (id);

            if (movie == null)
                return NotFound($"No movie was found with ID{id} ");
            var isValidGenre = await _genresService.IsvalidGenre (dto.GenreId); 

            if (!isValidGenre)
                return BadRequest("Invalid genre ID!");

            if(dto.Poster != null)
            {
                if (!_allowedExtenstion.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("only .jpg and .png images are allowed");


                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 10MB!");

                using var datastream = new MemoryStream();
                await dto.Poster.CopyToAsync(datastream);
                movie .Poster = datastream.ToArray();

            }

            movie.Title = dto.Title;
            movie.GenreId = dto.GenreId;
            movie.Year = dto.Year;
            movie.StoreLine = dto.StoryLine;    
            movie.Ratr = dto.Ratr;

            _moviesService.Update(movie);  


            return Ok(movie);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var Movie = await _moviesService.GetById(id);

            if (Movie == null)
                return NotFound($"No movie was found with ID{id} ");

            _moviesService.Delete(Movie);   

            return Ok(Movie);
            


        }
    }
}
