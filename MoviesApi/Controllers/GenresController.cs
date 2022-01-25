using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Dtos;
using MoviesRepositoryPattern.Core.Models;
using MoviesRepositoryPattern.Core.Repositories;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IBaseRepository<Genre> _baseRepository;

        public GenresController(IBaseRepository<Genre> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenresAsync()
        {
            return Ok(await _baseRepository.GetAllGenre());
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Genre genre = new()
            {
                Name = dto.Name,
            };
            await _baseRepository.CreateGenre(genre);
            await _baseRepository.Complete();
            return Ok(genre);
        }
        [HttpPost("CreateListAsync")]
        public async Task<IActionResult> CreateListAsync(List<CreateGenreDto> dtos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var genre = new List<Genre>();
            genre = dtos.Select(a => new Genre { Name = a.Name }).ToList();

             _baseRepository.CreateListGenre(genre);
            await _baseRepository.Complete();
            return Ok(genre);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenreAsync(byte id,[FromBody] CreateGenreDto dto)
        {
            var genre = await _baseRepository.GetGenreById(id);
            if (genre == null) return NotFound($"No Genra with ID: {id}");
            genre.Name = dto.Name;
            _baseRepository.UpdateGenre(genre);
            await _baseRepository.Complete();
            return Ok(genre);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGernreAsync(byte id)
        {
            var genre = await _baseRepository.GetGenreById(id);
            if (genre == null) return NotFound($"No Genra with ID: {id}");
            _baseRepository.DeleteGenre(genre);
            await _baseRepository.Complete();
            return Ok();
        }
    }
}
