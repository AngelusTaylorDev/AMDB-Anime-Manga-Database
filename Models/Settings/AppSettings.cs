using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMDB_Anime_Manga_Database.Models.Settings
{
    public class AppSettings
    {
        internal IOptions<AppSettings> value;

        // Settings used for the AnimeMangaDatabase and TMDB
        public AMDBSettings AMDBSettings { get; set; }
        public TMDBSettings TMDBSettings { get; set; }
    }
}
