using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Dtos;
using MoviesRepositoryPattern.Core.Consts;
using MoviesRepositoryPattern.Core.Models;
using MoviesRepositoryPattern.Core.Repositories;
using MoviesRepositoryPattern.EF;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoivesRepository<Movie> _moviesRepository;
        private readonly IBaseRepository<Genre> _baseRepository;

        private new List<string> _allowedExtenstions = new()
        {
            ".jpg",
            ".png"
        };
        private long _maxAllowedPosterSize = 1048576;

        public MoviesController(IMoivesRepository<Movie> moivesRepository, IBaseRepository<Genre> baseRepository)
        {
            _moviesRepository = moivesRepository;
            _baseRepository = baseRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]MovieDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only jpg and png files allowed");
            if(dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max size allowed is 1MB!");
            var isvalidGenre = await _baseRepository.GetGenreById(dto.GenreId);
            if (isvalidGenre == null)
                return BadRequest($"Invalid Genre ID: {dto.GenreId}");

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            Movie movie = new()
            {
                GenreId = dto.GenreId,
                Title = dto.Title,
                Rate = dto.Rate,
                Poster = dataStream.ToArray(),
                StoryLine = dto.StoryLine,
                Year = dto.Year,
            };
            await _moviesRepository.CreateMovie(movie);
            await _moviesRepository.Complete();
            return Ok(movie);
        }
        [HttpGet("GetAllMovies")]
        public async Task<IActionResult> GellAllMovies()
        {
            
            return Ok(await _moviesRepository.GetAllMovies(new []{ "Genre" })); 
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMoviesByFilter()
        {
            return Ok(await _moviesRepository.FindAll(a => a.Title == "Fd", 1, 1, a => a.GenreId == 2, OrderBy.Ascending));
        }
    }
}
