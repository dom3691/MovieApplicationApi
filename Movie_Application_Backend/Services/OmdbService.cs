using Movie_Application_Backend.Models;
using Newtonsoft.Json;

namespace Movie_Application_Backend.Services;


public class OmdbService : IOmdService
{
    private readonly HttpClient _httpClient;
    private List<string> _searchHistory = new List<string>();
    private readonly string _apiKey = "ecd0cd0a";


    public OmdbService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _searchHistory = new List<string>();

    }

    public async Task<SearchResponse> SearchMoviesAsync(string title)
    {
        string url = $"http://www.omdbapi.com/?apikey={_apiKey}&s={title}";

        var response = await _httpClient.GetStringAsync(url);
        SaveSearchQuery(title);

        return JsonConvert.DeserializeObject<SearchResponse>(response);
    }

    public async Task<MovieDetailsResponse> GetMovieDetailsAsync(string imdbId)
    {
        string apiUrl = $"http://www.omdbapi.com/?apikey={_apiKey}&i={imdbId}";

        var response = await _httpClient.GetStringAsync(apiUrl);

        return JsonConvert.DeserializeObject<MovieDetailsResponse>(response);
    }
    public List<string> GetSearchHistory()
    {
        return _searchHistory;
    }

    public void SaveSearchQuery(string title)
    {
        _searchHistory.Insert(0, title);
        if (_searchHistory.Count > 5)
            _searchHistory = _searchHistory.Take(5).ToList();
    }
}
