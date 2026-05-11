namespace Movie_Application_Backend.Models;

public class MovieItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public double Rating { get; set; }
}
