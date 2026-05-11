using System.ComponentModel.DataAnnotations;

namespace Movie_Application_Backend.Models;

public class CreateMovieRequest
{
    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Genre { get; set; } = string.Empty;

    [Range(1888, 2100)]
    public int ReleaseYear { get; set; }

    [Range(0, 10)]
    public double Rating { get; set; }
}
