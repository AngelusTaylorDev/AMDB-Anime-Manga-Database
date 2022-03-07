using AMDB_Anime_Manga_Database.Enums;
using AMDB_Anime_Manga_Database.Models.Settings;
using AMDB_Anime_Manga_Database.Models.TMDB;
using AMDB_Anime_Manga_Database.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace AMDB_Anime_Manga_Database.Services
{
    public class TMDBService : IRemoteService
    {
        // Adding the App settings and Http Client
        private readonly AppSettings _appSettings;
        private readonly IHttpClientFactory _httpClient;

        public TMDBService(IOptions<AppSettings> appSettings, IHttpClientFactory httpClient)
        {
            _appSettings = appSettings.Value;
            _httpClient = httpClient;
        }

        // Actor Detail
        public async Task<ActorDetail> ActorDetailAsync(int id)
        {
            //Step 1: Setup a default return object
            ActorDetail actorDetail = new();

            //Step 2: Assemble the full request uri string
            var query = $"{_appSettings.TMDBSettings.BaseUrl}/person/{id}";
            var queryParams = new Dictionary<string, string>()
            {
                { "api_key", _appSettings.AMDBSettings.TmDbApiKey },
                { "language", _appSettings.TMDBSettings.QueryOptions.Language}
            };
            var requestUri = QueryHelpers.AddQueryString(query, queryParams);

            //Step 3: Create a client and execute the request
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            //Step 4: Return the ActorDetail object
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                var dcjs = new DataContractJsonSerializer(typeof(ActorDetail));
                actorDetail = (ActorDetail)dcjs.ReadObject(responseStream);
            }

            return actorDetail;
        }

        // Movie Detail
        public async Task<MovieDetail> MovieDetailAsync(int id)
        {
            //Step 1: Setup default return object
            MovieDetail movieDetail = new();

            //Step 2: Assemble the request
            var query = $"{_appSettings.TMDBSettings.BaseUrl}/movie/{id}";
            var queryParams = new Dictionary<string, string>()
            {
                { "api_key", _appSettings.AMDBSettings.TmDbApiKey },
                { "language", _appSettings.TMDBSettings.QueryOptions.Language},
                { "append_to_response", _appSettings.TMDBSettings.QueryOptions.AppendToResponse}
            };
            var requestUri = QueryHelpers.AddQueryString(query, queryParams);

            //Step 3: Create client and execute request
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            //Step 4: Deserialize into Moviedetail 
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var dcjs = new DataContractJsonSerializer(typeof(MovieDetail));
                movieDetail = dcjs.ReadObject(responseStream) as MovieDetail;
            }
            return movieDetail;
        }

        // Search for a Movie
        public async Task<MovieSearch> SearchMoviesAsync(Category category, int count)
        {
            //Step 1: Setup a default instance of MovieSearch
            MovieSearch movieSearch = new();

            //Step 2: Assemble the full request uri string
            var query = $"{_appSettings.TMDBSettings.BaseUrl}/movie/{category}";

            var queryParams = new Dictionary<string, string>()
            {
                { "api_key", _appSettings.AMDBSettings.TmDbApiKey },
                { "language", _appSettings.TMDBSettings.QueryOptions.Language },
                { "page", _appSettings.TMDBSettings.QueryOptions.Page }
            };

            var requestUri = QueryHelpers.AddQueryString(query, queryParams);

            //Step 3: Create a client and execute the request
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            //Step 4: Return the MovieSearch object
            if (response.IsSuccessStatusCode)
            {
                // Data Contract json Serializer
                var dcjs = new DataContractJsonSerializer(typeof(MovieSearch));
                using var responseStream = await response.Content.ReadAsStreamAsync();
                movieSearch = (MovieSearch)dcjs.ReadObject(responseStream);
                movieSearch.results = movieSearch.results.Take(count).ToArray();
                movieSearch.results.ToList().ForEach(r => r.poster_path = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.AMDBSettings.DefaultPosterSize}/{r.poster_path}");
            }

            return movieSearch;
        }

        // Search for a Show
        public async Task<TVSearch> SearchTVShowAsync(Category category, int count)
        {
            //Step 1: Setup a default instance of MovieSearch
            TVSearch tvSearch = new();

            //Step 2: Assemble the full request uri string
            var query = $"{_appSettings.TMDBSettings.BaseUrl}/movie/{category}";

            var queryParams = new Dictionary<string, string>()
            {
                { "api_key", _appSettings.AMDBSettings.TmDbApiKey },
                { "language", _appSettings.TMDBSettings.QueryOptions.Language },
                { "page", _appSettings.TMDBSettings.QueryOptions.Page }
            };

            var requestUri = QueryHelpers.AddQueryString(query, queryParams);

            //Step 3: Create a client and execute the request
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            //Step 4: Return the MovieSearch object
            if (response.IsSuccessStatusCode)
            {
                // Data Contract json Serializer
                var dcjs = new DataContractJsonSerializer(typeof(TVSearch));
                using var responseStream = await response.Content.ReadAsStreamAsync();
                tvSearch = (TVSearch)dcjs.ReadObject(responseStream);
                tvSearch.results = tvSearch.results.Take(count).ToArray();
                tvSearch.results.ToList().ForEach(r => r.poster_path = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.AMDBSettings.DefaultPosterSize}/{r.poster_path}");
            }

            return tvSearch;
        }

        // Show Detail
        public async Task<TVShowDetail> TVShowDetailAsync(int id)
        {
            //Step 1: Setup default return object
            TVShowDetail tvShowDetail = new();

            //Step 2: Assemble the request
            var query = $"{_appSettings.TMDBSettings.BaseUrl}/movie/{id}";
            var queryParams = new Dictionary<string, string>()
            {
                { "api_key", _appSettings.AMDBSettings.TmDbApiKey },
                { "language", _appSettings.TMDBSettings.QueryOptions.Language},
                { "append_to_response", _appSettings.TMDBSettings.QueryOptions.AppendToResponse}
            };
            var requestUri = QueryHelpers.AddQueryString(query, queryParams);

            //Step 3: Create client and execute request
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            //Step 4: Deserialize into Moviedetail 
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var dcjs = new DataContractJsonSerializer(typeof(TVShowDetail));
                tvShowDetail = dcjs.ReadObject(responseStream) as TVShowDetail;
            }
            return tvShowDetail;
        }
    }
}
