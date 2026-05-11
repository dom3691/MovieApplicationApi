using Microsoft.EntityFrameworkCore;
using Movie_Application_Backend.Data;
using Movie_Application_Backend.Models;

namespace Movie_Application_Backend.Services;

public class MovieCrudService : IMovieCrudService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<MovieCrudService> _logger;

    public MovieCrudService(AppDbContext dbContext, ILogger<MovieCrudService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IReadOnlyList<MovieItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Movies
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<MovieItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<MovieItem> CreateAsync(CreateMovieRequest request, CancellationToken cancellationToken = default)
    {
        var movie = new MovieItem
        {
            Title = request.Title.Trim(),
            Genre = request.Genre.Trim(),
            ReleaseYear = request.ReleaseYear,
            Rating = request.Rating,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        };

        _dbContext.Movies.Add(movie);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Movie created. MovieId: {MovieId}, Title: {Title}", movie.Id, movie.Title);
        return movie;
    }

    public async Task<bool> UpdateAsync(int id, CreateMovieRequest request, CancellationToken cancellationToken = default)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (movie is null)
        {
            return false;
        }

        movie.Title = request.Title.Trim();
        movie.Genre = request.Genre.Trim();
        movie.ReleaseYear = request.ReleaseYear;
        movie.Rating = request.Rating;
        movie.UpdatedUtc = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Movie updated. MovieId: {MovieId}", id);

        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (movie is null)
        {
            return false;
        }

        _dbContext.Movies.Remove(movie);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Movie deleted. MovieId: {MovieId}", id);

        return true;
    }
}
