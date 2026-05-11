using Microsoft.AspNetCore.Mvc;
using Movie_Application_Backend.Models;
using Movie_Application_Backend.Services;

namespace Movie_Application_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieCrudController : ControllerBase
{
    private readonly IMovieCrudService _movieCrudService;
    private readonly ILogger<MovieCrudController> _logger;

    public MovieCrudController(IMovieCrudService movieCrudService, ILogger<MovieCrudController> logger)
    {
        _movieCrudService = movieCrudService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var movies = await _movieCrudService.GetAllAsync(cancellationToken);
        return Ok(movies);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var movie = await _movieCrudService.GetByIdAsync(id, cancellationToken);
        if (movie is null)
        {
            _logger.LogWarning("Movie not found. MovieId: {MovieId}", id);
            return NotFound($"Movie with ID {id} was not found.");
        }

        return Ok(movie);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request, CancellationToken cancellationToken)
    {
        var movie = await _movieCrudService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateMovieRequest request, CancellationToken cancellationToken)
    {
        var updated = await _movieCrudService.UpdateAsync(id, request, cancellationToken);
        if (!updated)
        {
            _logger.LogWarning("Movie update skipped. Movie not found. MovieId: {MovieId}", id);
            return NotFound($"Movie with ID {id} was not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _movieCrudService.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            _logger.LogWarning("Movie delete skipped. Movie not found. MovieId: {MovieId}", id);
            return NotFound($"Movie with ID {id} was not found.");
        }

        return NoContent();
    }
}
