using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Application_Backend.Services;

namespace Movie_Application_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IOmdService _omdService;

        public MovieController(IOmdService omdService)
        {
            _omdService = omdService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchMovies([FromQuery] string title)
        {
            var searchResults = await _omdService.SearchMoviesAsync(title);

            return Ok(searchResults);
        }

        [HttpGet("{imdbId}")]
        public async Task<IActionResult> GetMovieDetails(string imdbId)
        {
            var movieDetails = await _omdService.GetMovieDetailsAsync(imdbId);
            return Ok(movieDetails);
        }
    }
}
