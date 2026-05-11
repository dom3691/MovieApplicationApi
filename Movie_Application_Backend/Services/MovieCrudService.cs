using Movie_Application_Backend.Models;

namespace Movie_Application_Backend.Services;

public class MovieCrudService : IMovieCrudService
{
    private readonly List<MovieItem> _movies = new();
    private int _nextId = 1;

    public IEnumerable<MovieItem> GetAll()
    {
        return _movies;
    }

    public MovieItem? GetById(int id)
    {
        return _movies.FirstOrDefault(m => m.Id == id);
    }

    public MovieItem Create(CreateMovieRequest request)
    {
        var movie = new MovieItem
        {
            Id = _nextId++,
            Title = request.Title,
            Genre = request.Genre,
            ReleaseYear = request.ReleaseYear,
            Rating = request.Rating
        };

        _movies.Add(movie);
        return movie;
    }

    public bool Update(int id, CreateMovieRequest request)
    {
        var movie = GetById(id);
        if (movie is null)
        {
            return false;
        }

        movie.Title = request.Title;
        movie.Genre = request.Genre;
        movie.ReleaseYear = request.ReleaseYear;
        movie.Rating = request.Rating;
        return true;
    }

    public bool Delete(int id)
    {
        var movie = GetById(id);
        if (movie is null)
        {
            return false;
        }

        _movies.Remove(movie);
        return true;
    }
}
