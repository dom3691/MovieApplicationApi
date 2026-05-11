using Movie_Application_Backend.Models;

namespace Movie_Application_Backend.Services;

public interface IMovieCrudService
{
    Task<IReadOnlyList<MovieItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MovieItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<MovieItem> CreateAsync(CreateMovieRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, CreateMovieRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
