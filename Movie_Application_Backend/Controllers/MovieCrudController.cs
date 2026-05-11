using Microsoft.AspNetCore.Mvc;
using Movie_Application_Backend.Models;
using Movie_Application_Backend.Services;

namespace Movie_Application_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieCrudController : ControllerBase
{
    private readonly IMovieCrudService _movieCrudService;

    public MovieCrudController(IMovieCrudService movieCrudService)
    {
        _movieCrudService = movieCrudService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_movieCrudService.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var movie = _movieCrudService.GetById(id);
        if (movie is null)
        {
            return NotFound($"Movie with ID {id} was not found.");
        }

        return Ok(movie);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateMovieRequest request)
    {
        var movie = _movieCrudService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
    }

    [HttpPut("{id:int}")]
z    public IActionResult Update(int id, [FromBody] CreateMovieRequest request)
    {
        var updated = _movieCrudService.Update(id, request);
        if (!updated)
        {
            return NotFound($"Movie with ID {id} was not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _movieCrudService.Delete(id);
        if (!deleted)
        {
            return NotFound($"Movie with ID {id} was not found.");
        }

        return NoContent();
    }
}
