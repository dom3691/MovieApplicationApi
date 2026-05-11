using System.ComponentModel.DataAnnotations;

namespace Movie_Application_Backend.Models;

public class MovieItem
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Genre { get; set; } = string.Empty;

    [Range(1888, 2100)]
    public int ReleaseYear { get; set; }

    [Range(0, 10)]
    public double Rating { get; set; }

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow;
}
