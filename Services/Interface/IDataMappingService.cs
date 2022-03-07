using AMDB_Anime_Manga_Database.Models.Database;
using AMDB_Anime_Manga_Database.Models.TMDB;
using System.Threading.Tasks;

namespace AMDB_Anime_Manga_Database.Services.Interface
{
    public interface IDataMappingService
    {
        // Movie
        Task<Movie> MapMovieDetailAsync(MovieDetail movie);

        // TV Show
        Task<TVShow> MapTVShowDetailAsync(TVShowDetail movie);

        // Actor Detail
        ActorDetail MapActorDetail(ActorDetail actor);
    }
}
