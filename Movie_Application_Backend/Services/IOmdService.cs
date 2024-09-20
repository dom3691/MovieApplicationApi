using Movie_Application_Backend.Models;

namespace Movie_Application_Backend.Services;

public interface IOmdService
{
    Task<MovieDetailsResponse> GetMovieDetailsAsync(string imdbId);
    Task<SearchResponse> SearchMoviesAsync(string title);
}
