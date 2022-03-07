using AMDB_Anime_Manga_Database.Enums;
using AMDB_Anime_Manga_Database.Models.Database;
using AMDB_Anime_Manga_Database.Models.Settings;
using AMDB_Anime_Manga_Database.Models.TMDB;
using AMDB_Anime_Manga_Database.Services.Interface;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TVShowCast = AMDB_Anime_Manga_Database.Models.Database.TVShowCast;

namespace AMDB_Anime_Manga_Database.Services
{
    public class TMDBMappingService : IDataMappingService
    {

        private readonly AppSettings _appSettings;
        private readonly IImageService _imageService;

        public TMDBMappingService(IOptions<AppSettings> appSettings, IImageService imageService)
        {
            _appSettings = appSettings.Value;
            _imageService = imageService;
        }


        public ActorDetail MapActorDetail(ActorDetail actor)
        {
            //1. Image
            actor.profile_path = BuildCastImage(actor.profile_path);

            //2. Bio
            if (string.IsNullOrEmpty(actor.biography))
            {
                actor.biography = "Not Available";
            }

            //Place of birth
            if (string.IsNullOrEmpty(actor.place_of_birth))
            {
                actor.place_of_birth = "Not Available";
            }

            //Birthday
            if (string.IsNullOrEmpty(actor.birthday))
                actor.birthday = "Not Available";
            else
                actor.birthday = DateTime.Parse(actor.birthday).ToString("MMM dd, yyyy");

            return actor;
        }

        public async Task<Movie> MapMovieDetailAsync(MovieDetail movie)
        {
            Movie newMovie = null;

            try
            {
                newMovie = new Movie()
                {
                    MovieId = movie.id,
                    Title = movie.title,
                    TagLine = movie.tagline,
                    OverView = movie.overview,
                    RunTime = movie.runtime,
                    Backdrop = await EncodeBackdropImageAsync(movie.backdrop_path),
                    BackDropType = BuildImageType(movie.backdrop_path),
                    Poster = await EncodePosterImageAsync(movie.poster_path),
                    PosterType = BuildImageType(movie.poster_path),
                    Rating = GetRating(movie.release_dates),
                    ReleseDate = DateTime.Parse(movie.release_date),
                    TrailerUrl = BuildTrailerPath(movie.videos),
                    VoteAverage = movie.vote_average
                };

                // Getting all the avaiable cast data
                var castMembers = movie.credits.cast.OrderByDescending(c => c.popularity)
                                                    .GroupBy(c => c.cast_id)
                                                    .Select(g => g.FirstOrDefault())
                                                    .Take(20).ToList();

                castMembers.ForEach(member =>
                {
                    newMovie.Cast.Add(new MovieCast()
                    {
                        CastID = member.id,
                        Department = member.known_for_department,
                        Name = member.name,
                        Character = member.character,
                        ImageUrl = BuildCastImage(member.profile_path),
                    });
                });

                var crewMembers = movie.credits.crew.OrderByDescending(c => c.popularity)
                                                    .GroupBy(c => c.id)
                                                    .Select(g => g.First())
                                                    .Take(20).ToList();

                crewMembers.ForEach(member =>
                {
                    newMovie.Crew.Add(new MovieCrew()
                    {
                        CrewID = member.id,
                        Department = member.department,
                        Name = member.name,
                        Job = member.job,
                        ImageUrl = BuildCastImage(member.profile_path)
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MapMovieDetailAsync: {ex.Message}");
            }

            return newMovie;
        }

        public async Task<TVShow> MapTVShowDetailAsync(TVShowDetail tvShow)
        {
            TVShow newTvShow = null;

            try
            {
                newTvShow = new TVShow()
                {
                    TVShowId = tvShow.id,
                    Title = tvShow.name,
                    TagLine = tvShow.tagline,
                    OverView = tvShow.overview,
                    number_of_episodes = tvShow.number_of_episodes,
                    number_of_seasons = tvShow.number_of_seasons,
                    Backdrop = await EncodeBackdropImageAsync(tvShow.backdrop_path),
                    BackDropType = BuildImageType(tvShow.backdrop_path),
                    Poster = await EncodePosterImageAsync(tvShow.poster_path),
                    PosterType = BuildImageType(tvShow.poster_path),
                    ReleseDate = DateTime.Parse(tvShow.first_air_date),
                    TrailerUrl = BuildTrailerPath(tvShow.videos),
                    ReviewAverage = tvShow.vote_average
                };

                // Getting all the avaiable cast data
                var castMembers = tvShow.credits.cast.OrderByDescending(c => c.popularity)
                                                    .GroupBy(c => c.cast_id)
                                                    .Select(g => g.FirstOrDefault())
                                                    .Take(20).ToList();

                castMembers.ForEach(member =>
                {
                    newTvShow.Cast.Add(new TVShowCast()
                    {
                        CastID = member.id,
                        Department = member.known_for_department,
                        Name = member.name,
                        Character = member.character,
                        ImageUrl = BuildCastImage(member.profile_path),
                    });
                });

                var crewMembers = tvShow.credits.crew.OrderByDescending(c => c.popularity)
                                                    .GroupBy(c => c.id)
                                                    .Select(g => g.First())
                                                    .Take(20).ToList();

                crewMembers.ForEach(member =>
                {
                    newTvShow.Crew.Add(new TVShowCrew()
                    {
                        CrewID = member.id,
                        Department = member.department,
                        Name = member.name,
                        Job = member.job,
                        ImageUrl = BuildCastImage(member.profile_path)
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MapMovieDetailAsync: {ex.Message}");
            }

            return newTvShow;
        }

        // Encoding the backdrop image
        private async Task<byte[]> EncodeBackdropImageAsync(string path)
        {
            var backdropPath = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.AMDBSettings.DefaultBackdropSize}/{path}";
            return await _imageService.EncodeImageURLAsync(backdropPath);
        }

        // Building the backdrop image Type
        private string BuildImageType(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            return $"image/{Path.GetExtension(path).TrimStart('.')}";
        }

        // Getting the Poster path
        private async Task<byte[]> EncodePosterImageAsync(string path)
        {
            var posterPath = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.AMDBSettings.DefaultPosterSize}/{path}";
            return await _imageService.EncodeImageURLAsync(posterPath);
        }

        // Getting the ESRB Rating
        private ESRBRating GetRating(Release_Dates dates)
        {
            var rating = ESRBRating.NR;
            var certification = dates.results.FirstOrDefault(r => r.iso_3166_1 == "US");
            if (certification is not null)
            {
                var apiRating = certification.release_dates.FirstOrDefault(c => c.certification != "")?.certification.Replace("-", "");
                if (!string.IsNullOrEmpty(apiRating))
                {
                    rating = (ESRBRating)Enum.Parse(typeof(ESRBRating), apiRating, true);
                }
            }
            return rating;
        }

        // Building the Trailer Path
        private string BuildTrailerPath(Videos videos)
        {
            var videoKey = videos.results.FirstOrDefault(r => r.type.ToLower().Trim() == "trailer" && r.key != "")?.key;
            return string.IsNullOrEmpty(videoKey) ? videoKey : $"{_appSettings.TMDBSettings.BaseYouTubePath}{videoKey}";
        }

        private string BuildCastImage(string profilePath)
        {
            if (string.IsNullOrEmpty(profilePath))
                return _appSettings.AMDBSettings.DefaultCastImage;

            return $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.AMDBSettings.DefaultPosterSize}/{profilePath}";
        }
    }
}
