using Movie_Application_Backend.Models;

namespace Movie_Application_Backend.Services;

public interface IMovieCrudService
{
    IEnumerable<MovieItem> GetAll();
    MovieItem? GetById(int id);
    MovieItem Create(CreateMovieRequest request);
    bool Update(int id, CreateMovieRequest request);
    bool Delete(int id);
}
