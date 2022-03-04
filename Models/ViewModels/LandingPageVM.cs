using AMDB_Anime_Manga_Database.Models.TMDB;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AMDB_Anime_Manga_Database.Models.ViewModels
{
    public class LandingPageVM
    {
        public List<Collection> CustomCollections { get; set; }

        // Movies
        public MovieSearch NowPlaying { get; set; }
        public MovieSearch Popular { get; set; }
        public MovieSearch TopRated { get; set; }
        public MovieSearch Upcoming { get; set; }

        // TV Shows
        public TVSearch CurrentlyRunning { get; set; }
        public TVSearch PopularShows { get; set; }
        public TVSearch TopRatedShows { get; set; }
        public TVSearch UpcomingShows { get; set; }
        public TVSearch ShowGenre { get; set; }
    }
}
