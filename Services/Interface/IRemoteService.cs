using AMDB_Anime_Manga_Database.Enums;
using AMDB_Anime_Manga_Database.Models.TMDB;
using System.Threading.Tasks;

namespace AMDB_Anime_Manga_Database.Services.Interfaces
{
    public interface IRemoteService
    {
        // Movie 
        Task<MovieDetail> MovieDetailAsync(int id);
        Task<MovieSearch> SearchMoviesAsync(Category category, int count);

        // TV Show
        Task<TVShowDetail> TVShowDetailAsync(int id);
        Task<TVSearch> SearchTVShowAsync(Category category, int count);

        // Actor Detail Search
        Task<ActorDetail> ActorDetailAsync(int id);
    }
}
